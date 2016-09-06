using SQLite;
using System;

namespace Connect.Conference.Mobile.Models
{
    [Table("Conferences")]
    public class Conference
    {
        public int ConferenceId { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Location { get; set; }
        public string TimeZoneId { get; set; }
        public int? NrSessions { get; set; }
        public string Host { get; set; }
        public string Scheme { get; set; }
        public int TabId { get; set; }
        public int ModuleId { get; set; }
        public string Username { get; set; }
        public DateTime LastSync { get; set; }
    }
}
