using exploralocalfinal.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exploralocalfinal.Servicios
{
    public interface IResenaRepository
    {
        Task<Resena> GuardarResenaAsync(Resena resena);
        Task<bool> ActualizarResenaAsync(int idResena, string nuevoTexto);
        Task<bool> EliminarResenaAsync(int idResena);

    }
}
