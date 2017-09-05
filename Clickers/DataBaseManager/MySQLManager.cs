using Clickers.Json;
using Clickers.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clickers.DataBaseManager
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    class MySQLManager<TEntity> : DbContext where TEntity : class
    {
        MySQLFullDB initDBIfNotExist;
        public MySQLManager()
            : base(JsonManager.Instance.ReadFile<ConnectionString>(@"D:\Workspaces\Clickers\Clickers\JsonConfig\", @"MysqlConfig.json").ToString())
        {
            initDBIfNotExist = new MySQLFullDB();
        }
        public DbSet<TEntity> DbSetT { get; set; }
        
        public async Task<TEntity> Insert(TEntity item)
        {
            this.DbSetT.Add(item);
            await this.SaveChangesAsync();
            return item;
        }

        public async Task<IEnumerable<TEntity>> Insert(IEnumerable<TEntity> items)
        {
            foreach (var item in items)
            {
                this.DbSetT.Add(item);
            }
            await this.SaveChangesAsync();
            return items;
        }

        public async Task<TEntity> Update(TEntity item)
        {
            if (this.Entry<TEntity>(item).State == EntityState.Detached)
            {
                this.DbSetT.Attach(item);
            }

            await Task.Factory.StartNew(() =>
            {
                this.Entry<TEntity>(item).State = EntityState.Modified;
            });
            await this.SaveChangesAsync();
            return item;
        }

        public async Task<IEnumerable<TEntity>> Update(IEnumerable<TEntity> items)
        {
            await Task.Factory.StartNew(() =>
            {
                foreach (var item in items)
                {
                    this.Entry<TEntity>(item).State = EntityState.Modified;
                }
            });
            await this.SaveChangesAsync();
            return items;
        }

        public async Task<TEntity> Get(Int32 id)
        {
            return await this.DbSetT.FindAsync(id) as TEntity;
        }

        public async Task<TEntity> Get(String id)
        {
            return await this.DbSetT.FindAsync(id) as TEntity;
        }

        public async Task<List<TEntity>> GetAll_Task()
        {
            bool isOk = true;
            int itemNumber = 1;
            List<TEntity> itemList = new List<TEntity>();
            TEntity itemTank;
            while (isOk)
            {
                itemTank = await this.DbSetT.FindAsync(itemNumber) as TEntity;
                if (itemTank == null)
                {
                    isOk = false;
                }
                else
                {
                    itemList.Add(itemTank);
                }
                itemNumber++;
            }
            return itemList;
        }

        public List<TEntity> GetAll()
        {
            Task<List<TEntity>> test = this.GetAll_Task();
            List<TEntity> tarte = test.Result;
            return tarte;
        }

        public async Task<IEnumerable<TEntity>> Get()
        {
            DbSet<TEntity> temp = default(DbSet<TEntity>);
            List<TEntity> result = new List<TEntity>();
            await Task.Factory.StartNew(() =>
            {
                temp = base.Set<TEntity>();
            });
            result.AddRange(temp);
            return result;
        }

        public async Task<Int32> Delete(TEntity item)
        {
            await Task.Factory.StartNew(() =>
            {
                this.DbSetT.Attach(item);
                this.DbSetT.Remove(item);
            });
            return await this.SaveChangesAsync();
        }

        public async Task<Int32> Delete(IEnumerable<TEntity> items)
        {
            await Task.Factory.StartNew(() =>
            {
                this.DbSetT.Attach((items as List<TEntity>)[0]);
                this.DbSetT.RemoveRange(items);
            });
            var res = await this.SaveChangesAsync();
            return res;
        }

        public async Task<TEntity> UpdateBoucle(TEntity item)
        {
                Task.Run(async () =>
                {
                    Update(item);
                });
            
            return item;
        }
    }
}
