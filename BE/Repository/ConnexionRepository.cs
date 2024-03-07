using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using db_Api.Models.Generated;
using db_Api.Models.Generated.Connexion;
using static db_Api.Models.Generated.Db_PartialContext;

namespace db_Api.Repository.ConnexionRepository
{
    public class ConnexionRepository : IConnexionRepository
    {
        private readonly Db_PartialContext _Context;
        private readonly Db_PartialContext _PartialContext;

        public ConnexionRepository(Db_PartialContext Context, Db_PartialContext PartialContext)
        {
            _Context = Context;
            _PartialContext = PartialContext;
        }

        

    }
    }