using Consultorio.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Consultorio.Map
{
    public class ProfissionalMap : BaseMap<Profissional>
    {
        public ProfissionalMap() : base("tb_profissional")
        {
        }

        public override void Configure(EntityTypeBuilder<Profissional> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Nome).HasColumnName("nome").HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.Ativo).HasColumnName("ativa").HasDefaultValue(true);
        }
    }
}
