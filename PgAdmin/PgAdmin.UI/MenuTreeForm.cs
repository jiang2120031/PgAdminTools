using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PgAdmin.Services;
using PgAdmin.Services.Model;
using System.Configuration;
using NLog;
using System.Text.RegularExpressions;

namespace PgAdmin.UI
{
    public partial class MenuTreeForm : Form
    {
        public MenuTreeForm()
        {
            InitializeComponent();
            InitMenuTree();
        }

        DataTable detailDataTable;
        ClientQueryService postgresHelper = new ClientQueryService();
        FindAllDbAndTables findDocuments = new FindAllDbAndTables();
        public UpdateDelegate updateTable;
        private ILogger logger = LogManager.GetLogger("MenuTreeForm");

        public DataTable DetailDataTable
        {
            get
            {
                return detailDataTable;
            }
            set
            {
                detailDataTable = value;
            }
        }


        public string DataName { get; set; }
        public string TableName { get; set; }
        List<DbName> dbTrees;

        private List<DbName> GetDBDocuments(string sql)
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["postgres"].ConnectionString;
                var dbList = findDocuments.FindDbAndTables(connectionString, System.Data.CommandType.Text, sql).ToList();
                return dbList;
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Error, "GetDBDocuments:" + e.Message);
                return null;
            }
        }
        private DataTable GetDetailDocuments(string tablename, string database)
        {
            try
            {
                var sql = @"SELECT * FROM " + tablename;
                var dbConnStr = string.Format("Host=localhost;Database={0};Port=5432;Username=postgres;Password=123456;Pooling=true;MaxPoolSize=1024;", database);
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
      
        private void InitMenuTree()
        {
            treeView.Nodes.Clear();
            dbTrees = GetDBDocuments(getPgDatabaseSql);
            UpdateMenuTree(dbTrees);
        }

        private void UpdateMenuTree(List<DbName> dbnames)
        {
            treeView.Nodes.Clear();
            foreach (var item in dbnames)
            {
                if (!item.DName.Contains("template"))
                {
                    TreeNode mytreenode = new TreeNode();
                    mytreenode.Text = item.DName;
                    if (item.IsSelected)
                        mytreenode.BackColor = Color.Blue;
                    else
                        mytreenode.BackColor = treeView.BackColor;
                    foreach (var itemchild in item.TName)
                    {
                        TreeNode childnode = new TreeNode();
                        childnode.Text = itemchild.TName;
                        mytreenode.Nodes.Add(childnode);
                    }

                    treeView.Nodes.Add(mytreenode);
                }
            }
        }
        
        private void treeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            UpdateSelectedTreeDetail();
        }

        private void UpdateSelectedTreeDetail()
        {
            if (treeView.SelectedNode != null)
            {
                if (treeView.SelectedNode.Level == 0)
                {
                    DataName = treeView.SelectedNode.Text;
                    TableName = null;
                    updateTable();
                }
                if (treeView.SelectedNode.Level == 1)
                {
                    detailDataTable = GetDetailDocuments(treeView.SelectedNode.Text, treeView.SelectedNode.Parent.Text);
                    DataName = treeView.SelectedNode.Parent.Text;
                    TableName = treeView.SelectedNode.Text;
                    updateTable();
                }
            }
        }

        private void databaseBox_TextChanged(object sender, EventArgs e)
        {            
            if (!String.IsNullOrEmpty(databaseBox.Text))
            {
                try
                {
                    DbName[] dbTemp = new DbName[dbTrees.Count];
                    dbTrees.CopyTo(dbTemp);
                    foreach (var item in dbTemp)
                    {
                        if (item.DName.ToLower().Contains(databaseBox.Text.ToLower()))
                        {
                            item.IsSelected = true;
                        }
                        else
                            item.IsSelected = false;
                    }

                    UpdateMenuTree(dbTemp.ToList());
                }
                catch (Exception ex)
                {
                    logger.Log(LogLevel.Error, ex.Message);
                }
            }
            else
                InitMenuTree();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshDatabase();
            if (!(string.IsNullOrEmpty(TableName) || string.IsNullOrEmpty(DataName)))
            {
                detailDataTable = GetDetailDocuments(TableName, DataName);
                updateTable();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            RefreshDatabase();
        }

        private void RefreshDatabase()
        {
            if (this.checkBox1.Checked == true)
            {
                treeView.Nodes.Clear();
                var hideDbs = ConfigurationManager.AppSettings["hideDbs"].Split(',');

                StringBuilder sbSql = new StringBuilder(@"SELECT datname FROM pg_database ");
                if (hideDbs.Count() > 0)
                {
                    sbSql.Append("Where");
                    bool isNotFirstDb = false;
                    foreach (var dbName in hideDbs)
                    {
                        if (isNotFirstDb)
                        {
                            sbSql.Append(string.Format(" And lower(datname) not Like '{0}%'", dbName.ToLower()));
                        }
                        else
                        {
                            sbSql.Append(string.Format(" lower(datname) not Like '{0}%'", dbName.ToLower()));
                            isNotFirstDb = true;
                        }
                    }
                }
                sbSql.Append(" order by lower(datname)");

                dbTrees = GetDBDocuments(sbSql.ToString());
                UpdateMenuTree(dbTrees);

            }
            else
            {
                InitMenuTree();
            }
        }

        private readonly string getPgDatabaseSql = @"SELECT datname FROM pg_database order by lower(datname) ";
    }
}
