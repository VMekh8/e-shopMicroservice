﻿using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Data
{
    public static class MigrationExtension
    {
        public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<DiscountContext>();

            dbContext.Database.MigrateAsync();
            
            return app;
        }
    }
}
