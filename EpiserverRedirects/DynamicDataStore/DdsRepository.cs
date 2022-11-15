using Forte.EpiserverRedirects.Model.RedirectRule;
using Forte.EpiserverRedirects.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace Forte.EpiserverRedirects.DynamicDataStore
{
    public class DdsRepository : IRedirectRuleRepository
    {
        private readonly IDynamicDataStore<DdsRedirectRule> _ruleStore;
        private readonly IDdsRedirectRuleMapper _mapper;

        public DdsRepository(
            IDynamicDataStore<DdsRedirectRule> ruleStore,
            IDdsRedirectRuleMapper mapper)
        {
            _ruleStore = ruleStore;
            _mapper = mapper;
        }

        public RedirectRuleModel GetById(Guid id)
        {
            var entity = _ruleStore.GetById(id);
            return _mapper.MapToModel(entity);
        }

        public IList<RedirectRuleModel> GetAll()
        {
            return _ruleStore.Items()
                .Select(entity => _mapper.MapToModel(entity))
                .ToList();
        }

        public IList<RedirectRuleModel> Query(out int allRedirectsCount, RedirectRuleQuery query)
        {
            return _ruleStore.Items()
                .ApplyQuery(out allRedirectsCount, query)
                .Select(entity => _mapper.MapToModel(entity))
                .ToList();
        }

        public IList<RedirectRuleModel> GetByContent(IList<int> contentIds)
        {
            return _ruleStore.Items()
                .Where(x => x.ContentId.HasValue && contentIds.Contains(x.ContentId.Value))
                .Select(entity => _mapper.MapToModel(entity))
                .ToList();
        }

        public RedirectRuleModel FindRegexMatch(string patern)
        {
            var entity = _ruleStore.Items()
                .Where(r => r.IsActive && r.RedirectRuleType == RedirectRuleType.Regex)
                .OrderBy(x => x.Priority)
                .AsEnumerable()
                .FirstOrDefault(r => Regex.IsMatch(patern, r.OldPattern, RegexOptions.IgnoreCase));
            return _mapper.MapToModel(entity);
        }

        public RedirectRuleModel FindExactMatch(string patern)
        {
            var entity = _ruleStore.Items()
                .Where(r => r.IsActive && r.RedirectRuleType == RedirectRuleType.ExactMatch)
                .OrderBy(x => x.Priority)
                .FirstOrDefault(r => r.OldPattern == patern);
            return _mapper.MapToModel(entity);
        }

        public RedirectRuleModel Add(RedirectRuleModel redirectRule)
        {
            var entity = _mapper.MapForSave(redirectRule);
            _ruleStore.Save(entity);
            return _mapper.MapToModel(entity);
        }

        public RedirectRuleModel Update(RedirectRuleModel redirectRule)
        {
            var entity = _ruleStore.GetById(redirectRule.Id);
            if (entity == null)
            {
                throw new InvalidOperationException("No existing redirect with this GUID");
            }

            _mapper.MapForUpdate(redirectRule, entity);
            _ruleStore.Save(entity);
            return _mapper.MapToModel(entity);
        }

        public bool Delete(Guid id)
        {
            try
            {
                _ruleStore.Delete(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ClearAll()
        {
            try
            {
                _ruleStore.DeleteAll();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
