using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Models.Entities;

namespace TaskManagementApp.Models.DataContext
{
    /* Uygulamanın veritabanı bağlantı sınıfı:
     * Fields:
     *          UserAccounts (Kullanıcı hesapları): UserAccount modeli verileri,
     *          Tasks(Görevler): TaskItem modeli verileri
     * Methods:
     *          Constructor: Uygulama-veritabanı bağlamını DbContextOptions ile gerekli ayarları ile oluşturur 
    */
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
