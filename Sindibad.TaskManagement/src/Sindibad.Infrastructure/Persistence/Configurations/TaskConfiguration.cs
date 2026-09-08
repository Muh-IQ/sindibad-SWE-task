using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sindibad.Infrastructure.Persistence.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<Domain.Entities.Task>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Task> builder)
    {
        builder.ToTable("Tasks");

        builder.HasKey(task => task.Id);

        builder.Property(task => task.Id)
            .ValueGeneratedNever();

        builder.Property(task => task.ProjectId)
            .IsRequired();

        builder.Property(task => task.Title)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(task => task.Completed)
            .IsRequired();

        builder.Property(task => task.CreatedAt)
            .IsRequired();

        builder.HasIndex(task => task.ProjectId);
    }
}
