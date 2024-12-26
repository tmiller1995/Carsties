using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace IdentityService.Data.ValueGenerators;

public sealed class GuidVersion7Generator : ValueGenerator<Guid>
{
    public override Guid Next(EntityEntry entry) => Guid.CreateVersion7();

    public override bool GeneratesTemporaryValues => false;
}