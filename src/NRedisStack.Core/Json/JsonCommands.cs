using NRedisStack.Core;
using NRedisStack.Core.Literals;
using StackExchange.Redis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NRedisStack.Core
{

    public class JsonCommands
    {
        IDatabase _db;
        public JsonCommands(IDatabase db)
        {
            _db = db;
        }
        private readonly JsonSerializerOptions Options = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };
        public RedisResult Set(RedisKey key, string path, object obj, When when = When.Always)
        {
            string json = JsonSerializer.Serialize(obj);
            return Set(key, path, json, when);
        }

        public RedisResult Set(RedisKey key, string path, string json, When when = When.Always)
        {
            switch (when)
            {
                case When.Exists:
                    return _db.Execute(JSON.SET, key, path, json, "XX");
                case When.NotExists:
                    return _db.Execute(JSON.SET, key, path, json, "NX");
                default:
                    return _db.Execute(JSON.SET, key, path, json);
            }
        }

        public RedisResult Get(RedisKey key, RedisValue indent = default,
                                          RedisValue newLine = default, RedisValue space = default, RedisValue path = default)
        {
            var args = Helper.CreateArrayWithoutNull(key, indent, newLine, space, path);
            // if (args == null || args.Length == 0)
            // {
            //     return _db.Execute(JSON.GET);
            // }
            return _db.Execute(JSON.GET, new List<object> { key }); //TODO: put back the params
        }
    }
}