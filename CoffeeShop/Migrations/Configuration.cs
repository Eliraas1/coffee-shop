namespace CoffeeShop.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CoffeeShop.Dal.OrdersDal>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "CoffeeShop.Dal.OrdersDal";
        }

        protected override void Seed(CoffeeShop.Dal.OrdersDal context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
