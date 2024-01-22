using Microsoft.EntityFrameworkCore;
using MaApiCrud.Models;

namespace MaApiCrud.Bdd
{
    public class ConnectBdd : DbContext
    {
        public ConnectBdd(DbContextOptions<ConnectBdd> options) : base(options)
        {
        }

        public DbSet<Produit> Produits { get; set; }
        public DbSet<Categorie> Categorie { get; set; }
    }
}