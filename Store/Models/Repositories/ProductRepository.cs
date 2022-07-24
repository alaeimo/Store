using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Store.Models.DomainModels;
using System.Linq.Expressions;
using System.IO;

namespace Store.Models.Repositories
{
    public class ProductRepository : IDisposable
    {
        private StoreDBEntities db = null;

        public ProductRepository()
        {
            db = new DomainModels.StoreDBEntities();
        }

        public bool Add(Product entity, bool AutoSave = true)
        {
            try
            {
                db.Products.Add(entity);
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

        public bool Update(Product entity, bool AutoSave = true)
        {
            
            
            int id = entity.Id;
            entity.Id = 0;
            bool add = Add(entity);
            bool delete = Delete(id,false);

                return add && delete;
        }

        public bool Delete(Product entity, bool AutoSave = true)
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

        public bool Delete(int? id,bool delateImage = true , bool autoSave = true)
        {
            try
            {
                var entity = db.Products.Find(id);
                db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                if (autoSave)
                {
                    bool result = Convert.ToBoolean(db.SaveChanges());
                    if (result && delateImage)
                    {
                        String ImagePath = AppDomain.CurrentDomain.BaseDirectory + "\\Files\\UploadImage\\" + entity.Image;
                        try
                        {
                            if (File.Exists(ImagePath))
                                File.Delete(ImagePath);
                        }
                        catch (Exception)
                        { }
                    }
                    return result; 
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public Product Find(int Id)
        {
            try
            {
                return db.Products.Find(Id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> predicate)
        {
            try
            {
                return db.Products.Where(predicate);
            }
            catch (Exception)
            {
                return null;
            }

        }

        public IQueryable<Product> Select()
        {
            try
            {
                return db.Products.AsQueryable();
            }
            catch (Exception)
            {
                return null;
            }

        }


        public IQueryable<TResult> Select<TResult>(System.Linq.Expressions.Expression<Func<Product, TResult>> selector)
        {
            try
            {
                return db.Products.Select(selector);
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
                if (db.Products.Any())
                    return db.Products.OrderByDescending(p => p.Id).First().Id;
                else
                    return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int Save()
        {
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
        ~ProductRepository()
        {
            Dispose(false);
        }

    }


}