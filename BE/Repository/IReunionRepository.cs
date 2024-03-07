using db_Api.Models.Generated;
using db_Api.Models.Generated.Reunion;

namespace db_Api.Repository.ReunionsRepository
{
    public interface IReunionRepository
    {
        List<ListeReunion11> liste_reunion11(string? numReunion, string? type, string? lieu, string? site);
        
        Task<List<OK>> add_reunion11(string? codetypeR, DateTime? date_prev, DateTime? date_Real,string? lieu, string? site);
        Delete DeleteReunion(int nReunion);

        Task<IsDone> update_reunion(updatereunion Am);
    }
}
