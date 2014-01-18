using System.Data.Entity;

namespace H5Forms.EfRepository
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<H5FormsContext>
    {
        protected override void Seed(H5FormsContext context)
        {
            //var users = new List<User>
            //{
            //    new User { Nick = "Test", CreateDate = DateTime.Now, UpdateDate = DateTime.Now}
            //};

            //users.ForEach(u => context.Users.Add(u));
            //context.SaveChanges();
        }
    }
}
