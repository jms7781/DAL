using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Routine
    {
        public string SPECIFIC_CATALOG { get; set; }
        public string SPECIFIC_SCHEMA { get; set; }
        public string SPECIFIC_NAME { get; set; }
        public string ROUTINE_CATALOG { get; set; }
        public string ROUTINE_SCHEMA { get; set; }
        public string ROUTINE_NAME { get; set; }
        public string ROUTINE_TYPE { get; set; }
        public string MODULE_CATALOG { get; set; }
        public string MODULE_SCHEMA { get; set; }
        public string MODULE_NAME { get; set; }
        public string UDT_CATALOG { get; set; }
        public string UDT_SCHEMA { get; set; }
        public string UDT_NAME { get; set; }
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
        public string TYPE_UDT_CATALOG { get; set; }
        public string TYPE_UDT_SCHEMA { get; set; }
        public string TYPE_UDT_NAME { get; set; }
        public string SCOPE_CATALOG { get; set; }
        public string SCOPE_SCHEMA { get; set; }
        public string SCOPE_NAME { get; set; }
        public string MAXIMUM_CARDINALITY { get; set; }
        public string DTD_IDENTIFIER { get; set; }
        public string ROUTINE_BODY { get; set; }
        public string ROUTINE_DEFINITION { get; set; }
        public string EXTERNAL_NAME { get; set; }
        public string EXTERNAL_LANGUAGE { get; set; }
        public string PARAMETER_STYLE { get; set; }
        public string IS_DETERMINISTIC { get; set; }
        public string SQL_DATA_ACCESS { get; set; }
        public string IS_NULL_CALL { get; set; }
        public string SQL_PATH { get; set; }
        public string SCHEMA_LEVEL_ROUTINE { get; set; }
        public string MAX_DYNAMIC_RESULT_SETS { get; set; }
        public string IS_USER_DEFINED_CAST { get; set; }
        public string IS_IMPLICITLY_INVOCABLE { get; set; }
        public string CREATED { get; set; }
        public string LAST_ALTERED { get; set; }

    }
}
