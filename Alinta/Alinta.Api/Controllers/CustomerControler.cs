using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Serilog;
using Alinta.Core.Domain;
using Alinta.Core.Service.Abstract;
using Alinta.Core.Entities;

namespace Alinta.Api.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService<CustomerViewModel, Customer> _customerService;
        public CustomerController(ICustomerService<CustomerViewModel, Customer> customerService)
        {
            _customerService = customerService;
        }

       
        [HttpGet]
        public IActionResult GetAll()
        {
            var items = _customerService.GetAll();
            if (items == null)
            {
                Log.Error("GetAll() NOT FOUND");
                return NotFound();
            }
            return Ok(items);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            

            var item = _customerService.GetOne(id);
            if (item == null)
            {
                Log.Error("GetById({ ID}) NOT FOUND", id);
                return NotFound();
            }

            return Ok(item);
        }


        [HttpGet("getbyname/{name:maxlength(30)}")]
        public IActionResult  GetByName(string name)
        {


            var items = _customerService.GetByName(name);
            if (items == null)
            {
                Log.Error("GetByName({ NAME}) NOT FOUND", name);
                return NotFound();
            }
            return Ok(items);
        }


        [HttpPost]
        public IActionResult Create([FromBody] CustomerViewModel customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var id = _customerService.Add(customer);
            return Created($"api/Customer/{id}", id);  
        }

        
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CustomerViewModel customer)
        {
            if (!ModelState.IsValid || customer.Id != id)
                return BadRequest();
            if (_customerService.Update(customer))
                return Accepted(customer);
            else
                return StatusCode(304);    
        }

        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_customerService.Remove(id))
                return NoContent();         
            else
                return NotFound();           
        }
    }
}


