using BlogApi.Model;
using BusinessObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Service;
using Service.Impl;

namespace BlogApi.Controllers
{

    [ApiController]
    [Route("api/categories")]
    [Authorize]
    public class CategoryController : Controller
    {
        private ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = categoryService.GetAllCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            if (id == null) {
                return BadRequest("Id must not be empty");
            }
            var category= categoryService.GetCategoryById(id);
            if (category == null)
            {
                return BadRequest("There is no category with id: " + id);
            }
            else { 
                return Ok(category);
            }
        }


        [HttpPost]
        public IActionResult createCategorie([FromBody] CategoryDto categoryDto) {
            if (ModelState.IsValid)
            {
                Category category = new Category(); 
                category.Id = categoryDto.Id;
                category.Name = categoryDto.Name;

                var checkCategoryById = categoryService.GetCategoryById(category.Id);
                if (checkCategoryById != null)
                {
                    return BadRequest("This category already exist");
                }
                var createdCategory = categoryService.AddCategory(category);
                return new CreatedAtActionResult(nameof(createCategorie), "Category", new { id = category.Id }, category);
            }
            else
            {
                return BadRequest("You must enter valid category!");
            }
        }


        [HttpPut]
        public IActionResult updateCategory([FromBody] CategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category();
                category.Id = categoryDto.Id;
                category.Name = categoryDto.Name;
                var updateCategory = categoryService.UpdateCategory(category);
                return Ok(updateCategory);
            }
            else
            {
                return BadRequest("You must enter valid category!");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult deleteCategory(int id)
        {
            if (id != null)
            {
                var deletedCaregory= categoryService.GetCategoryById(id);
                if (deletedCaregory != null)
                {
                    categoryService.RemoveCategory(deletedCaregory);
                    return Ok("Delete success!");
                }
                else {
                    return BadRequest("This category does not exist");
                }
            }
            else
            {
                return BadRequest("You must enter the id");
            }
        }
    }
}
