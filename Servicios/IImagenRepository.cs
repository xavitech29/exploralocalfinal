using exploralocalfinal.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exploralocalfinal.Servicios
{
    public interface IImagenRepository
    {
        Task<string> SubirImagenAsync(byte[] imagenBytes);
        Task<bool> ActualizarImagenAsync(int id, string nuevaUrlImagen);
        Task<bool> EliminarImagenAsync(int id);
    }
}
