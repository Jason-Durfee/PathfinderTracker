using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderTracker.Models
{
    public class Campaign : NamedObject
    {
        private DateTime _CurrentTime;
        private DateTime _OriginalStartDate;

        //gets the number of days since the start of the campaign
        [Display(Name = "Days Played")]
        public int DaysPlayed {
            get {
                int years = CurrentTime.Year - OriginalStartDate.Year;
                int offset = years * 365;
                return CurrentTime.DayOfYear - OriginalStartDate.DayOfYear + offset;
            }
        }


        [Display(Name = "Current Time")]
        public DateTime CurrentTime {
            get {
                return _CurrentTime;
            }
            set {
                _CurrentTime = value;
            }
        }

        [Display(Name = "Original Start Date")]
        public DateTime OriginalStartDate {
            get {
                return _OriginalStartDate;
            }
            set {
                _OriginalStartDate = value;
            }
        }

        /// <summary>
        /// gets the current time of day displayed similar to 9:30 PM
        /// </summary>
        public string CurrentTimeDisplay {
            get {
                return CurrentTime.ToString("h:mm tt");
            }
        }

        #region Constructors
        public Campaign(SqlDataReader dr) {
            ID = (int)dr["CampaignID"];
            Name = (string)dr["Name"];
            CurrentTime = (DateTime)dr["CurrentTime"];
            OriginalStartDate = (DateTime)dr["OriginalStartDate"];
        }

        public Campaign() {

        }
        #endregion

        public void Add24Hours() {
            CurrentTime = CurrentTime.AddHours(24);
            DAL.UpdateCampaign(this, this.ID);
        }

        public void AddHours(int hours) {
            CurrentTime = CurrentTime.AddHours(hours);
            DAL.UpdateCampaign(this, this.ID);
        }
    }
}
