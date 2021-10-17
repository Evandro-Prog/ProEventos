using Microsoft.EntityFrameworkCore;
using ProEventos.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Repository.Impl
{
    public class IPalestranteRepositoryImpl : IPalestranteRepository
    {
        private readonly ProEventosContext _context;
        public IPalestranteRepositoryImpl(ProEventosContext context)
        {
            _context = context;
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool incluirEventos)
        {
            IQueryable<Palestrante> querry = _context.Palestrantes
            .Include(p => p.RedesSociais);

            if (incluirEventos)
            {
                querry = querry.Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            querry = querry.OrderBy(p => p.Id);

            return await querry.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool incluirEventos)
        {
            IQueryable<Palestrante> querry = _context.Palestrantes
            .Include(p => p.RedesSociais);

            if (incluirEventos)
            {
                querry = querry.Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            querry = querry.OrderBy(p => p.Id).Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

            return await querry.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool incluirEventos)
        {
            IQueryable<Palestrante> querry = _context.Palestrantes
            .Include(p => p.RedesSociais);

            if (incluirEventos)
            {
                querry = querry.Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            querry = querry.OrderBy(p => p.Id).Where(p => p.Id == palestranteId);

            return await querry.FirstOrDefaultAsync();
        }
    }   
}
