using Microsoft.AspNetCore.Mvc;
using db_Api.Models.Generated;
using db_Api.Models.Generated.Employee;
//using db_Api.Repository.OutilsRepository;
using db_Api.Repository.EmployeeRepository;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly Db_PartialContext _db_PartialContext; 
        //private readonly IOutilRepository _outilRepository;

        public EmployeeController(IEmployeeRepository employeeRepository, Db_PartialContext db_PartialContext)
        //IOutilRepository outilRepository)
        {
            _employeeRepository = employeeRepository;
            _db_PartialContext = db_PartialContext;
        //    _outilRepository = outilRepository;
        }

/*
               [HttpGet]
        [Route("getListeReunion11")]
        public ActionResult<IEnumerable<ListeReunion11>> GetListeReunion11(string? numReunion, string? type, string? lieu, string? site)
        {

            var items = _reunionRepository.liste_reunion11( numReunion, type, lieu, site);
            items.Reverse();
            return Ok(items);

        } */

        [HttpGet]
[Route("getEmployee")]
public ActionResult<IEnumerable<ListeEmployee>> GetListeEmployee(int? id, string? name, string? email, string? salary, string? address)
{
    var query = _db_PartialContext.ListeEmployees.AsQueryable();

    if (id != null)
{
    query = query.Where(e => e.id == id);
}

    if (!string.IsNullOrEmpty(name))
    {
        query = query.Where(e => e.name == name);
    }

    if (!string.IsNullOrEmpty(email))
    {
        query = query.Where(e => e.email == email);
    }

    if (!string.IsNullOrEmpty(salary))
    {
        query = query.Where(e => e.salary == salary);
    }

    if (!string.IsNullOrEmpty(address))
    {
        query = query.Where(e => e.address == address);
    }

    var items = query.ToList();
    items.Reverse();
    return Ok(items);
}


        [HttpPost]
        [Route("addEmployee")]
        public async Task<ActionResult<NumeroEmployee>> AddEmployee(string? name, string? email, string? password,string? salary, string? address, string? image)
        {
            try
            {
            var items = await _employeeRepository.add_employee(name, email, password, salary, address, image);
            NumeroEmployee numeroEmployee = new NumeroEmployee();
            numeroEmployee.id = Int32.Parse(items[0].ok);
            return numeroEmployee;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteEmployee")]
        public ActionResult<Delete> DeleteEmployee(int id)
        {
            try
            {
                var response = _employeeRepository.DeleteEmployee(id);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("updateEmployee")]
        public async Task<ActionResult> updateEmployee(updateEmployee Am)
        {
            try
            {
                var items = await _employeeRepository.update_employe(Am);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 

        [HttpPost]
        [Route("loginEmployee")]
        public async Task<ActionResult<NumeroEmployee>> loginEmployee(string email, string password)
        {
            try
            {
                var items = await _employeeRepository.login_employe( email, password);
                NumeroEmployee numeroEmployee = new NumeroEmployee();
                numeroEmployee.id = Int32.Parse(items[0].ok);
            return numeroEmployee;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getDetailEmployee")]
        public ActionResult<IEnumerable<ListeEmployee>> getDetailEmployee(string id)
        {

            var items = _employeeRepository.detail_Emp( id);
            items.Reverse();
            return Ok(items);

        }


    }
}