using StackExchange.Redis;
namespace NRedisStack.Core
{
    public static class Helper
    {
        public static object[] CreateArrayWithoutNull(params object[] args)
        {
            //var result = new List<RedisValue>();
            return args.Where(x => x != null).ToArray();
        }
    }
}