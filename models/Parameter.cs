using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Parameter
    {
        public string SPECIFIC_CATALOG { get; set; }
        public string SPECIFIC_SCHEMA { get; set; }
        public string SPECIFIC_NAME { get; set; }
        public string ORDINAL_POSITION { get; set; }
        public string PARAMETER_MODE { get; set; }
        public string IS_RESULT { get; set; }
        public string AS_LOCATOR { get; set; }
        public string PARAMETER_NAME { get; set; }
        public string DATA_TYPE { get; set; }
        public string CHARACTER_MAXIMUM_LENGTH { get; set; }
        public string CHARACTER_OCTET_LENGTH { get; set; }
        public string COLLATION_CATALOG { get; set; }
        public string COLLATION_SCHEMA { get; set; }
        public string COLLATION_NAME { get; set; }
        public string CHARACTER_SET_CATALOG { get; set; }
        public string CHARACTER_SET_SCHEMA { get; set; }
        public string CHARACTER_SET_NAME { get; set; }
        public string NUMERIC_PRECISION { get; set; }
        public string NUMERIC_PRECISION_RADIX { get; set; }
        public string NUMERIC_SCALE { get; set; }
        public string DATETIME_PRECISION { get; set; }
        public string INTERVAL_TYPE { get; set; }
        public string INTERVAL_PRECISION { get; set; }
        public string USER_DEFINED_TYPE_CATALOG { get; set; }
        public string USER_DEFINED_TYPE_SCHEMA { get; set; }
        public string USER_DEFINED_TYPE_NAME { get; set; }
        public string SCOPE_CATALOG { get; set; }
        public string SCOPE_SCHEMA { get; set; }
        public string SCOPE_NAME { get; set; }

    }
}
