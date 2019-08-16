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
        private static int _CurrentWeatherTypeID;
        private static WeatherType _CurrentWeatherType;
        private static int _TimeSinceWeatherChange;

        public static int CurrentCampaignID {
            get {
                return _CurrentCampaignID;
            }
            set {
                _CurrentCampaignID = value;
            }
        }

        public static int CurrentWeatherTypeID {
            get {
                return _CurrentWeatherTypeID;
            }
            set {
                _CurrentWeatherTypeID = value;
            }
        }

        public static int TimeSinceWeatherChange {
            get {
                return _TimeSinceWeatherChange;
            }
            set {
                _TimeSinceWeatherChange = value;
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

        public static WeatherType CurrentWeatherType {
            get {
                if(_CurrentWeatherType == null && _CurrentWeatherTypeID > 0) {
                    _CurrentWeatherType = DAL.GetWeatherType(_CurrentWeatherTypeID);
                }
                if(_CurrentWeatherType != null) {
                    return _CurrentWeatherType;
                }
                WeatherType blankWeatherType = new WeatherType();
                blankWeatherType.Name = "";
                blankWeatherType.ID = -1;
                return blankWeatherType;
            }
            set {
                _CurrentWeatherType = value;
            }
        }

    }
}
