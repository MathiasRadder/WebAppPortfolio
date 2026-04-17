using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataPortfolio.Models;

public partial class ProjectsWebContext : DbContext
{
    public virtual DbSet<Icon> Icon { get; set; }
    public virtual DbSet<IconBridge> IconBridge { get; set; }
    public virtual DbSet<PageSection> PageSection { get; set; }

    public virtual DbSet<ProjectPage> ProjectPage { get; set; }

    public virtual DbSet<ProjectType> ProjectType { get; set; }

    public virtual DbSet<SectionType> SectionType { get; set; }

    public virtual DbSet<TextBox> TextBox { get; set; }

    public virtual DbSet<TextPart> TextPart { get; set; }

    public ProjectsWebContext()
    {
    }

    public ProjectsWebContext(DbContextOptions<ProjectsWebContext> options)
        : base(options)
    {
    }

   
}
