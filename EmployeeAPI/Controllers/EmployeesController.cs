using EmployeeAPI.Models;
using EmployeeAPI.Processors;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesProcessor processor;

        public EmployeesController(IEmployeesProcessor processor)
        {
            this.processor = processor;
        }


        //GET api/Employees
        [HttpGet("ping")]
        public async Task<ActionResult<bool>> Ping()
        {
            return await Task.FromResult(true);
        }

        //POST api/Employees
        [HttpPost]
        public async Task<ActionResult> PostEmployees([FromBody] Dictionary<string, List<string>> employeeInfos)
        {
            if (employeeInfos.Count != 0)
            {
                await this.processor.Insert(employeeInfos);
                return await Task.FromResult(Ok());
            }

            return await Task.FromResult(NoContent());
        }

        //POST api/Employees/post
        [HttpPost("post")]
        public async Task<ActionResult> PostEmployee([FromBody] Employee employeeInfo)
        {
            if (employeeInfo != null)
            {
                await this.processor.Insert(employeeInfo);
                return await Task.FromResult(Ok());
            }

            return await Task.FromResult(NoContent());
        }

        //GET api/Employees/get
        [HttpGet("get")]
        public async Task<ActionResult<List<Employee>>> GetEmployees()
        {
            var result = await this.processor.FindAll();

            if (result != null)
            {
                return await Task.FromResult(result);
            }

            return await Task.FromResult(NotFound());
        }

        //GET api/Employees/get/{{id}}
        [HttpGet("get/{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(Guid id)
        {
            var result = await this.processor.FindOne(id);

            if (result != null)
            {
                return await Task.FromResult(result);
            }

            return await Task.FromResult(NotFound());
        }

        //UPDATE api/Employees/update/{{id}}
        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdateEmployee(Dictionary<string, List<string>> employeeInfo, Guid id)
        {
            var result = await this.processor.FindOne(id);

            if (result != null)
            {
                await this.processor.Update(employeeInfo, id);
                return await Task.FromResult(Ok());
            }

            return await Task.FromResult(NotFound());
        }

        //DELETE api/Employees/delete/{{id}}
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteEmployee(Guid id)
        {
            var result = await this.processor.FindOne(id);

            if (result != null)
            {
                await this.processor.DeleteOne(id);
                return await Task.FromResult(Ok());
            }

            return await Task.FromResult(NotFound());
        }

        //DELETE api/Employees/delete
        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteEmployees()
        {
            await this.processor.DeleteAll();
            return await Task.FromResult(Ok());
        }
    }
}