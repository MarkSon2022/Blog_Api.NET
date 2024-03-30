using BusinessObject;
using DataAccessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Impl
{
    public class CategoryRepository : ICategoryRepository
    {
        public Category Add(Category category)=>CategoryDAO.Instance.Add(category); 

        public IEnumerable<Category> GetAll()=>CategoryDAO.Instance.GetAll();

        public Category GetById(int id)=>CategoryDAO.Instance.GetById(id);  

        public void Remove(Category category)=>CategoryDAO.Instance.Remove(category);

        public Category Update(Category category)=>CategoryDAO.Instance.Update(category);

     
    }
}
