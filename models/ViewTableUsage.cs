using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ViewTableUsage
    {
        public string VIEW_CATALOG { get; set; }
        public string VIEW_SCHEMA { get; set; }
        public string VIEW_NAME { get; set; }
        public string TABLE_CATALOG { get; set; }
        public string TABLE_SCHEMA { get; set; }
        public string TABLE_NAME { get; set; }

    }
}
