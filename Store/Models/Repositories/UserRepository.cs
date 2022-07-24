using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Store.Models.DomainModels;
using System.Linq.Expressions;

namespace Store.Models.Repositories
{
    public class UserRepository:IDisposable
    {
        private Store.Models.DomainModels.StoreDBEntities db = null;

        public UserRepository()
        {
            db = new DomainModels.StoreDBEntities();
        }

        public bool Add(User entity, bool AutoSave = true)
        {
            try
            {
                db.Users.Add(entity);
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

        public bool Update(User entity, bool AutoSave = true)
        {
            try
            {
                db.Users.Attach(entity);
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

        public bool Delete(User entity, bool AutoSave = true)
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

        public User Find(int Id)
        {
            try
            {
                return db.Users.Find(Id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Exist(String username, String password)
        {
            
            try
            {
               return db.Users.Where(p => p.Username == username && p.Password == password).Any();
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<User> Where(Expression<Func<User, bool>> predicate)
        {
            try
            {
                return db.Users.Where(predicate);
            }
            catch (Exception)
            {
                return null;
            }
        
        }

        public IQueryable<User> Select()
        {
            try
            {
                return db.Users.AsQueryable();
            }
            catch (Exception)
            {
                return null;
            }

        }


        public IQueryable<TResult> Select<TResult>(System.Linq.Expressions.Expression<Func<User, TResult>> selector)
        {
            try
            { 
                return db.Users.Select(selector);
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
                if (db.Users.Any())
                    return db.Users.OrderByDescending(p => p.Id).First().Id;
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
        ~UserRepository()
        {
            Dispose(false); 
        }
    }


}