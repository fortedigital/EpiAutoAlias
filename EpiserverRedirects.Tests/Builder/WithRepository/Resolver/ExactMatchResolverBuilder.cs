using EPiServer;
using Forte.EpiserverRedirects.Resolver;
using Moq;

namespace Forte.EpiserverRedirects.Tests.Builder.WithRepository.Resolver
{
    public class ExactMatchResolverBuilder : BaseWithRepositoryBuilder<ExactMatchResolver, ExactMatchResolverBuilder>
    {
        protected override ExactMatchResolverBuilder ThisBuilder => this;

        public override ExactMatchResolver Create()
        {
            CreateRepository();
            return new ExactMatchResolver(RedirectRuleRepository, Mock.Of<IContentLoader>());
        }
    }
}
