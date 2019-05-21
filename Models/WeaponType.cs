using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class WeaponType : NamedObject
    {
        #region Constructors
        public WeaponType(SqlDataReader dr) {
            ID = (int)dr["WeaponTypeID"];
            Name = (string)dr["Name"];
        }

        public WeaponType() {

        }
        #endregion
    }
}
