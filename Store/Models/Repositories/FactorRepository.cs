using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Store.Models.DomainModels;
using System.Linq.Expressions;
using System.Data.SqlClient;

namespace Store.Models.Repositories
{
    public class FactorRepository:IDisposable
    {
        private StoreDBEntities db = null;

        public FactorRepository()
        {
            db = new DomainModels.StoreDBEntities();
        }

        public bool Add(Factor entity, bool AutoSave = true)
        {
            try
            {
                db.Factors.Add(entity);
                   if (AutoSave == true)
                    return Convert.ToBoolean(db.SaveChangesAsync());
                else
                    return false;
            }
            catch (Exception)
            {
                return false; 
            }
        }

        public bool Update(Factor entity, bool AutoSave = true)
        {
            try { 
       
                db.Factors.Attach(entity);
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

        public bool Delete(Factor entity, bool AutoSave = true)
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
                var entity = db.Factors.Find(id);
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

        public Factor Find(int Id)
        {
            try
            {
                return db.Factors.Find(Id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IQueryable<Factor> Where(Expression<Func<Factor, bool>> predicate)
        {
            try
            {
                return db.Factors.Where(predicate);
            }
            catch (Exception)
            {
                return null;
            }
        
        }

        public IQueryable<Factor> Select()
        {
            try
            {
                return db.Factors.AsQueryable();
            }
            catch (Exception)
            {
                return null;
            }

        }


        public IQueryable<TResult> Select<TResult>(System.Linq.Expressions.Expression<Func<Factor, TResult>> selector)
        {
            try
            {
                return db.Factors.Select(selector);
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
                if (db.Factors.Any())
                    return db.Factors.OrderByDescending(p => p.Id).First().Id;
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
        ~FactorRepository()
        {
            Dispose(false); 
        }

    }


}