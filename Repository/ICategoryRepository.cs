using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ICategoryRepository
    {
        public IEnumerable<Category> GetAll();
        public Category GetById(int id);
        public Category Add(Category category);
        public void Remove(Category category);
        public Category Update(Category category);
    }
}
