using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class View
    {
        public string TABLE_CATALOG { get; set; }
        public string TABLE_SCHEMA { get; set; }
        public string TABLE_NAME { get; set; }
        public string VIEW_DEFINITION { get; set; }
        public string CHECK_OPTION { get; set; }
        public string IS_UPDATABLE { get; set; }

    }
}
