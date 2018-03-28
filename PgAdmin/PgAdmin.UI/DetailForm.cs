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

namespace PgAdmin.UI
{
    public partial class DetailForm : Form
    {
        public DetailForm()
        {
            InitializeComponent();          
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
            }
        }

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
