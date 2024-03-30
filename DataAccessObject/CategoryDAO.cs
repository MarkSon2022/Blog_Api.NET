using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;    
        private static readonly object locker=new object();
        private CategoryDAO() { }
        public static CategoryDAO Instance { 
            get { 
                lock(locker)
                {
                    if (instance == null)
                    {
                        instance = new CategoryDAO();
                    }
                    return instance;
                }
            } 
        }

        public IEnumerable<Category> GetAll()
        {
            try { 
                List<Category> list = new List<Category>();
                var context= new BlogContext(); 
                list= context.Categories.ToList();
                
                return list;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Category GetById(int id)
        {
            try
            {
                Category category = new Category();
                var context = new BlogContext();
                category = context.Categories.SingleOrDefault(c=>c.Id.Equals(id));

                return category;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Category Add(Category category)
        {
            try
            {
                var existedCategory = GetById(category.Id);
                if (existedCategory == null)
                {
                    var context = new BlogContext();
                    context.Categories.Add(category);
                    context.SaveChanges();
                    return category;
                }
                else
                {
                    throw new Exception("This category already exists!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(Category category)
        {
            try
            {
                var existedCategory = GetById(category.Id);
                if (existedCategory != null)
                {
                    var context = new BlogContext();
                    context.Categories.Remove(category);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This category does not exist to delete!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Category Update(Category category)
        {
            try
            {
                var existedCategory = GetById(category.Id);
                if (existedCategory != null)
                {
                    var context = new BlogContext();
                    context.Entry<Category>(category).State=EntityState.Modified;
                    context.SaveChanges();
                    return (category);
                }
                else
                {
                    throw new Exception("This category does not exist to update!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
