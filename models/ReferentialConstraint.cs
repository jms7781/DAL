using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ReferentialConstraint
    {
        public string CONSTRAINT_CATALOG { get; set; }
        public string CONSTRAINT_SCHEMA { get; set; }
        public string CONSTRAINT_NAME { get; set; }
        public string UNIQUE_CONSTRAINT_CATALOG { get; set; }
        public string UNIQUE_CONSTRAINT_SCHEMA { get; set; }
        public string UNIQUE_CONSTRAINT_NAME { get; set; }
        public string MATCH_OPTION { get; set; }
        public string UPDATE_RULE { get; set; }
        public string DELETE_RULE { get; set; }

    }
}
