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
    public partial class Form1 : Form
    {
        public Form1()
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
            menuTreeForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            detailForm.TopLevel = false;
            tablePanel.Controls.Add(detailForm);
            detailForm.Show();
            detailForm.Dock = DockStyle.Fill;
            detailForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        public void UpdateDetailTable()
        {
            detailForm.DetailDataTable = menuTreeForm.DetailDataTable;
        }

        DetailForm detailForm = new DetailForm();
        MenuTreeForm menuTreeForm = new MenuTreeForm();

    }
    public delegate void UpdateDelegate();
      
}
