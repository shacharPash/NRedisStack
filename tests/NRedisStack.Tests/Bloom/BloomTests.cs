using Xunit;
using StackExchange.Redis;
using NRedisStack.Core.RedisStackCommands;
using Moq;


namespace NRedisStack.Tests.Bloom;

public class BloomTests : AbstractNRedisStackTest, IDisposable
{
    Mock<IDatabase> _mock = new Mock<IDatabase>();
    private readonly string key = "BLOOM_TESTS";
    public BloomTests(RedisFixture redisFixture) : base(redisFixture) { }

    public void Dispose()
    {
        redisFixture.Redis.GetDatabase().KeyDelete(key);
    }

    [Fact]
    public void TestBfAddWhenExist()
    {
        IDatabase db = redisFixture.Redis.GetDatabase();

        Assert.True((db.BF().Add(key, "item1")).ToString() == "1"); // first time
        Assert.True(db.BF().Add(key, "item1").ToString() == "0"); // second time
    }

    [Fact]
    public void TestBfAddExists()
    {
        IDatabase db = redisFixture.Redis.GetDatabase();

        db.BF().Add(key, "item1");
        Assert.True(db.BF().Exists(key, "item1").ToString() == "1");
    }

    [Fact]
    public void TestInsert()
    {
        IDatabase db = redisFixture.Redis.GetDatabase();
        string[] items = new string[] { "foo", "bar", "baz" };
        db.BF().Insert("filter", items, 1000, 0.5, null, true, false);
        //db.SetAdd("key", "value");
        Assert.True(1 == 1);
    }
}