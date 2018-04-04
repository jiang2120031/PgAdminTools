using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;

namespace PgAdmin.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            try
            {
                InitializeComponent();
                this.StartPosition = FormStartPosition.CenterScreen;
                FormLayout();
                menuTreeForm.updateTable += new UpdateDelegate(UpdateDetailTable);
                menuTreeForm.updateTable.Invoke();
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Error, "MainForm:" + e.Message);
            }
        }

        private void FormLayout()
        {
            MenuPanel.Dock = DockStyle.Left;
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
        private void FormResize()
        {
            MenuPanel.Width = this.Width / 5;
        }

        public void UpdateDetailTable()
        {
            FormResize();
            detailForm.DetailDataTable = menuTreeForm.DetailDataTable;
            detailForm.DataName = menuTreeForm.DataName;
            detailForm.TableName = menuTreeForm.TableName;
        }

        TableListForm detailForm = new TableListForm();
        MenuTreeForm menuTreeForm = new MenuTreeForm();
        #region FIELDS
        private ILogger logger = LogManager.GetLogger("PgAdmin");
        #endregion
    }

    public delegate void UpdateDelegate();

}
