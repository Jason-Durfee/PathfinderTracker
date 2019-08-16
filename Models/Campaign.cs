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
        private string _Notes;

        //gets the number of days since the start of the campaign
        [Display(Name = "Days Played")]
        public int DaysPlayed {
            get {
                int years = CurrentTime.Year - OriginalStartDate.Year;
                int offset = years * 365;
                return CurrentTime.DayOfYear - OriginalStartDate.DayOfYear + offset;
            }
        }

        //gets and sets the current time of the campaign
        [Display(Name = "Current Time")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime CurrentTime {
            get {
                return _CurrentTime;
            }
            set {
                _CurrentTime = value;
            }
        }

        //gets and sets the original start date of the campaign
        [Display(Name = "Original Start Date")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime OriginalStartDate {
            get {
                return _OriginalStartDate;
            }
            set {
                _OriginalStartDate = value;
            }
        }

        //gets and sets the notes for the campaign
        public string Notes {
            get {
                return _Notes;
            }
            set {
                _Notes = value;
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
            Notes = (string)dr["Notes"];
        }

        public Campaign() {

        }
        #endregion

        public void AddHours(int hours) {
            CurrentTime = CurrentTime.AddHours(hours);
            DAL.UpdateCampaign(this, this.ID);
        }
    }
}
