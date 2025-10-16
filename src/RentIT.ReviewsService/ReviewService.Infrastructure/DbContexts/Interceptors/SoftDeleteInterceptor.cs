using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ReviewServices.Core.Domain.Interfaces;
namespace ReviewServices.Infrastructure.DbContexts.Interceptors;

public class SoftDeleteInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is null) return result;

        foreach(var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry is not { State: EntityState.Deleted, Entity: ISoftDelete delete })
                continue;

            entry.State = EntityState.Modified;
            delete.IsActive = false;
            delete.DateDeleted = DateTime.UtcNow;
        }

        return result;
    }
}

