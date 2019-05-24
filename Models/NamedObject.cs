using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class NamedObject : DatabaseObject
    {
        private string _Name;

        /// <summary>
        /// gets and sets the Name for the NamedObject object
        /// </summary>
        [Required]
        public string Name {
            get {
                return _Name;
            }
            set {
                _Name = value;
            }
        }
    }
}
