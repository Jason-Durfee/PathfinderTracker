using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class Player : NamedObject
    {
        #region Constructors
        public Player(SqlDataReader dr) {
            ID = (int)dr["PlayerID"];
            Name = (string)dr["Name"];
        }

        public Player() {

        }
        #endregion
    }
}
