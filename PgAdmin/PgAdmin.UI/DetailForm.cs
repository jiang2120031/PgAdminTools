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

    }
}
