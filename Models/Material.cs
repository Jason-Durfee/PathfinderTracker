using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class Material : NamedObject
    {
        #region Constructors
        public Material(SqlDataReader dr) {
            ID = (int)dr["MaterialID"];
            Name = (string)dr["Name"];
        }

        public Material() {

        }
        #endregion
    }
}
