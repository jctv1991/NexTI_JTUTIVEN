using CPCore.Entidades;
using CPCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPAplicacion.Servicios
{
    public class EventoService
    {
        private readonly IEventoRepository _eventoRepository;

        public EventoService(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        public async Task<IEnumerable<Evento>> GetEventos()
        {
            return await _eventoRepository.GetEventos();
        }


        public async Task<bool> AgregarEvento(Evento evento)
        {
            // Lógica para insertar un evento
            try
            {
                await _eventoRepository.AgregarEvento(evento);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ActualizaEvento(Evento evento)
        {
            // Lógica para actualizar un evento
            try
            {
                await _eventoRepository.ActualizaEvento(evento);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EliminarEvento(int id)
        {
            // Lógica para eliminar un evento
            try
            {
                await _eventoRepository.EliminarEvento(id);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}

