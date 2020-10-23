using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Models.DataModels;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{
    public class CachedRepository<T> : Repository<T>, ICachedRepository<T> where T : BaseModel
    {
        private static ApplicationDbContext db = null;
        private static ApplicationDbContext Db 
        {
            get { return db ??= new ApplicationDbContext(); }
        }
        public CachedRepository(ILogger<CachedRepository<T>> logger) : base(Db, logger) { }
        private static Dictionary<int, T> cache;
        private static readonly object cacheLock = new object();
        private Dictionary<int, T> Cache
        {
            get
            {
                lock (cacheLock)
                {
                    if (cache == null) InitializeCache();
                    return cache;
                }
            }
        }
        private void InitializeCache()
        {
            cache = new Dictionary<int, T>();
            foreach (var p in base.GetAll())
            {
                cache[p.Id] = p;
            }
        }
        public override T Get(int id)
        {
            return Cache[id];
        }
        public override IQueryable<T> GetAll()
        {
            return Cache.Values.AsQueryable();
        }
        public override void HardDelete(T entity)
        {
            lock(Cache)
            {
                Cache.Remove(entity.Id);
            }
            base.HardDelete(entity);
        }
        public override void HardDeleteRange(IEnumerable<T> entities)
        {
            lock (Cache)
            {
                foreach (var e in entities)
                {
                    Cache.Remove(e.Id);
                }
            }
            base.HardDeleteRange(entities);
        }
        public override async Task Insert(T entity)
        {
            await base.Insert(entity);
            lock(Cache)
            {
                Cache[entity.Id] = entity;
            }
        }
        public override async Task InsertRange(IEnumerable<T> entities)
        {
            await base.InsertRange(entities);
            lock (Cache)
            {
                foreach (var e in entities)
                {
                    Cache[e.Id] = e;
                }
            }
        }
        public override void SoftDelete(T entity)
        {
            lock(Cache)
            {
                Cache.Remove(entity.Id);
            }
            base.SoftDelete(entity);
        }
        public override void SoftDeleteRange(IEnumerable<T> entities)
        {
            lock (Cache)
            {
                foreach (var e in entities)
                {
                    Cache.Remove(e.Id);
                }
            }
            base.SoftDeleteRange(entities);
        }
        public override void Update(T entity)
        {
            lock(Cache)
            {
                Cache[entity.Id] = entity;
            }
            base.Update(entity);
        }
        public override void UpdateRange(IEnumerable<T> entities)
        {
            lock(Cache)
            {
                foreach(var e in entities)
                {
                    Cache[e.Id] = e;
                }
            }
            base.UpdateRange(entities);
        }
    }
}
