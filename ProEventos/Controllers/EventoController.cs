using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Models;
using ProEventos.Services;
using System;
using System.Threading.Tasks;

namespace ProEventos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventoController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _eventService.ExibirTodosEventos(true);
                if (eventos == null) return NotFound("Nenhum evento encontrado.");

                return Ok(eventos);
            }catch(Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar localizar eventos! Erro: {e.Message}");
            }                 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = await _eventService.ExibirPorId(id, true);
                if (evento == null) return NotFound("Evento não encontrado.");

                return Ok(evento);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar localizar o evento! Erro: {e.Message}");
            }
        }

        [HttpGet("{tema}/tema")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var eventos = await _eventService.ExibirEventosPorTema(tema, true);
                if (eventos == null) return NotFound("Não foi encontrado nenhum evento com o tema solicitado.");

                return Ok(eventos);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar localizar o evento! Erro: {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {             
            try
            {
                var evento = await _eventService.AdicionarEvento(model);
                if (evento == null) return BadRequest("Erro ao adicionar evento.");

                return Ok(evento);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar evento! Erro: {e.Message}");
            }            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento model)
        {
            try
            {
                var evento = await _eventService.AtualizarEvento(id, model);
                if (evento == null) return BadRequest("Erro ao atualizar evento.");

                return Ok(evento);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar o evento! Erro: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                return await _eventService.DeletarEvento(id) ?
                    Ok("Deletado!") :
                    BadRequest("Evento não deletado!");                    
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar o evento! Erro: {e.Message}");
            }
        }
    }
}
