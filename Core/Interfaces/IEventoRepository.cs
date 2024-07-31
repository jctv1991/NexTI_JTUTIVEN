using CPCore.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPCore.Interfaces
{
    public interface IEventoRepository
    {
        Task<IEnumerable<Evento>> GetEventos();
        Task AgregarEvento(Evento evento);
        Task ActualizaEvento(Evento evento);
        Task EliminarEvento(int id);
    }
}
