using HIS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.DAL.Configurations;

public class ServiceTypeConfiguration : IEntityTypeConfiguration<ServiceType>
{
    public void Configure(EntityTypeBuilder<ServiceType> builder)
    {
        builder.ToTable("service_types");

        builder.HasKey(st => st.Id);
        builder.Property(st => st.Id).ValueGeneratedNever();
        builder.Property(st => st.Code).HasMaxLength(20).IsRequired();
        builder.HasIndex(st => st.Code).IsUnique();
        builder.Property(st => st.Name).HasMaxLength(50).IsRequired();

        builder.HasData(
            new ServiceType { Id = (int)ServiceTypeId.Examination, Code = "EXAMINATION", Name = "Khám bệnh" },
            new ServiceType { Id = (int)ServiceTypeId.LabTest, Code = "LAB_TEST", Name = "Xét nghiệm" },
            new ServiceType { Id = (int)ServiceTypeId.Imaging, Code = "IMAGING", Name = "Chẩn đoán hình ảnh" }
        );
    }
}
