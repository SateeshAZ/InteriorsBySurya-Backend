using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Survey.Models
{
    public class SurveyModel
    {
        [JsonProperty("id")]
        public string SurveyId { get; set; }
        public string ClientId { get; set; }   // Partition Key

        public string SurveyType { get; set; }  // MarketResearch, EmployeeEngagement, etc.
        public string Status { get; set; }  // Draft, Active, Closed, Archived
        public int QuotaLimit { get; set; }  // Max responses allowed
        public int Version { get; set; }  // Version tracking
        public bool IsLocked { get; set; }  // Lock/unlock survey
        public DateTime CreatedAt { get; set; }  // Timestamp for tracking versions
        public UserInfoModel? CreatedBy { get; set; }  // Who created the survey
        public UserInfoModel? LastUpdatedBy { get; set; }  // Last modified by
        public List<UserActivityModel>? Contributors { get; set; } = new();  // History of contributors
        public List<string>? AuthorizedUsers { get; set; } = new(); // Users allowed to lock/unlock
        public List<PageModel> Pages { get; set; } = new();
    }

}
