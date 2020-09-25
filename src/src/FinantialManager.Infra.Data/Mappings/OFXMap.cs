using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FinantialManager.Domain.Models;

namespace FinantialManager.Infra.Data.Mappings
{    
    public class OFXMap : IEntityTypeConfiguration<OFX>
    {
        public void Configure(EntityTypeBuilder<OFX> builder)
        {
        }
    }
}
