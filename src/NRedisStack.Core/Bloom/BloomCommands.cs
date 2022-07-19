using NRedisStack.Core.Literals;
using StackExchange.Redis;

namespace NRedisStack.Core
{

    public class BloomCommands
    {
        IDatabase _db;
        public BloomCommands(IDatabase db)
        {
            _db = db;
        }

        public RedisResult Add(RedisKey key, string item)
        {
            return _db.Execute(BF.ADD, key, item);
        }

        public RedisResult Exists(RedisKey key, string item)
        {
            return _db.Execute(BF.EXISTS, key, item);
        }

        public RedisResult Info(RedisKey key)
        {
            return _db.Execute(BF.INFO, key);
        }

        public RedisResult Insert(RedisKey key, string[] items, int? capacity = null, double? error = null, int? expansion = null, bool nocreate = false, bool nonscaling = false) //NOT DONE
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            // var args = Helper.CreateArrayWithoutNull(capacity,error,expansion);
            List<object> args = new List<object> {key};
            args.AddRange(Helper.CreateArrayWithoutNull(capacity,
                                                        error,
                                                        expansion));

            // int i = 0;

            // arr[i++] = key.ToString();

            // foreach(var argument in args)
            // {
            //     arr[i++] = argument.ToString();
            // }

            if (capacity != null) {
                args.Add(BloomArgs.CAPACITY);
                args.Add(capacity);
            }

            if(nocreate)
                args.Add(BloomArgs.NOCREATE);

            if(nonscaling)
                args.Add(BloomArgs.NONSCALING);

            foreach(var item in items)
            {
                args.Add(item);
            }

            return _db.Execute(BF.INSERT, args);
        }

        public RedisResult ScanDump(RedisKey key, int iterator)
        {
            return _db.Execute(BF.SCANDUMP, key, iterator);
        }

        public RedisResult LoadChunk(RedisKey key, int iterator, string data)
        {
            return _db.Execute(BF.LOADCHUNK, key, iterator, data);
        }

        // BF.MADD
        // BF.MEXISTS
        // BF.RESERVE
    }
}
