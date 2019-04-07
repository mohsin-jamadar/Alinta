using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Serilog;
using System.Threading.Tasks;
using Alinta.Core.Domain;
using Alinta.Core.Entities;
using Alinta.Core.Service.Abstract;

namespace Alinta.Api.Controllers
{
    [Route("api/[controller]")]
    public class CustomerAsyncController : ControllerBase
    {
        private readonly ICustomerServiceAsync<CustomerViewModel, Customer> _customerServiceAsync;
        public CustomerAsyncController(ICustomerServiceAsync<CustomerViewModel, Customer> customerServiceAsync)
        {
            _customerServiceAsync = customerServiceAsync;
        }

      
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _customerServiceAsync.GetAll();
            return Ok(items);
        }

     
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _customerServiceAsync.GetOne(id);
            if (item == null)
            {
                Log.Error("GetById({ ID}) NOT FOUND", id);
                return NotFound();
            }
            return Ok(item);
        }

     
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerViewModel customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var id = await _customerServiceAsync.Add(customer);
            return Created($"api/Customer/{id}", id);  
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerViewModel customer)
        {
            if (!ModelState.IsValid || customer.Id != id)
                return BadRequest();

            if (await _customerServiceAsync.Update(customer))
                return Accepted(customer);
            else
                return StatusCode(304);
        }

        [HttpGet("getbyname/{name:maxlength(30)}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var items = await _customerServiceAsync.GetByName(name);
            if (items == null)
            {
                Log.Error("GetOneByName({ NAME}) NOT FOUND", name);
                return NotFound();
            }
            return Ok(items);
        }

 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _customerServiceAsync.Remove(id))
                return NoContent();   	    
            else
                return NotFound();          
        }
    }
}


