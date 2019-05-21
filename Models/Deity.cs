using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Deity : NamedObject
    {
        #region Constructors
        public Deity(SqlDataReader dr) {
            ID = (int)dr["DeityID"];
            Name = (string)dr["Name"];
            AlignmentID = (int)dr["AlignmentID"];
            Description = (string)dr["Description"];
        }

        public Deity() {

        }
        #endregion

        private int _AlignmentID;
        private Alignment _Alignment;
        private string _Description;

        /// <summary>
        /// gets and sets the AlignmentID attribute for the Deity object
        /// </summary>
        public int AlignmentID {
            get {
                return _AlignmentID;
            }
            set {
                _AlignmentID = value;
            }
        }

        /// <summary>
        /// gets and sets the Alignment attribute for the Deity object
        /// </summary>
        public Alignment Alignment {
            get {
                if(_Alignment == null && _AlignmentID > 0) {
                    _Alignment = DAL.GetAlignment(_AlignmentID);
                }
                return _Alignment;
            }
            set {
                _Alignment = value;
            }
        }

        /// <summary>
        /// gets and sets the Description attribute for the Deity object
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
