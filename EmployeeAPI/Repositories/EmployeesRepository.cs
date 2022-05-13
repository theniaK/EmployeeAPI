using EmployeeAPI.Models;
using MongoDB.Driver;

namespace EmployeeAPI.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly IMongoCollection<Employee> db;

        public EmployeesRepository(IMongoClient client)
        {
            var database = client.GetDatabase("Users");
            db = database.GetCollection<Employee>("Employees");
        }

        public async Task Insert(Dictionary<string, List<string>> employeeInfos)
        {
            foreach (var info in employeeInfos)
            {
                var employee = new Employee
                {
                    Id = new Guid(),
                    Name = info.Key,
                    Description = info.Value.FirstOrDefault(),
                    Date = DateTime.Now,
                };

                var cursor = await db.FindAsync(s => s.Name == info.Key);
                var employeeResult = cursor.FirstOrDefault();
                if (employeeResult == null)
                {
                    await db.InsertOneAsync(employee);
                }
            }
        }

        public async Task Insert(Employee employeeInfo)
        {
            var employee = new Employee
            {
                Id = new Guid(),
                Name = employeeInfo.Name,
                Description = employeeInfo.Description,
                Date = DateTime.Now,
            };

            var cursor = await db.FindAsync(s => s.Name == employee.Name);
            var employeeResult = cursor.FirstOrDefault();
            if (employeeResult == null)
            {
                await db.InsertOneAsync(employee);
            }
        }

        public async Task<List<Employee>> FindAll()
        {
            var cursor = await db.FindAsync(Builders<Employee>.Filter.Empty);
            var employeeInfos = cursor.ToList();
            return await Task.FromResult(employeeInfos);
        }

        public async Task<Employee> FindOne(Guid id)
        {
            var cursor = await db.FindAsync(s => s.Id == id);
            var employeeInfo = cursor.FirstOrDefault();
            return await Task.FromResult(employeeInfo);
        }

        public async Task Update(Dictionary<string, List<string>> employeeInfos, Guid id)
        {
            var result = db.Find(s => s.Id == id).FirstOrDefault();
            if (result != null)
            {
                foreach (var info in employeeInfos)
                {
                    result.Name = info.Key;
                    result.Description = info.Value[0];
                }

                var filter = Builders<Employee>.Filter.Eq(x => x.Id, id);
                await db.ReplaceOneAsync(filter, result);
            }
        }

        public async Task DeleteAll()
        {
            await db.DeleteManyAsync(Builders<Employee>.Filter.Empty);
        }

        public async Task DeleteOne(Guid id)
        {
            await db.DeleteOneAsync(s => s.Id == id);
        }
    }
}
