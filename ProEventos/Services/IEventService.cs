using ProEventos.Models;
using ProEventos.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Services
{
    public class IEventService
    {
        private readonly IEventoRepository _eventoRepository;        
        private readonly IProEventosRepository _proEventosRepository;

        public IEventService(IEventoRepository eventoRepository, IProEventosRepository proEventosRepository)
        {
            _eventoRepository = eventoRepository;            
            _proEventosRepository = proEventosRepository;
        }

        public async Task<Evento> AdicionarEvento(Evento model)
        {
            try
            {
                _proEventosRepository.Add(model);
                if (await _proEventosRepository.SaveChangesAsync())
                {
                    return await _eventoRepository.GetEventoByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Evento> AtualizarEvento(int eventoId, Evento model)
        {
            try
            {
                var evento = await _eventoRepository.GetEventoByIdAsync(eventoId, false);
                if (evento == null) return null;

                model.Id = evento.Id;

                _proEventosRepository.Update(model);                
                if (await _proEventosRepository.SaveChangesAsync())
                {
                    return await _eventoRepository.GetEventoByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeletarEvento(int eventoId)
        {
            try
            {
                var evento = await _eventoRepository.GetEventoByIdAsync(eventoId, false);
                if (evento == null) throw new Exception("Evento não encontrado!");

                _proEventosRepository.Delete(evento);
                return await _proEventosRepository.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Evento[]> ExibirTodosEventos(bool includePalestrantes)
        {
            try
            {
                var eventos = await _eventoRepository.GetAllEventosAsync(includePalestrantes);
                if (eventos == null) return null;

                return eventos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Evento[]> ExibirEventosPorTema(string tema, bool includePalestrantes)
        {
            try
            {
                var eventos = await _eventoRepository.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if (eventos == null) return null;

                return eventos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Evento> ExibirPorId(int eventoId, bool includePalestrantes)
        {
            try
            {
                var eventos = await _eventoRepository.GetEventoByIdAsync(eventoId, includePalestrantes);
                if (eventos == null) return null;

                return eventos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

