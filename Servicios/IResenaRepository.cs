﻿using exploralocalfinal.Modelos;
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
    
       
    }
}
