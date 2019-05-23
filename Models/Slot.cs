using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class Slot : NamedObject
    {
        #region Constructors
        public Slot(SqlDataReader dr) {
            ID = (int)dr["SlotID"];
            Name = (string)dr["Name"];
        }

        public Slot() {

        }
        #endregion
    }
}
