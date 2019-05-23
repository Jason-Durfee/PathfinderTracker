using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class WeaponCategory : NamedObject
    {
        #region Constructors
        public WeaponCategory(SqlDataReader dr) {
            ID = (int)dr["WeaponCategoryID"];
            Name = (string)dr["Name"];
        }

        public WeaponCategory() {

        }
        #endregion
    }
}
