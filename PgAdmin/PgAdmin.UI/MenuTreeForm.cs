﻿using System;
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
            { var sql = @"SELECT * FROM " + tablename;
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
            var ds = GetDBDocuments(@"SELECT datname FROM pg_database order by datname ");
            UpdateMenuTree(ds);
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

        private void searchButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(databaseBox.Text))
            {
                try
                {
                    var sql = string.Format(@"SELECT u.datname  FROM pg_catalog.pg_database u where u.datname='"+databaseBox.Text+"';");
                    var ds = GetDBDocuments(sql);
                    UpdateMenuTree(ds);

                }
                catch (Exception ex)
                {
                    logger.Log(LogLevel.Error, ex.Message);
                }
            }
            else
                InitMenuTree();
        }

    }
}
