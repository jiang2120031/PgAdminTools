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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            FormLayout();
            menuTreeForm.updateTable += new UpdateDelegate(UpdateDetailTable);
            menuTreeForm.updateTable.Invoke();
        }

        private void FormLayout()
        {
            MenuPanel.Dock = DockStyle.Left;
            MenuPanel.Width = this.Width / 5;
            menuTreeForm.TopLevel = false;
            MenuPanel.Controls.Add(menuTreeForm);
            menuTreeForm.Show();
            menuTreeForm.Dock = DockStyle.Fill;
            menuTreeForm.FormBorderStyle = FormBorderStyle.None;
            detailForm.TopLevel = false;
            tablePanel.Controls.Add(detailForm);
            detailForm.Show();
            detailForm.Dock = DockStyle.Fill;
            detailForm.FormBorderStyle = FormBorderStyle.None;
        }

        public void UpdateDetailTable()
        {
            detailForm.DetailDataTable = menuTreeForm.DetailDataTable;
            detailForm.DataName = menuTreeForm.DataName;
            detailForm.TableName = menuTreeForm.TableName;
        }

        DetailForm detailForm = new DetailForm();
        MenuTreeForm menuTreeForm = new MenuTreeForm();

    }
    public delegate void UpdateDelegate();
      
}
