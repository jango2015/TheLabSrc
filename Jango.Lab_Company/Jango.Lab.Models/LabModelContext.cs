using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jango.Lab.Models
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class LabModels : DbContext
    {
        public LabModels() : base("LbDbInfo")
        {
        }

        public DbSet<ChargeCard> ChargeCard { get; set; }
        public DbSet<ChargeRecord> ChargeRecords { get; set; }
        public DbSet<Coacher> Coachers { get; set; }
        public DbSet<CourseInfo> CourseInfos { get; set; }
        public DbSet<CourseReserveRecord> CourseReserveRecords { get; set; }
        public DbSet<CourseSignInRecord> CourseSignInRecords { get; set; }
        public DbSet<CourseTeller> CourseTellers { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<UserConsigneeInfo> UserConsigneeInfos { get; set; }

        public DbSet<CourseCategory> CourseCategories { get; set; }

        public DbSet<CourseCoacher> CourseCoachers { get; set; }
    }
}
