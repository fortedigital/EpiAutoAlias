﻿using System;
using System.Linq;
using Forte.EpiserverRedirects.Model.RedirectRule;
using Forte.EpiserverRedirects.Repository;

namespace Forte.EpiserverRedirects.Caching
{
    public class RedirectRuleCachedRepositoryDecorator : IRedirectRuleRepository
    {
        public const string CacheKey = "Forte.EpiserverRedirects.RedirectRuleList";
        private readonly ICache _cache;
        private readonly IRedirectRuleRepository _redirectRuleRepository;
        private static readonly object Locker = new object();
        
        public RedirectRuleCachedRepositoryDecorator(IRedirectRuleRepository redirectRuleRepository, ICache cache)
        {
            _cache = cache;
            _redirectRuleRepository = redirectRuleRepository;
        }

        public RedirectRule GetById(Guid id) => _redirectRuleRepository.GetById(id);

        public IQueryable<RedirectRule> GetAll()
        {
            if (_cache.TryGet<RedirectRule[]>(CacheKey, out var redirectRulesFirstAttempt))
            {
                return redirectRulesFirstAttempt.AsQueryable();
            }

            lock(Locker)
            {
                if (_cache.TryGet<RedirectRule[]>(CacheKey, out var redirectRules))
                {
                    return redirectRules.AsQueryable();
                }
                
                redirectRules = _redirectRuleRepository.GetAll().ToArray();
                _cache.Add(CacheKey, redirectRules);
                
                return redirectRules.AsQueryable();
            }
        }

        public RedirectRule Add(RedirectRule redirectRule) => _redirectRuleRepository.Add(redirectRule);

        public RedirectRule Update(RedirectRule redirectRule) => _redirectRuleRepository.Update(redirectRule);

        public bool Delete(Guid id) => _redirectRuleRepository.Delete(id);

        public bool ClearAll() => _redirectRuleRepository.ClearAll();
    }
}
