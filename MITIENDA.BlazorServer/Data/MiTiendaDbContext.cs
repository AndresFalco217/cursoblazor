using Microsoft.EntityFrameworkCore;
using MITIENDA.BlazorServer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MITIENDA.BlazorServer.Data
{
    public class MiTiendaDbContext : DbContext
    {
        public MiTiendaDbContext(DbContextOptions<MiTiendaDbContext> options) : base(options)
        {
                
        }

        public DbSet<Categoria> Categorias{ get; set; }
        public DbSet<Cliente> Clientes{ get; set; }
        public DbSet<DetalleFactura> DetalleFacturas{ get; set; }
        public DbSet<Factura> Facturas{ get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios{ get; set; }


        //CONFIGURACION DE LAS TABLAS --- MAPEAR RELACIONES EN EL CONTEXTO CON LA BASE DE DATOS.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Categoria
            var categorias = modelBuilder.Entity<Categoria>();
            categorias.HasKey(x=>x.Id); //Id de la tabla categoria.
            categorias.Property(x=>x.Id).ValueGeneratedOnAdd();//Decir que es autoincrementable.
            //Tiene una relacion de uno a muchos; los productos se relacionan con muchas categorias
            categorias.HasMany(x => x.Productos) //Relacion con la tabla productos
                .WithOne(x => x.Categoria)
                .HasForeignKey(x => x.IdCategoria); //El Id con el que se relaciona

            //Clientes
            var clientes = modelBuilder.Entity<Cliente>();
            clientes.HasKey(x => x.Id); 
            clientes.Property(x => x.Id).ValueGeneratedOnAdd();

            clientes.HasMany(x => x.Facturas)
                .WithOne(x => x.Cliente)
                .HasForeignKey(x => x.IdCliente);

            //Facturas
            var facturas = modelBuilder.Entity<Factura>();
            facturas.HasKey(x => x.Id);
            facturas.Property(x => x.Id).ValueGeneratedOnAdd();

            facturas.HasMany(x => x.DetalleFacturasc)
                .WithOne(x => x.Factura)
                .HasForeignKey(x => x.IdFactura);

            //Productos
            var productos = modelBuilder.Entity<Producto>();
            productos.HasKey(x => x.Id);
            productos.Property(x => x.Id).ValueGeneratedOnAdd();

            productos.HasMany(x => x.DetalleFacturas)
                .WithOne(x => x.Producto)
                .HasForeignKey(x => x.IdProducto);

            //Rol
            var roles = modelBuilder.Entity<Rol>();
            roles.HasKey(x => x.Id);
            roles.Property(x => x.Id).ValueGeneratedOnAdd();

            roles.HasMany(x => x.Usuarios)
            .WithOne(x => x.Rol)
            .HasForeignKey(x => x.IdRol);

            //DetalleFactura
            //Se esctrutura de esta manera porque no dependen de otra tabla.
            var detalles = modelBuilder.Entity<DetalleFactura>();
            detalles.HasKey(x => x.Id);
            detalles.Property(x => x.Id).ValueGeneratedOnAdd();

            //Usuarios
            var usuarios = modelBuilder.Entity<Usuario>();
            usuarios.HasKey(x => x.Id);
            usuarios.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
