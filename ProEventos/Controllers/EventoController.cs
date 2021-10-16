using Microsoft.AspNetCore.Mvc;
using ProEventos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {

        public IEnumerable<Evento> _evento = new Evento[]
        {
            new Evento()
            {
                EventoId = 1,
                Tema = "Angular 11 e .NET 5",
                Local = "São Paulo",
                Lote = "1º",
                QtdPessoas = 250,
                DataEvento = DateTime.Now.AddDays(2).ToString(),
                ImgUrl = "Foto.png"
            },
            new Evento()
            {
                EventoId = 2,
                Tema = "Angular 11 e .NET 5",
                Local = "Rio de Janeiro",
                Lote = "1º",
                QtdPessoas = 250,
                DataEvento = DateTime.Now.AddDays(2).ToString(),
                ImgUrl = "Foto.png"
            }
        };   
    public EventoController()
        {
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return _evento;                 
        }

        [HttpGet("{id}")]
        public IEnumerable<Evento> GetById(int id)
        {
            return _evento.Where(evento => evento.EventoId == id);
        }

        [HttpPost]
        public string Post()
        {
            return "post";
        }

        [HttpPut("{id}")]
        public string Put(int id)
        {
            return $"value = {id}";
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return $"value = {id}";
        }
    }
}
