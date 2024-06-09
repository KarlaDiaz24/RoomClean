using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;

namespace RoomClean.Context
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Evidencia> Evidencias { get; set; }
        public DbSet<Foto> Fotos { get; set; }

        public static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    Nombre = "SuperUser",
                    Apellido = "1",
                    Número = "9983546731",
                    Correo = "superuser1@hotmail.com",
                    Contraseña = ComputeSha256Hash("12345678"),
                    Foto = "",
                    FKRol = 1
                });

            modelBuilder.Entity<Roles>().HasData(
                new Roles { Id = 1, Nombre = "Admin" },
                new Roles { Id = 2, Nombre = "Empleado" });
        }
    }
}
