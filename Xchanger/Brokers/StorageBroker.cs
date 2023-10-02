using System.Linq;
using System.Threading.Tasks;
using EFxceptions;
using Microsoft.EntityFrameworkCore;
using Xchanger.Models;

namespace Tarteeb.Importer.Brokers.Storages
{
    public class StorageBroker : EFxceptionsContext
    {
        public DbSet<Students> Students { get; set; }

        public StorageBroker() =>
            this.Database.EnsureCreated();

        public async Task<Students> InsertStudentAsync(Students student)
        {
            await this.Students.AddAsync(student);
            await this.SaveChangesAsync();

            return student;
        }

        public IQueryable<Students> SelectAllStudentAsync() =>
            this.Students.AsQueryable();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Data Source=..\\..\\..\\Student.DB";
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            optionsBuilder.UseSqlite(connectionString);
        }
    }
}
