using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Interfaces;
using TaskManagementAPI.Models;
using TaskManagementAPI.Services;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : Controller
    {
        private readonly IGenericRepository<Category> _repository;

        public CategoryController(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult GetCategories()
        {
            List<Category> result = (List<Category>)_repository.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult Get([FromRoute] int id)
        {
            Category task = _repository.GetById(id);
            return task == null ? NotFound("Category not found") : Ok(task);
        }

        //[HttpPost]
        //public ActionResult AddCategory([FromBody] Category category)
        //{
        //    if (category == null)
        //    {
        //        return BadRequest("Category is required");
        //    }
        //    if (string.IsNullOrWhiteSpace(category.Name))
        //    {
        //        return BadRequest("Category must have a name.");
        //    }
        //    else
        //    {
        //        try
        //        {
        //            _repository.Add(category);
        //            return Ok(category);
        //        }
        //        catch (Exception e)
        //        {
        //            return BadRequest(e);
        //        }
        //    }
        //}

        [HttpPost]
        public ActionResult AddCategory(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Category must have a name.");
            }
            else
            {
                try
                {
                    Category category = new Category { Name = name, Description = description };

                    _repository.Add(category);
                    return Ok(category);
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTask(int id, [FromBody] Category category)
        {
            var cat= _repository.GetById(id);
            if (cat == null || cat.Id != category.Id)
            {
                return NotFound("Category Not Found!!!!!!");
            }
            _repository.Update(category);
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCourse([FromRoute] int id)
        {
            var category = _repository.GetById(id);
            if (category == null)
            {
                return NotFound("Category not found!!!!!");
            }

            try
            {
                _repository.Delete(id);
                return Ok("Category Deleted Successfully");
            } catch (Exception e)
            {
                return BadRequest(e);
            }
            
        }
    }
}
