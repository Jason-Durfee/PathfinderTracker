using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public static class CurrentVariables
    {
        private static int _CurrentCampaignID;
        private static Campaign _CurrentCampaign;

        public static int CurrentCampaignID {
            get {
                return _CurrentCampaignID;
            }
            set {
                _CurrentCampaignID = value;
            }
        }

        public static Campaign CurrentCampaign {
            get {
                if(_CurrentCampaign == null && _CurrentCampaignID > 0) {
                    _CurrentCampaign = DAL.GetCampaign(_CurrentCampaignID);
                }
                if(_CurrentCampaign != null) {
                    return _CurrentCampaign;
                }
                Campaign blankCampaign = new Campaign();
                blankCampaign.Name = "";
                blankCampaign.ID = -1;
                return blankCampaign;
            }
            set {
                _CurrentCampaign = value;
            }
        }

    }
}
