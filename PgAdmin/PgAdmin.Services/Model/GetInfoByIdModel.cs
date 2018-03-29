using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgAdmin.Services.Model
{
    public class GetInfoByIdModel
    {
        public string Id { get; set; }
        public string  Data { get; set; }
        public DateTimeOffset MtLastModified { get; set; }
    }
}
