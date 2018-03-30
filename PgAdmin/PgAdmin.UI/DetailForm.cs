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

namespace PgAdmin.UI
{
    public partial class DetailForm : Form
    {
        public DetailForm()
        {
            InitializeComponent();
            //pageSearchPanel.Controls.Add(pagerControl1);
            //pagerControl1.Dock = DockStyle.Bottom;           
            //pagerControl1.OnPageChanged += new EventHandler(pagerControl1_OnPageChanged);
        }

        private void GridLayoutResize()
        {
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.Height = this.Height - searchPanel.Height;
            for (int i = 0; i < dataGridView.Columns.Count; i++)
            {
                dataGridView.Columns[i].Width = dataGridView.Width / dataGridView.Columns.Count;
            }
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                dataGridView.Rows[i].Height = (dataGridView.Height - dataGridView.ColumnHeadersHeight - SystemInformation.HorizontalScrollBarHeight) / pageSize;
            }
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
                    InitDataSet();
                GridLayoutResize();
            }
        }


        private void pagerControl1_OnPageChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void InitDataSet()
        {
            nMax = dt.Rows.Count;
            pageCount = (nMax / pageSize);
            if ((nMax % pageSize) > 0) pageCount++;
            pageCurrent = 1;
            nCurrent = 0;
            LoadData();
            pageSizeText.Text = pageSize.ToString();
            totalCountLabel.Visible = true;
            totalCountLabel.Text = "/" + pageCount.ToString();

        }
        private void LoadData()
        {
            int nStartPos = 0;
            int nEndPos = 0;

            DataTable dtTemp = dt.Clone();

            #region set button
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
            #endregion

            if (pageCurrent == pageCount)
            {
                nEndPos = nMax;
            }
            else
                nEndPos = pageSize * pageCurrent;

            nStartPos = nCurrent;

            totalCountLabel.Text = "/" + pageCount.ToString();
            if (dt.Rows.Count == 0)
            {
                pageToolStripText.Text = "0";
            }
            else
            {
                pageToolStripText.Text = Convert.ToString(pageCurrent);
            }
            //从元数据源复制记录行
            for (int i = nStartPos; i < nEndPos; i++)
            {
                dtTemp.ImportRow(dt.Rows[i]);
                nCurrent++;
            }


            bindingSource1.DataSource = dtTemp;
            bindingNavigator1.BindingSource = bindingSource1;
            dataGridView.DataSource = bindingSource1;


        }

        private void dataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView.Rows[e.RowIndex].Cells.Count > 2 && dataGridView.Columns.Contains("data"))
            {
                var title = dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                if (!String.IsNullOrEmpty(title))
                {
                    JObject jo = (JObject)JsonConvert.DeserializeObject(dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString());
                    JsonForm jsonForm = new JsonForm();
                    jsonForm.JsonData = jo;
                    jsonForm.Title = title;
                    jsonForm.StartPosition = FormStartPosition.CenterScreen;
                    jsonForm.Show();
                    jsonForm.Focus();
                }
            }
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
                    SearchResultForm searchResultForm = new SearchResultForm(result);
                    searchResultForm.ShowDialog();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("error occoured when query data,please ensure you have selected the right table","error" ,MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
        }



        private void pageSizeText_TextChanged(object sender, EventArgs e)
        {
            int num = 0;
            //输入不符合规范时，默认设置为100  
            if (!int.TryParse(pageSizeText.Text.Trim(), out num) || num <= 0)
            {
                num = 25;
                pageSizeText.Text = "25";
            }
            pageSize = num;
            InitDataSet();

        }


        private void bindingNavigator1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "Move first":
                    InitDataSet();
                    break;
                case "Move previous":
                    if (pageCurrent == 1)
                    {
                        //MessageBox.Show("已经是第一页，请点击“下一页”查看!");
                        return;
                    }
                    else
                    {
                        pageCurrent--;
                        nCurrent = pageSize * (pageCurrent - 1);
                        LoadData();
                    }
                    break;
                case "Move next":
                    if (pageCurrent == pageCount)
                    {
                        //bindingNavigator1.PositionItem.Text = pageCurrent.ToString();
                        //bindingNavigatorPositionItem.Text= pageCurrent.ToString();
                        //MessageBox.Show("已经是最后一页，请点击“上一页”查看!");
                        return;
                    }
                    else
                    {
                        pageCurrent++;
                        nCurrent = pageSize * (pageCurrent - 1);
                        LoadData();
                    }
                    break;
                case "Move last":
                    pageCurrent = pageCount;
                    nCurrent = pageSize * (pageCurrent - 1);
                    LoadData();
                    break;
                default: break;
            }

        }

        int pageSize = 25;
        int pageCurrent = 1; //当前页数从1开始
        int nCurrent = 0; //当前记录数从0开始
        int pageCount = 0;
        int nMax = 0;

        private void bindingNavigatorPositionItem_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    var index = int.Parse(pageToolStripText.Text);
            //    if (index > pageCount)
            //    {
            //        return;
            //    }
            //    else
            //    {
            //        bindingNavigator1.PositionItem.Text = pageCurrent.ToString();
            //        pageCurrent = index;
            //        nCurrent = pageSize * (pageCurrent - 1);
            //        LoadData();
            //    }
            //}
            //catch (Exception er)
            //{
            //    InitDataSet();
            //}
        }
    }
}