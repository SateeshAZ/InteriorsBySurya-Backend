using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMetaCore.models
{
    public class Survey
    {
        public string SurveyID { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public string Status { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; } = DateTime.Now;
        public bool IsLive { get; set; } = false;
        public bool IsPaused { get; set; } = false;
        public bool IsAbandoned { get; set; } = false;
        public bool IsArchived { get; set; } = false;
        public string StatusReason { get; set; } = string.Empty;
        public int Version { get; set; } = 0;
        public bool IsAnonymous { get; set; } = false;
        public string TargetAudience { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public int MaxResponses { get; set; } = 0;
        public int QuotaLimit { get; set; } = 0;
        public string QuotaCriteria { get; set; } = string.Empty;
        public int CurrentQuotaCount { get; set; } = 0;
        public bool IsQuotaFulfilled { get; set; } = false;
        public string LogoURL { get; set; } = string.Empty;
        public string CustomCSS { get; set; } = string.Empty;
        public string WelcomeMessage { get; set; } = string.Empty;
        public string ThankYouMessage { get; set; } = string.Empty;
        public string AccessCode { get; set; } = string.Empty;
        public List<string> IPRestrictions { get; set; } = [];
        public bool PasswordProtection { get; set; } = false;
        public bool EncryptionEnabled { get; set; } = false;
        public List<string> SupportedLanguages { get; set; } = [];
        public string DefaultLanguage { get; set; } = string.Empty;
        public bool LanguageSelectorEnabled { get; set; } = false;
        public bool HasIncentives { get; set; } = false;
        public string IncentiveType { get; set; } = string.Empty;
        public double IncentiveValue { get; set; } = 0;
        public string Theme { get; set; } = string.Empty;
        public bool AllowMultipleAttempts { get; set; } = false;
        public bool ShowProgressBar { get; set; } = false;
        public int TotalResponses { get; set; } = 0;
        public double AvgCompletionTime { get; set; } = 0;
        public string CreatorID { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = [];
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime LastModified { get; set; } = DateTime.Now;
    }

}
