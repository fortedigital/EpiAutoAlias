using System;
using System.Collections.Generic;
using System.Linq;
using Forte.EpiserverRedirects.Model;
using Forte.EpiserverRedirects.Model.RedirectRule;

namespace Forte.EpiserverRedirects.Tests.Data
{
    public static class RandomDataGenerator
    {
        private const int MaxNumberOfDirectories = 3;
        private const int MaxLengthOfDirectory = 6;
        private static readonly Random RandomGenerator = new Random();
        public static RedirectRuleModel CreateRandomRedirectRule()
        {
            return new RedirectRuleModel
            {
                RuleId = Guid.NewGuid(),
                OldPattern = UrlPath.NormalizePath(GetRandomPath()),
                NewPattern = GetRandomPath(),
                IsActive = true,
                Notes = "some notes",
                CreatedOn = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                RedirectType = RedirectType.Temporary,
                CreatedBy = "user",
                RedirectRuleType = RedirectRuleType.ExactMatch,
                RedirectOrigin = RedirectOrigin.System,
                Priority = RandomGenerator.Next(1, int.MaxValue),
                HostId = null
            };
        }

        public static RedirectRuleDto CreateRandomRedirectRuleDto()
        {
            return new RedirectRuleDto
            {
                Id = Guid.NewGuid(),
                OldPattern = GetRandomPath(),
                NewPattern = GetRandomPath(),
                IsActive = true,
                Notes = "some notes",
                CreatedOn = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                RedirectType = RedirectType.Temporary,
                CreatedBy = "user",
                RedirectRuleType = RedirectRuleType.ExactMatch,
                RedirectOrigin = RedirectOrigin.System,
                Priority = RandomGenerator.Next(1, int.MaxValue),
                HostId = null,
                HostName = "All hosts"
            };
        }
        private static string GetRandomPath()
        {
            var directoriesNumber = RandomGenerator.Next(1, MaxNumberOfDirectories);

            var directories = new List<string>();

            for (var i = 0; i < directoriesNumber; i++)
            {
                var directory = GetRandomDirectoryString(RandomGenerator);
                directories.Add(directory);
            }
            var randomPath = string.Join("/", directories);
            
            return randomPath;
        }
        
        private static string GetRandomDirectoryString(Random random)
        {
            return GetRandomString(random, MaxLengthOfDirectory);
        }

        public static string GetRandomString(Random random, int characterAmount)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, random.Next(1, characterAmount))
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}