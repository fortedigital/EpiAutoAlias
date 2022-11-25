﻿using Forte.EpiserverRedirects.Model.RedirectRule;
using System;

namespace Forte.EpiserverRedirects.EntityFramework.Model
{
    public class RedirectRuleEntity : IRedirectRule
    {
        public Guid RuleId { get; set; }
        public int? ContentId { get; set; }
        public string OldPattern { get; set; }
        public string NewPattern { get; set; }
        public RedirectRuleType RedirectRuleType { get; set; }
        public RedirectType RedirectType { get; set; }
        public RedirectOrigin RedirectOrigin { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string Notes { get; set; }
        public int Priority { get; set; }
    }
}
