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
            searchPanel.Controls.Add(pagerControl1);
            pagerControl1.Dock = DockStyle.Right;
            //激活OnPageChanged事件  
            
            pagerControl1.OnPageChanged += new EventHandler(pagerControl1_OnPageChanged);
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
                dataGridView.Rows[i].Height = (dataGridView.Height - dataGridView.ColumnHeadersHeight - SystemInformation.HorizontalScrollBarHeight) / maxRows;
            }
        }


        public string DataName { get; set; }
        public string TableName { get; set; }

        public DataTable DetailDataTable
        {
            get
            {
                return (DataTable)dataGridView.DataSource;
            }
            set
            {
                dataGridView.DataSource = null;
                dataGridView.DataSource = value;
                GridLayoutResize();
            }
        }

        /// <summary>  
        /// 页数变化时调用绑定数据方法  
        /// </summary>  
        /// <param name="sender"></param>  
        /// <param name="e"></param>  
        private void pagerControl1_OnPageChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        /// <summary>  
        /// 重新加载数据  
        /// </summary>  
        private void LoadData()
        {
            int count;
            //using (MAction action = new MAction("Users.txt", "Txt Path={0}"))
            //{
            //    action.Select(pagerControl1.PageIndex, pagerControl1.PageSize, string.Empty, out count).Bind(gvUsers);
            //    pagerControl1.DrawControl(count);
            //}
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

        int maxRows = 30;
        PagerControl pagerControl1 = new PagerControl();

        private void idButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(idTextBox.Text)&& !string.IsNullOrWhiteSpace(DataName) && !string.IsNullOrWhiteSpace(TableName))
            {
                ClientQueryService clientQueryService = new ClientQueryService();
                var result = clientQueryService.GetInfoById(idTextBox.Text.Trim(), DataName,TableName, CommandType.Text, null);
                SearchResultForm searchResultForm = new SearchResultForm(result);
                searchResultForm.ShowDialog();
            }
        }
    }
}
