using Microsoft.EntityFrameworkCore;
using SmartSchedulingApp.Notifications;
using SmartSchedulingApp.Doctors;
using SmartSchedulingApp.Schedules;
using SmartSchedulingApp.Timeslots;
using System.Reflection.Emit;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace SmartSchedulingApp.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class SmartSchedulingAppDbContext :
    AbpDbContext<SmartSchedulingAppDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion
    
    public DbSet<Person> Persons { get; set; }

    public SmartSchedulingAppDbContext(DbContextOptions<SmartSchedulingAppDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(SmartSchedulingAppConsts.DbTablePrefix + "YourEntities", SmartSchedulingAppConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});


        builder.Entity<Person>(b =>
        {
            b.ToTable(SmartSchedulingAppConsts.DbTablePrefix + "Persons", SmartSchedulingAppConsts.DbSchema);
            b.ConfigureByConvention();

            b.HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId);

            b.HasIndex(p => p.UserId);

            b.HasMany(p => p.Schedules)
            .WithOne();

            b.HasMany(p => p.Notifications)
            .WithOne();

            b.HasDiscriminator<string>("PersonType")
            .HasValue<Doctor>("Doctor")
            .HasValue<Staff>("Staff")
            .HasValue<Manager>("Manager");
        });
        builder.Ignore<Timeslot>();
        builder.Entity<Schedule>(b =>
        {
            b.ToTable(SmartSchedulingAppConsts.DbTablePrefix + "Schedules", SmartSchedulingAppConsts.DbSchema);
            b.ConfigureByConvention();
            b.OwnsOne(x => x.Timeslot);
        });
        builder.Entity<Notification>(b =>
        {
            b.ToTable(SmartSchedulingAppConsts.DbTablePrefix + "Notifications", SmartSchedulingAppConsts.DbSchema);
            b.ConfigureByConvention();
        });
    }
}
