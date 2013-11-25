using System.Collections.Generic;
using System.Data.Entity;
using H5Forms.Entities;

namespace H5Forms.EfRepository
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<H5FormsContext>
    {
        protected override void Seed(H5FormsContext context)
        {
            var users = new List<User>
            {
                new User { Nick = "Test"}
            };

            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();
        }
    }
}
