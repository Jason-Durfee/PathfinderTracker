using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class Alignment : NamedObject
    {
        #region constructors
        public Alignment(SqlDataReader dr) {
            ID = (int)dr["AlignmentID"];
            Name = (string)dr["Name"];
            Abbreviation = (string)dr["Abbreviation"];
            Description = (string)dr["Description"];
        }

        public Alignment() {

        }
        #endregion

        private string _Abbreviation;
        private string _Description;

        /// <summary>
        /// gets and sets the abbreviation for the Alignment object
        /// </summary>
        public string Abbreviation {
            get {
                return _Abbreviation;
            }
            set {
                _Abbreviation = value;
            }
        }

        /// <summary>
        /// gets and sets the Description for the Alignment object
        /// </summary>
        public string Description {
            get {
                return _Description;
            }
            set {
                _Description = value;
            }
        }
    }
}
