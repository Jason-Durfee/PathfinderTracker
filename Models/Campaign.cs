using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Campaign : NamedObject
    {
        #region Constructors
        public Campaign(SqlDataReader dr) {
            ID = (int)dr["CampaignID"];
            Name = (string)dr["Name"];
        }

        public Campaign() {

        }
        #endregion
    }
}
