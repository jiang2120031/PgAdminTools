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
        public string TableName { get; set;}


        private List<DbName> GetDBDocuments(string sql, string database)
        {
            var dbConnStr = " Host=localhost;Database=" + database +
                ";Port=5432;Username=postgres;Password=123456;Pooling=true;MaxPoolSize=1024;";
            var dbList = findDocuments.FindDbAndTables(dbConnStr, System.Data.CommandType.Text, sql).ToList();
            return dbList;
        }

        private DataTable GetDetailDocuments(string tablename, string database)
        {
            var sql = @"SELECT * FROM " + tablename;
            var dbConnStr = " Host=localhost;Database=" + database +
                ";Port=5432;Username=postgres;Password=123456;Pooling=true;MaxPoolSize=1024;";
            var dataTable = new DataTable();
            dataTable.Load(postgresHelper.ExecuteReader(dbConnStr, System.Data.CommandType.Text, sql));
            return dataTable;
        }
        
        private void InitMenuTree()
        {
            treeView.Nodes.Clear();
            var ds = GetDBDocuments(@"SELECT datname FROM pg_database", "postgres");
            foreach (var item in ds)
            {
                if (!item.DName.Contains("template"))
                {
                    TreeNode mytreenode = new TreeNode();
                    mytreenode.Text = item.DName;

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
            if (treeView.SelectedNode != null && treeView.SelectedNode.Level == 1)
            {
                detailDataTable = GetDetailDocuments(treeView.SelectedNode.Text, treeView.SelectedNode.Parent.Text);
                DataName = treeView.SelectedNode.Parent.Text;
                TableName = treeView.SelectedNode.Text;
                updateTable();
            }

        }


    }
}
