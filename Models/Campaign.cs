using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class Campaign : NamedObject
    {
        private DateTime _CurrentTime;

        public DateTime CurrentTime {
            get {
                return _CurrentTime;
            }
            set {
                _CurrentTime = value;
            }
        }

        #region Constructors
        public Campaign(SqlDataReader dr) {
            ID = (int)dr["CampaignID"];
            Name = (string)dr["Name"];
            _CurrentTime = (DateTime)dr["CurrentTime"];
        }

        public Campaign() {

        }
        #endregion
    }
}
