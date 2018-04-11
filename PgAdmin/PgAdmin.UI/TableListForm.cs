using PgAdmin.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using NLog;
using System.Configuration;
using System.IO;

namespace PgAdmin.UI
{
    public partial class TableListForm : Form
    {
        public TableListForm()
        {
            InitializeComponent();
            bindingNavigator1.Enabled = false;
        }

        private void GridLayoutResize()
        {
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.Height = this.Height - searchPanel.Height;
            for (int i = 0; i < dataGridView.Columns.Count; i++)
            {
                dataGridView.Columns[i].Width = (dataGridView.Width - dataGridView.RowHeadersWidth) / dataGridView.Columns.Count;
            }
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                dataGridView.Rows[i].Height = (dataGridView.Height - dataGridView.ColumnHeadersHeight
                    - SystemInformation.HorizontalScrollBarHeight) / pageSize;
            }
        }

        public void refreshDataTable()
        {
            DetailDataTable = GetDetailDocuments(TableName, DataName);
        }

        public string DataName { get; set; }
        public string TableName { get; set; }
        DataTable dt;
        public DataTable DetailDataTable
        {
            get
            {
                return dt;
            }
            set
            {
                dt = value;
                if (dt != null)
                    InitDataSet(dt);
                GridLayoutResize();
            }
        }


        private void InitDataSet(DataTable dataTable)
        {
            bindingNavigator1.Enabled = true;
            pageToolStripText.Enabled = true;
            nMax = dataTable.Rows.Count;
            pageCount = (nMax / pageSize);
            if ((nMax % pageSize) > 0) pageCount++;
            pageCurrent = 1;
            nCurrent = 0;
            LoadData(dataTable);
            pageSizeText.Text = pageSize.ToString();
            totalCountLabel.Visible = true;
            totalCountLabel.Text = "/" + pageCount.ToString();
            totalCountsBox.Text = nMax.ToString();
        }
        private void SetButton()
        {
            if (pageCurrent <= 1)
            {
                bindingNavigatorMoveFirstItem.Enabled = false;
                bindingNavigatorMovePreviousItem.Enabled = false;
            }
            else
            {
                bindingNavigatorMoveFirstItem.Enabled = true;
                bindingNavigatorMovePreviousItem.Enabled = true;
            }


            if (pageCurrent >= pageCount)
            {
                bindingNavigatorMoveNextItem.Enabled = false;
                bindingNavigatorMoveLastItem.Enabled = false;
            }
            else
            {
                bindingNavigatorMoveNextItem.Enabled = true;
                bindingNavigatorMoveLastItem.Enabled = true;
            }
        }
        private void LoadData(DataTable dataTable)
        {
            int nStartPos = 0;
            int nEndPos = 0;

            dtTemp = dataTable.Clone();

            SetButton();
            if (dt.Rows.Count > 0)
            {
                if (pageCurrent == pageCount)
                {
                    nEndPos = nMax;
                }
                else
                    nEndPos = pageSize * pageCurrent;
            }

            nStartPos = nCurrent;

            totalCountLabel.Text = "/" + pageCount.ToString();
            if (dataTable.Rows.Count <= 0)
            {
                pageToolStripText.Text = "0";
            }
            else
            {
                pageToolStripText.Text = Convert.ToString(pageCurrent);
                for (int i = nStartPos; i < nEndPos; i++)
                {
                    dtTemp.ImportRow(dataTable.Rows[i]);
                    nCurrent++;
                }
            }
            bindingSource1.DataSource = dtTemp;
            dataGridView.DataSource = bindingSource1;

        }

        private void dataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView.Rows[e.RowIndex].Cells.Count > 2 && ContainsColumn("data"))
            {
                var title = dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                if (!String.IsNullOrEmpty(title))
                {
                    JObject jo = (JObject)JsonConvert.DeserializeObject(dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString());
                    string id = dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                    ShowJsonForm(jo, id);
                }
            }
        }

        private bool ContainsColumn(string containsValue)
        {
            foreach (DataGridViewColumn item in dataGridView.Columns)
            {
                Regex headerValue = new Regex(containsValue);
                if (headerValue.IsMatch(item.HeaderText))
                    return true;
            }
            return false;
        }

        private void idButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(DataName) || string.IsNullOrWhiteSpace(TableName))
                {
                    MessageBox.Show("Please select a table", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (string.IsNullOrWhiteSpace(idTextBox.Text))
                {
                    MessageBox.Show("Please enter id", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    ClientQueryService clientQueryService = new ClientQueryService();
                    var result = clientQueryService.GetInfoById(idTextBox.Text.Trim(), DataName, TableName,
                        CommandType.Text, null);
                    if (result != null)
                    {
                        JObject jo = (JObject)JsonConvert.DeserializeObject(result.Data);
                        ShowJsonForm(jo,result.Id);
                    }
                    else
                        MessageBox.Show("Please enter valid id", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, "idSearchError:" + ex.Message);
            }
        }



        private void pageSizeText_TextChanged(object sender, EventArgs e)
        {
            int num = 0;
            if (!int.TryParse(pageSizeText.Text.Trim(), out num) || num <= 0)
            {
                num = 25;
                pageSizeText.Text = "25";
            }
            pageSize = num;
            InitDataSet(dt);

        }


        private void bindingNavigator1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "Move first":
                    InitDataSet(dt);
                    break;
                case "Move previous":
                    if (pageCurrent == 1)
                    {
                        MessageBox.Show("This is the first!");
                        return;
                    }
                    else
                    {
                        pageCurrent--;
                        nCurrent = pageSize * (pageCurrent - 1);
                        pageToolStripText.Text = pageCurrent.ToString();
                    }
                    break;
                case "Move next":
                    if (pageCurrent == pageCount)
                    {
                        MessageBox.Show("This is the last!");
                        return;
                    }
                    else
                    {
                        pageCurrent++;
                        nCurrent = pageSize * (pageCurrent - 1);
                        pageToolStripText.Text = pageCurrent.ToString();

                    }
                    break;
                case "Move last":
                    pageCurrent = pageCount;
                    nCurrent = pageSize * (pageCurrent - 1);
                    pageToolStripText.Text = pageCurrent.ToString();
                    break;
                default: break;
            }

        }

        private void bindingNavigatorPositionItem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsInt(pageToolStripText.Text))
                {
                    var index = int.Parse(pageToolStripText.Text);

                    if (index > pageCount)
                    {
                        pageToolStripText.Text = pageCurrent.ToString();
                        MessageBox.Show("Out of range!");
                        return;
                    }
                    else
                    {
                        pageCurrent = index;
                        nCurrent = pageSize * (pageCurrent - 1);
                        LoadData(dt);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid input!");
                    return;
                }
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, "CurrentPageSizeSetError:" + ex.Message);
                return;
            }

        }

        private void logButton_Click(object sender, EventArgs e)
        {
            try
            {
                var path = Environment.CurrentDirectory + "\\logs";
                var logsfilename = GetLatestFileTimeInfo(path);
                if (!String.IsNullOrEmpty(logsfilename))
                {
                    path += "\\"+ logsfilename + "\\logs";
                    DirectoryInfo logs = new DirectoryInfo(path);
                    FileInfo[] fileInfo = logs.GetFiles("Error.log");
                    if (fileInfo != null)
                    {
                        string log = "";
                        foreach (FileInfo temp in fileInfo)
                        {
                            using (StreamReader sr = temp.OpenText())
                            {
                                log += sr.ReadToEnd();
                            }
                        }
                        MessageBox.Show(log);
                    }
                }
                else
                {
                    MessageBox.Show("No logs!");
                }
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, "CurrentPageSizeSetError:" + ex.Message);
                return;
            }

        }


        public static bool IsInt(string strNumber)
        {
            Regex objNotNumberPattern = new Regex("[^0-9]");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            const string strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidIntegerPattern + ")");
            return !objNotNumberPattern.IsMatch(strNumber) &&
             !objTwoMinusPattern.IsMatch(strNumber) &&
             objNumberPattern.IsMatch(strNumber);
        }

        private void ShowJsonForm(JObject jo,string id)
        {
            try
            {
                DetailForm jsonForm = new DetailForm();
                jsonForm.JsonData = jo;
                jsonForm.Title = idTextBox.Text;
                jsonForm.StartPosition = FormStartPosition.CenterScreen;
                jsonForm.DbId = id;
                jsonForm.DataName = this.DataName;
                jsonForm.TableName = this.TableName;
                jsonForm.pForm = this;
                jsonForm.Show();
                jsonForm.Focus();
                
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, "ShowJsonFormError:" + ex.Message);
            }
        }

        static string GetLatestFileTimeInfo(string dir)
        {
            List<DateTime> dateList = new List<DateTime>();
            DirectoryInfo search = new DirectoryInfo(dir);
            FileSystemInfo[] fsinfos = search.GetFileSystemInfos();

            if (Directory.Exists(dir) && fsinfos.Length > 0)
            {
                
                foreach (FileSystemInfo fsinfo in fsinfos)
                {
                    if (fsinfo is DirectoryInfo)
                    {
                        dateList.Add(fsinfo.CreationTime);
                    }
                }
                dateList.Sort();
                var date = dateList[dateList.Count - 1].ToString("yyyy-MM-dd");
                return date;
            }
            else
            {
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                return string.Empty;
            }
        }

        private DataTable GetDetailDocuments(string tablename, string database)
        {
            try
            {
                ClientQueryService postgresHelper = new ClientQueryService();
                var sql = @"SELECT * FROM " + tablename;
                var dbConnStr = postgresHelper.ConnString+ database;
                var dataTable = new DataTable();
                dataTable.Load(postgresHelper.ExecuteReader(dbConnStr, System.Data.CommandType.Text, sql));
                return dataTable;
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Error, "GetDetailDocuments:" + e.Message);
                return null;
            }
        }

        #region FIELDS
        int pageSize = 25;
        int pageCurrent = 1; //当前页数从1开始
        int nCurrent = 0; //当前记录数从0开始
        int pageCount = 0;
        int nMax = 0;
        DataTable dtTemp;
        private ILogger logger = LogManager.GetLogger("DetailForm");

        #endregion


    }
}