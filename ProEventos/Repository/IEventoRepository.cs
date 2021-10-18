using ProEventos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Repository
{
    public interface IEventoRepository 
    {
        //EVENTOS
        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool incluirPalestrantes = false);
        Task<Evento[]> GetAllEventosAsync(bool incluirPalestrantes = false);
        Task<Evento> GetEventoByIdAsync(int eventoId, bool incluirPalestrantes = false);
    }
}
