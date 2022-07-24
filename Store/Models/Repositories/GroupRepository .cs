using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Store.Models.DomainModels;
using System.Linq.Expressions;

namespace Store.Models.Repositories
{
    public class GroupRepository:IDisposable
    {
        private StoreDBEntities db = null;

        public GroupRepository()
        {
            db = new DomainModels.StoreDBEntities();
        }

        public bool Add(Group entity, bool AutoSave = true)
        {
            try
            {
                db.Groups.Add(entity);
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

        public bool Update(Group entity, bool AutoSave = true)
        {
            try
            {
                db.Groups.Attach(entity);
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

        public bool Delete(Group entity, bool AutoSave = true)
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
                var entity = db.Groups.Find(id);
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

        public Group Find(int Id)
        {
            try
            {
                return db.Groups.Find(Id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IQueryable<Group> Where(Expression<Func<Group, bool>> predicate)
        {
            try
            {
                return db.Groups.Where(predicate);
            }
            catch (Exception)
            {
                return null;
            }
        
        }

        public IQueryable<Group> Select()
        {
            try
            {
                return db.Groups.AsQueryable();
            }
            catch (Exception)
            {
                return null;
            }

        }


        public IQueryable<TResult> Select<TResult>(System.Linq.Expressions.Expression<Func<Group, TResult>> selector)
        {
            try
            {
                return db.Groups.Select(selector);
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
                if (db.Groups.Any())
                    return db.Groups.OrderByDescending(p => p.Id).First().Id;
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
        ~GroupRepository()
        {
            Dispose(false); 
        }

    }


}