using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class WeatherType : NamedObject
    {
        private int _MinSelector;
        private int _MaxSelector;

        /// <summary>
        /// determines the min number needed on a percentile check to select that weather
        /// </summary>
        public int MinSelector {
            get {
                return _MinSelector;
            }
            set {
                _MinSelector = value;
            }
        }

        /// <summary>
        /// determines the max number needed on a percentile check to select that weather
        /// </summary>
        public int MaxSelector {
            get {
                return _MaxSelector;
            }
            set {
                _MaxSelector = value;
            }
        }


        #region Constructors
        public WeatherType(SqlDataReader dr) {
            ID = (int)dr["WeatherTypeID"];
            Name = (string)dr["Name"];
            MinSelector = (int)dr["MinSelector"];
            MaxSelector = (int)dr["MaxSelector"];
        }

        public WeatherType() {

        }
        #endregion
    }
}
