using ProEventos.Models;
using System.Threading.Tasks;

namespace ProEventos.Repository
{
    public interface IPalestranteRepository 
    {
        //PALESTRANTES
        Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string tema, bool incluirEventos = false);
        Task<Palestrante[]> GetAllPalestrantesAsync(bool incluirEventos);
        Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool incluirEventos = false);
    }
}
