using Microsoft.EntityFrameworkCore;
using ProEventos.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Repository.Impl
{
    public class IEventoRepositoryImpl : IEventoRepository
    {
        private readonly ProEventosContext _context;
        public IEventoRepositoryImpl(ProEventosContext context)
        {
            _context = context;            
        }

        public async Task<Evento[]> GetAllEventosAsync(bool incluirPalestrantes = false)
        {
            IQueryable<Evento> querry = _context.Eventos
                .Include(e => e.Lote)
                .Include(e => e.RedesSociais);

            if (incluirPalestrantes)
            {
                querry = querry.Include(e => e.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            querry = querry.AsNoTracking().OrderBy(e => e.Id);

            return await querry.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool incluirPalestrantes = false)
        {
            IQueryable<Evento> querry = _context.Eventos
                .Include(e => e.Lote)
                .Include(e => e.RedesSociais);

            if (incluirPalestrantes)
            {
                querry = querry.Include(e => e.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            querry = querry.AsNoTracking().OrderBy(e => e.Id)
                .Where(e => e.Tema.ToLower().Contains(tema.ToLower()));

            return await querry.ToArrayAsync();
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool incluirPalestrantes)
        {
            IQueryable<Evento> querry = _context.Eventos
                .Include(e => e.Lote)
                .Include(e => e.RedesSociais);

            if (incluirPalestrantes)
            {
                querry = querry.Include(e => e.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            querry = querry.AsNoTracking().OrderBy(e => e.Id)
                .Where(e => e.Id == eventoId);

            return await querry.FirstOrDefaultAsync();
        }
    }
}
