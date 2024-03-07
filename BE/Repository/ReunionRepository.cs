using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using db_Api.Models.Generated;
using db_Api.Models.Generated.Reunion;
using static db_Api.Models.Generated.Db_PartialContext;

namespace db_Api.Repository.ReunionsRepository
{
    public class ReunionRepository : IReunionRepository
    {
        private readonly Db_PartialContext _Context;
        private readonly Db_PartialContext _PartialContext;

        public ReunionRepository(Db_PartialContext Context, Db_PartialContext PartialContext)
        {
            _Context = Context;
            _PartialContext = PartialContext;
        }

        public List<ListeReunion11> liste_reunion11(string? numReunion, string? type, string? lieu, string? site)
        {
            string ch = "exec usp_mobile_liste_reunion11 @numr=N'" + numReunion + "',@date1=N'',@date2=N'',@date_prev1=N''," +
                "@date_prev2=N'',@type=N'" + type + "',@lieu=N'" + lieu + "' ,@site=N'" + site + "'";
            var x = _PartialContext.ListeReunion11s.FromSqlRaw<ListeReunion11>(ch);
            x.Reverse();
            return x.ToList();
        }

        public async Task<List<OK>> add_reunion11(string? typeR, DateTime? date_prev, DateTime? date_Real,string? lieu, string? site)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@typeR", Value = typeR },
                        new SqlParameter { ParameterName = "@dateprev", Value = date_prev },
                        new SqlParameter { ParameterName = "@dateReal", Value = date_Real },
                        new SqlParameter { ParameterName = "@lieu", Value = lieu },
                        
                        new SqlParameter { ParameterName = "@site", Value = site },
                        new SqlParameter { ParameterName = "@matdecl", Value ="0603" }
                    };
            var response = await Task.Run(() =>
              _PartialContext.OKs.FromSqlRaw<OK>("exec usp_mobile_insertreunion11 @typeR,@dateprev,@dateReal,@lieu,@Site ,@matdecl", sqlParams.ToArray()).ToListAsync());
            return response;

        }

        public async Task<IsDone> update_reunion(updatereunion Am)
        {
            List<SqlParameter> sqlParms = new List<SqlParameter>
            
                    {
                        new SqlParameter { ParameterName = "@typeReunion", Value = Am.typeReunion },
                        new SqlParameter { ParameterName = "@dateprev", Value = Am.datePrev },
                        new SqlParameter { ParameterName = "@dateReal", Value = Am.dateReal },
                        new SqlParameter { ParameterName = "@nReunion", Value = Am.nReunion },
                        
                        new SqlParameter { ParameterName = "@lieu", Value = Am.lieu },
                        new SqlParameter { ParameterName = "@site", Value = Am.site }
                    };
            var result = await Task.Run(() => _PartialContext.IsDones
             .FromSqlRaw<IsDone>("exec usp_update_mobile_reunion @typeReunion, @dateprev, @dateReal, @nReunion,@lieu,@site", sqlParms.ToArray()).ToListAsync());
             return result.FirstOrDefault();

        }

        public Delete DeleteReunion(int nReunion)
        {
            string ch = $" exec Usp_mobile_delete_Reunion11 @nReunion={nReunion},@lang='fr'  ";
            var x = _PartialContext.Deletes.FromSqlRaw<Delete>(ch).ToList();
            return x.FirstOrDefault();
        }

    }
    }