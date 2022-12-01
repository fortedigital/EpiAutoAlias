using System;
using System.Linq;
using System.Net;
using EPiServer.Shell.Services.Rest;
using Forte.EpiserverRedirects.Mapper;
using Forte.EpiserverRedirects.Model.RedirectRule;
using Forte.EpiserverRedirects.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Forte.EpiserverRedirects.Menu
{
    [RestStore("RedirectRuleStore")]
    public class RedirectRuleStore : RestControllerBase
    {
        private readonly IRedirectRuleRepository _redirectRuleRepository;
        private readonly IRedirectRuleModelMapper _redirectRuleMapper;
        private readonly Guid _clearAllGuid = Guid.Parse("00000000-0000-0000-0000-000000000000");

        public RedirectRuleStore(IRedirectRuleRepository redirectRuleRepository, IRedirectRuleModelMapper redirectRuleMapper)
        {
            _redirectRuleRepository = redirectRuleRepository;
            _redirectRuleMapper = redirectRuleMapper;
        }

        public ActionResult Get(Guid id)
        {
            var redirect = _redirectRuleRepository.GetById(id);

            return redirect == null ? null : Rest(_redirectRuleMapper.ModelToDto(redirect));
        }

        [HttpGet]
        public ActionResult Get(Query query = null)
        {
            var redirects = _redirectRuleRepository
                .GetAll()
                .GetPageFromQuery(out var allRedirectsCount, query)
                .Select(_redirectRuleMapper.ModelToDto);

            var itemRange = new ItemRange
            {
                Total = allRedirectsCount
            };

            itemRange.AddHeaderTo(HttpContext.Response);
            return Rest(redirects);
        }

        [HttpPost]
        public ActionResult Post(RedirectRuleDto dto)
        {
            if (!ViewData.ModelState.IsValid)
            {
                return null;
            }

            var newRedirectRule = _redirectRuleMapper.DtoToModel(dto);
            RedirectRuleModel.FromManual(newRedirectRule);
            newRedirectRule = _redirectRuleRepository.Add(newRedirectRule);
            var newRedirectRuleDto = _redirectRuleMapper.ModelToDto(newRedirectRule);

            return Rest(newRedirectRuleDto);
        }

        [HttpPut]
        public ActionResult Put(RedirectRuleDto dto)
        {
            if (!ViewData.ModelState.IsValid)
            {
                return null;
            }

            var updatedRedirectRule = _redirectRuleMapper.DtoToModel(dto);
            updatedRedirectRule = _redirectRuleRepository.Update(updatedRedirectRule);
            var updatedRedirectRuleDto = _redirectRuleMapper.ModelToDto(updatedRedirectRule);

            return Rest(updatedRedirectRuleDto);
        }

        [HttpDelete]
        public ActionResult Delete(Guid id)
        {
            var deletedSuccessfully = id == _clearAllGuid
                ? _redirectRuleRepository.ClearAll()
                : _redirectRuleRepository.Delete(id);

            return deletedSuccessfully
                ? Rest(HttpStatusCode.OK)
                : Rest(HttpStatusCode.Conflict);
        }
    }
}
