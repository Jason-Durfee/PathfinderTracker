using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Class : NamedObject
    {
        #region Constructors
        public Class(SqlDataReader dr) {
            ID = (int)dr["ClassID"];
            Name = (string)dr["Name"];
            SubClassID = (int)dr["SubClassID"];
        }

        public Class() {

        }
        #endregion

        private int _SubClassID;
        private SubClass _SubClass;

        /// <summary>
        /// gets and sets the SubClass objects SubClassID attribute
        /// </summary>
        public int SubClassID {
            get {
                return _SubClassID;
            }
            set {
                _SubClassID = value;
            }
        }

        /// <summary>
        /// gets and sets the SubClass attribute for the Armor object
        /// </summary>
        public SubClass SubClass {
            get {
                if(_SubClass == null && _SubClassID > 0) {
                    _SubClass = DAL.GetSubClass(_SubClassID);
                }
                return _SubClass;
            }
            set {
                _SubClass = value;
            }
        }
    }
}
