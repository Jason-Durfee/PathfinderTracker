using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class WeaponSubType : NamedObject
    {
        #region Constructors
        public WeaponSubType(SqlDataReader dr) {
            ID = (int)dr["WeaponSubTypeID"];
            Name = (string)dr["Name"];
        }

        public WeaponSubType() {

        }
        #endregion
    }
}
