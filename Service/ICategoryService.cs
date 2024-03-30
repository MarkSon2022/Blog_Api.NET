using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ICategoryService
    {
        public IEnumerable<Category> GetAllCategories();
        public Category GetCategoryById(int id);
        public Category AddCategory(Category category);
        public void RemoveCategory(Category category);
        public Category UpdateCategory(Category category);
    }
}
