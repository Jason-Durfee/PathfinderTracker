using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DamageType : NamedObject
    {
        #region Constructors
        public DamageType(SqlDataReader dr) {
            ID = (int)dr["DamageTypeID"];
            Name = (string)dr["Name"];
        }

        public DamageType() {

        }
        #endregion
    }
}
