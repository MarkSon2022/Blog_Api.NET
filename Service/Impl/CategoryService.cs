using BusinessObject;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Impl
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public Category AddCategory(Category category)
        {
           return categoryRepository.Add(category);
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return categoryRepository.GetAll();
        }

        public Category GetCategoryById(int id)
        {
            return categoryRepository.GetById(id);
        }

        public void RemoveCategory(Category category)
        {
            categoryRepository.Remove(category);
        }

        public Category UpdateCategory(Category category)
        {
            return categoryRepository.Update(category);
        }
    }
}
