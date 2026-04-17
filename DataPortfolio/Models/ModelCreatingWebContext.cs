using Microsoft.EntityFrameworkCore;

namespace DataPortfolio.Models
{
    public partial class ProjectsWebContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Icon>(entity =>
            {
                entity.HasKey(e => e.IconId).HasName("PK__Icon__43C7AD0FAD34AF0D");

                entity.ToTable("Icon");

                entity.Property(e => e.Addtext).HasMaxLength(100);
                entity.Property(e => e.IconName).HasMaxLength(50);
                entity.Property(e => e.Link).HasMaxLength(150);
            });

            modelBuilder.Entity<IconBridge>(entity =>
            {
                entity.HasKey(e => e.IconBridgeId).HasName("PK__IconBrid__4D21E2A185FD7B3D");

                entity.ToTable("IconBridge");

                entity.HasOne(d => d.Icon).WithMany(p => p.IconBridges)
                    .HasForeignKey(d => d.IconId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__IconBridg__IconI__5DCAEF64");

                entity.HasOne(d => d.Page).WithMany(p => p.IconBridges)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__IconBridg__PageI__5EBF139D");
            });

            modelBuilder.Entity<PageSection>(entity =>
            {
                entity.HasKey(e => e.SectionId).HasName("PK__PageSect__80EF0872B04C9D4E");

                entity.ToTable("PageSection");

                entity.Property(e => e.ImageName).HasMaxLength(75);
                entity.Property(e => e.Title).HasMaxLength(75);
                entity.Property(e => e.ImageFileType).HasMaxLength(50);

                entity.HasOne(d => d.Page).WithMany(p => p.PageSections)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PageSecti__PageI__5165187F");

                entity.HasOne(d => d.Type).WithMany(p => p.PageSections)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PageSecti__TypeI__52593CB8");
            });

            modelBuilder.Entity<ProjectPage>(entity =>
            {
                entity.HasKey(e => e.PageId).HasName("PK__ProjectP__C565B1043DF3B41B");

                entity.ToTable("ProjectPage");

                entity.Property(e => e.ImageName).HasMaxLength(75);
                entity.Property(e => e.ProjectDescription).HasMaxLength(200);
                entity.Property(e => e.Title).HasMaxLength(75);
                entity.Property(e => e.ImageFolder).HasMaxLength(75);

                entity.HasOne(d => d.Type).WithMany(p => p.ProjectPages)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProjectPa__TypeI__4E88ABD4");
            });

            modelBuilder.Entity<ProjectType>(entity =>
            {
                entity.HasKey(e => e.TypeId).HasName("PK__ProjectT__516F03B5DE65290B");

                entity.ToTable("ProjectType");

                entity.Property(e => e.Type)
                    .HasMaxLength(75)
                    .HasColumnName("ProjectType");
            });

            modelBuilder.Entity<SectionType>(entity =>
            {
                entity.HasKey(e => e.TypeId).HasName("PK__SectionT__516F03B59FC5F293");

                entity.ToTable("SectionType");

                entity.Property(e => e.Type)
                    .HasMaxLength(75)
                    .HasColumnName("SectionType");
            });

            modelBuilder.Entity<TextBox>(entity =>
            {
                entity.HasKey(e => e.TextBoxId).HasName("PK__TextBox__17E2CA64FCEB2701");

                entity.ToTable("TextBox");

                entity.Property(e => e.Title).HasMaxLength(75);

                entity.HasOne(d => d.Section).WithMany(p => p.TextBoxes)
                    .HasForeignKey(d => d.SectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TextBox__Section__5535A963");
            });

            modelBuilder.Entity<TextPart>(entity =>
            {
                entity.HasKey(e => new { e.TextBoxId, e.SortOrder }).HasName("PK__TextPart__62B8D35FB62AE510");

                entity.ToTable("TextPart");

                entity.Property(e => e.TextBody).HasMaxLength(1024);

                entity.HasOne(d => d.Icon).WithMany(p => p.TextParts)
                    .HasForeignKey(d => d.IconId)
                    .HasConstraintName("FK__TextPart__IconId__5AEE82B9");

                entity.HasOne(d => d.TextBox).WithMany(p => p.TextParts)
                    .HasForeignKey(d => d.TextBoxId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TextPart__TextBo__5812160E");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
