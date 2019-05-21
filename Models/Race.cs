using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Race : NamedObject
    {
        #region Constructors
        public Race(SqlDataReader dr) {
            ID = (int)dr["RaceID"];
            Name = (string)dr["Name"];
        }

        public Race() {

        }
        #endregion
    }
}
