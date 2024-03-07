using db_Api.Models.Generated;
using db_Api.Models.Generated.Employee;

namespace db_Api.Repository.EmployeeRepository
{
    public interface IEmployeeRepository
    {
/*        List<ListeReunion11> liste_reunion11(string? numReunion, string? type, string? lieu, string? site);
        
        Task<List<OK>> add_employee(string? codetypeR, DateTime? date_prev, DateTime? date_Real,string? lieu, string? site);
        Delete DeleteReunion(int nReunion);

        Task<IsDone> update_reunion(updatereunion Am); */
        Task<List<OK>> add_employee(string? name, string? email, string? password,string? salary, string? address, string? image);
        List<ListeEmployee> liste_employee(string? id, string? name, string? email, string? salary, string? address);
        Delete DeleteEmployee(int id);
        Task<IsDone> update_employe(updateEmployee Am);
        Task<List<OK>> login_employe(string email, string password);
        List<ListeEmployee> detail_Emp(string id);
    }
}
