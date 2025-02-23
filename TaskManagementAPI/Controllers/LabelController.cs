using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Interfaces;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("/api/labels")]
    public class LabelController : Controller
    {
        private readonly IGenericRepository<Label> _repository;

        public LabelController(IGenericRepository<Label> repository)
        {
            _repository = repository;

        }

        [HttpGet]
        public ActionResult GetLabels()
        {
            try
            {
                var result = _repository.GetAll();
                return Ok(result);
            } catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetLabelById([FromRoute] int id)
        {
            try
            {
                var label = _repository.GetById(id);
                return label == null ? NotFound("Label not found") : Ok(label);
            } catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        public ActionResult AddLabel([FromBody] Label label)
        {
            if (label == null)
            {
                return BadRequest("Label cannot be null");
            }
            else
            {
                try
                {
                    if (_repository.Any(l => l.Name == label.Name))
                    {
                        return Conflict(new { message = "Label's name already exists." });
                    }
                    _repository.Add(label);
                    return Ok(label);
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateLabel([FromRoute] int id, [FromBody] Label label)
        {
            var labelExists = _repository.GetById(id);
            if (labelExists == null )
            {
                return NotFound("Label Not Found!!!!!!");
            }

            try
            {
                if (_repository.Any(l => l.Id != id && l.Name == label.Name))
                {
                    return Conflict(new { message = "Label's name already exists." });
                }
                labelExists.Name = label.Name;
                _repository.Update(labelExists);
                return Ok(labelExists);
            } catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCourse([FromRoute] int id)
        {
            var label = _repository.GetById(id);
            if (label == null)
            {
                return NotFound("Label not found!!!!!");
            }

            try
            {
                _repository.Delete(id);
                return Ok("Label Deleted Successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }
    }
}

