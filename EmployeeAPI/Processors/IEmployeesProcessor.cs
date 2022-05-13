using EmployeeAPI.Models;

namespace EmployeeAPI.Processors
{
    public interface IEmployeesProcessor
    {
        /// <summary>
        /// Add employee infos in the db.
        /// </summary>
        /// <param name="employeeInfos"></param>
        /// <returns></returns>
        Task Insert(Dictionary<string, List<string>> employeeInfos);

        /// <summary>
        /// Add employee info in the db.
        /// </summary>
        /// <param name="employeeInfo"></param>
        /// <returns></returns>
        Task Insert(Employee employeeInfo);

        /// <summary>
        /// Retrieve all employees.
        /// </summary>
        /// <returns></returns>
        Task<List<Employee>> FindAll();

        /// <summary>
        /// Retrieve one employee.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Employee> FindOne(Guid id);

        /// <summary>
        /// Delete all employees.
        /// </summary>
        /// <returns></returns>
        Task DeleteAll();

        /// <summary>
        /// Delete one employee.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteOne(Guid id);

        /// <summary>
        /// Update all employees.
        /// </summary>
        /// <param name="employeeInfo"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Update(Dictionary<string, List<string>> employeeInfos, Guid id);
    }
}
