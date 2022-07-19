using StackExchange.Redis;
namespace NRedisStack.Core
{
    public static class Helper
    {
        public static object[] CreateArrayWithoutNull(params object[] args)
        {
            // TODO: If the object is RedisValue, need to check if it's not null (IsNull)
            return args.Where(x => x != null).ToArray();
        }
    }
}