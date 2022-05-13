using EmployeeAPI.Models;
using EmployeeAPI.Repositories;

namespace EmployeeAPI.Processors
{
    public class EmployeesProcessor : IEmployeesProcessor
    {
        private readonly IEmployeesRepository repo;

        public EmployeesProcessor(IEmployeesRepository repo)
        {
            this.repo = repo;
        }

        public async Task Insert(Dictionary<string, List<string>> employeeInfos)
        {
            await this.repo.Insert(employeeInfos);
        }

        public async Task Insert(Employee employeeInfo)
        {
            await this.repo.Insert(employeeInfo);
        }

        public async Task<List<Employee>> FindAll()
        {
            return await this.repo.FindAll();
        }

        public async Task<Employee> FindOne(Guid id)
        {
            return await this.repo.FindOne(id);
        }

        public async Task DeleteAll()
        {
            await this.repo.DeleteAll();
        }

        public async Task DeleteOne(Guid id)
        {
            await this.repo.DeleteOne(id);
        }

        public async Task Update(Dictionary<string, List<string>> employeeInfos, Guid id)
        {
            await this.repo.Update(employeeInfos, id);
        }
    }
}
