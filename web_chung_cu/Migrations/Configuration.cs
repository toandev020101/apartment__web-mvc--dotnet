namespace web_chung_cu.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<web_chung_cu.Models.DB_Entities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "web_chung_cu.Models.DB_Entities";
        }

        protected override void Seed(web_chung_cu.Models.DB_Entities context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
