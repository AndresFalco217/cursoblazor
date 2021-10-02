using MITIENDA.BlazorServer.Data.Entities;
using MITIENDA.BlazorServer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MITIENDA.BlazorServer.Data.Services
{
    public class UsuariosServices
    {
        private readonly MiTiendaDbContext _context;

        public UsuariosServices(MiTiendaDbContext context)
        {
            _context = context;
        }
        public MsgResult Registrar(RegistroUsuarioModels usuario)
        {
            var res = new MsgResult();

            //Verificar que no exista otro usuario con el mismo email
            var newUser = _context.Usuarios.FirstOrDefault(x => x.Email == usuario.Email);
            if (newUser != null)
            {
                res.IsSuccess = false;
                res.Message = "Ya existe un usuario con este email";
                return res;
            }

            //CON ESTE INSTANCIA CREAMOS UN NUEVO USUARIO.
                newUser = new Usuario
            {
                IdRol = usuario.IdRol,
                Email = usuario.Email,
                Clave = usuario.Clave,
                Nombre = usuario.Nombre,
            };

            _context.Usuarios.Add(usuario); //Trae los datos que se le asigna al usuario de la vista
            try
            {
                _context.SaveChanges(); //Guardar en la base de datos.
                res.IsSuccess = true;
                res.Message = "Usuario registrado correctamente";
            }
            catch (Exception ex)
            {
                res.IsSuccess = true;
                res.Message = "Error al registrar el usuario";
                res.Error = ex;
            }

            return res;
        }

    }
}
