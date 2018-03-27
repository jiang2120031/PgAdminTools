using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgAdmin.UI
{
    public class DBDocuments
    {
        public string Title { get; set; }
        public string Link { get; set; }

        public List<DataTables> tables { get; set; }
    }
}
