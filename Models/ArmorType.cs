using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ArmorType : NamedObject
    {
        #region Constructors
        public ArmorType(SqlDataReader dr) {
            ID = (int)dr["ArmorTypeID"];
            Name = (string)dr["Name"];
        }

        public ArmorType() {

        }
        #endregion
    }
}
