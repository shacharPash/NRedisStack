
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xunit;
namespace NRedisStack.Tests
{
    public abstract class AbstractNRedisStackTest : IClassFixture<RedisFixture>, IAsyncLifetime
    {
        protected internal RedisFixture redisFixture;

        protected internal AbstractNRedisStackTest(RedisFixture redisFixture) => this.redisFixture = redisFixture;

        private List<string> keyNames = new List<string>();

        protected internal string CreateKeyName([CallerMemberName] string memberName = "") => CreateKeyNames(1, memberName)[0];

        protected internal string[] CreateKeyNames(int count, [CallerMemberName] string memberName = "")
        {
            if (count < 1) throw new ArgumentOutOfRangeException(nameof(count), "Must be greater than zero.");

            var newKeys = new string[count];
            for (var i = 0; i < count; i++)
            {
                newKeys[i] = $"{GetType().Name}:{memberName}:{i}";
            }

            keyNames.AddRange(newKeys);

            return newKeys;
        }

        public Task InitializeAsync() => Task.CompletedTask;

        public async Task DisposeAsync()
        {
            var redis = redisFixture.Redis.GetDatabase();
            await redis.KeyDeleteAsync(keyNames.Select(i => (RedisKey)i).ToArray());
        }
    }
}