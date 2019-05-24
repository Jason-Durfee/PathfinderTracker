using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class Condition : NamedObject
    {
        #region Constructors
        public Condition(SqlDataReader dr) {
            ID = (int)dr["ConditionID"];
            Name = (string)dr["Name"];
            Effect = (string)dr["Effect"];
        }

        public Condition() {

        }
        #endregion

        private string _Effect;

        /// <summary>
        /// gets and sets the Effect attribute for the Condition object
        /// </summary>
        [Required]
        public string Effect {
            get {
                return _Effect;
            }
            set {
                _Effect = value;
            }
        }

    }
}
