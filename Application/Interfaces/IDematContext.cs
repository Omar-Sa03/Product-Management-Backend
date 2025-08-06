using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IDematContext : IContext
    {
        public DbSet<Test> Tests { get; set; }
        public DbSet<Perfume> Perfumes { get; set; }


    }
}
