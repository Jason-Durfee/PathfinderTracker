using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class DatabaseObject
    {
        private int _ID;

        /// <summary>
        /// gets and sets the ID for the NamedObject object
        /// </summary>
        public int ID {
            get {
                return _ID;
            }
            set {
                _ID = value;
            }
        }
    }
}
