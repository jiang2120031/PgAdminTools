using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgAdmin.Services.Model
{
    public class DbName
    {
        public DbName()
        {
            TName = new List<TableName>();
        }

        public string DName { get; set; }

        public IList<TableName> TName { get; set; }
    }
}
