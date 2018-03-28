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
        DBClass postgresHelper = new DBClass();
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
        private List<DBDocuments> dbDocuments;
        public List<DBDocuments> DBDocuments
        {
            get
            {
                return dbDocuments;
            }
            set
            {
                dbDocuments = value;
                //InitMenuTree(dbDocuments);
            }
        }

        public string DataName { get; set; }
        public string TableName { get; set;}


        private DataSet GetDBDocuments(string sql, string database)
        {
            var dbConnStr = " Host=localhost;Database=" + database + ";Port=5432;Username=postgres;Password=123456;Pooling=true;MaxPoolSize=1024;";
            var dataSet = postgresHelper.ExecuteQuery(dbConnStr, System.Data.CommandType.Text, sql);
            return dataSet;
        }
        private DataTable GetDetailDocuments(string tablename, string database)
        {
            var sql = @"SELECT * FROM " + tablename;
            var dbConnStr = " Host=localhost;Database=" + database + ";Port=5432;Username=postgres;Password=123456;Pooling=true;MaxPoolSize=1024;";
            var dataTable = postgresHelper.ExecuteReader(dbConnStr, System.Data.CommandType.Text, sql).GetSchemaTable();
            return dataTable;
        }


        private void InitMenuTree()//(List<DBDocuments> dbDocuments)
        {
            treeView.Nodes.Clear();
            var ds = GetDBDocuments(@"SELECT datname FROM pg_database", "postgres");
            foreach (DataRow col in ds.Tables[0].Rows)
            {
                TreeNode mytreenode = new TreeNode();
                mytreenode.Name = col["datname"].ToString();
                mytreenode.Text = col["datname"].ToString();
                if (!mytreenode.Text.Contains("template"))
                {
                    var dschild = GetDBDocuments(@"SELECT tablename FROM pg_tables", mytreenode.Text);
                    if (dschild != null)
                    {
                        foreach (DataRow colchild in dschild.Tables[0].Rows)
                        {
                            TreeNode childnode = new TreeNode();
                            childnode.Name = colchild["tablename"].ToString();
                            childnode.Text = colchild["tablename"].ToString();
                            mytreenode.Nodes.Add(childnode);
                        }
                    }
                }
                treeView.Nodes.Add(mytreenode);
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
