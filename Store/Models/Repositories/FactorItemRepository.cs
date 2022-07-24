using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Store.Models.DomainModels;
using System.Linq.Expressions;

namespace Store.Models.Repositories
{
    public class FactorItemRepository:IDisposable
    {
        private StoreDBEntities db = null;

        public FactorItemRepository()
        {
            db = new DomainModels.StoreDBEntities();
        }

        public bool Add(FactorItem entity, bool AutoSave = true)
        {
            try
            {
                db.FactorItems.Add(entity);
                if (AutoSave == true)
                    return Convert.ToBoolean(db.SaveChanges());
                else
                    return false;
            }
            catch (Exception)
            {
                return false; 
            }
        }

        public bool Update(FactorItem entity, bool AutoSave = true)
        {
            try
            {
                db.FactorItems.Attach(entity);
                db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                if (AutoSave)
                    return Convert.ToBoolean(db.SaveChanges());
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(FactorItem entity, bool AutoSave = true)
        {
            try
            {
                db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                if (AutoSave)
                    return Convert.ToBoolean(db.SaveChanges());
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int? id, bool autoSave = true)
        {
            try
            {
                var entity = db.FactorItems.Find(id);
                db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                if (autoSave)
                    return Convert.ToBoolean(db.SaveChanges());
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public FactorItem Find(int Id)
        {
            try
            {
                return db.FactorItems.Find(Id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IQueryable<FactorItem> Where(Expression<Func<FactorItem, bool>> predicate)
        {
            try
            {
                return db.FactorItems.Where(predicate);
            }
            catch (Exception)
            {
                return null;
            }
        
        }

        public IQueryable<FactorItem> Select()
        {
            try
            {
                return db.FactorItems.AsQueryable();
            }
            catch (Exception)
            {
                return null;
            }

        }


        public IQueryable<TResult> Select<TResult>(System.Linq.Expressions.Expression<Func<FactorItem, TResult>> selector)
        {
            try
            {
                return db.FactorItems.Select(selector);
            }
            catch
            {
                return null;
            }
        }

        public int GetLastIdentity()
        {
            try
            {
                if (db.FactorItems.Any())
                    return db.FactorItems.OrderByDescending(p => p.Id).First().Id;
                else
                    return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int Save() {
            try
            {
                return db.SaveChanges();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.db != null)
                {
                    this.db.Dispose();
                    this.db = null;
                }
            }
        }
        ~FactorItemRepository()
        {
            Dispose(false); 
        }

    }


}