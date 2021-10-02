using MITIENDA.BlazorServer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MITIENDA.BlazorServer.Data.Models
{
    public class RegistroUsuarioModels : Usuario
    {
        public string ConfirmPassword { get; set; }
    }
}
