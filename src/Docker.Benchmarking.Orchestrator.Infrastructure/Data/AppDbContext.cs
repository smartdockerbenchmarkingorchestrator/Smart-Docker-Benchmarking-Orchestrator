using Microsoft.EntityFrameworkCore;
using System.Linq;
using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using Docker.Benchmarking.Orchestrator.Core.SharedKernel;
using Docker.Benchmarking.Orchestrator.Core.Entities;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Docker.Benchmarking.Orchestrator.Infrastrcture.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IDomainEventDispatcher _dispatcher;

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            //_dispatcher = dispatcher;

            //if (System.Diagnostics.Debugger.IsAttached == false)
            //{
            //    System.Diagnostics.Debugger.Launch();
            //}
        }

        //DbSets for Context
        public DbSet<DockerHost> DockerHosts { get; set; }
        public DbSet<DockerImage> DockerImage { get; set; }
        public DbSet<ContainerMetric> ContainerMetrics { get; set; }
        public DbSet<BenchmarkExperiment> BenchmarkExperiments { get; set; }
        public DbSet<Application> Application { get; set; }
        public DbSet<BenchmarkTestItem> BenchmarkTestItems { get; set; }
        public DbSet<ApacheJmeterTestFile> ApacheJmeterTestFiles { get; set; }
        public DbSet<AzureHost> AzureHosts { get; set; }
        public DbSet<AzureCredientials> AzureCredentials { get; set; }
        public DbSet<AWSCredentials> AWSCredentials { get; set; }
        public DbSet<AWSCloudFormationTemplate> AWSTemplates { get; set; }
        public DbSet<AWSCloudFormationParameter> AWSTemplateParameters { get; set; }
        public DbSet<AWSHost> AWSHosts { get; set; }
        public DbSet<DockerImageVariable> DockerImageVariables { get; set; }
        public DbSet<ApplicationTestVariable> ApplicationTestPlanVariables { get; set; }
        public DbSet<BenchmarkExperimentVariable> BenchmarkExperimentVariables { get; set; }
        public DbSet<CSVUpload> CSVUploads { get; set; }

        //Override Default Save
        public override int SaveChanges()
        {
            int result = base.SaveChanges();

            // dispatch events only if save was successful
            var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
                .Select(e => e.Entity)
                .Where(e => e.Events.Any())
                .ToArray();

            foreach (var entity in entitiesWithEvents)
            {
                var events = entity.Events.ToArray();
                entity.Events.Clear();
                foreach (var domainEvent in events)
                {
                    //_dispatcher.Dispatch(domainEvent);
                }
            }

            return result;
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {

            //DateTime now = DateTime.UtcNow;
            //foreach (ObjectStateEntry entry in (this as IObjectContextAdapter).ObjectContext.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified))
            //{
            //    if (!entry.IsRelationship)
            //    {
            //        IHasLastModified lastModified = entry.Entity as IHasLastModified;
            //        if (lastModified != null)
            //            lastModified.LastModified = now;
            //    }
            //}

            int result = await base.SaveChangesAsync();

            // dispatch events only if save was successful
            var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
                .Select(e => e.Entity)
                .Where(e => e.Events.Any())
                .ToArray();

            foreach (var entity in entitiesWithEvents)
            {
                var events = entity.Events.ToArray();
                entity.Events.Clear();
                foreach (var domainEvent in events)
                {
                    _dispatcher.Dispatch(domainEvent);
                }
            }

            return result;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DataSeed(modelBuilder);

            //modelBuilder.Entity<DockerImage>()
            //    .HasIndex(b => b.Name)
            //    .IsUnique()
            //    .HasFilter(null);

            //modelBuilder.Entity<DockerImage>().Option(r => r.Payment)
            //.WithMany()
            //.HasForeignKey(r => r.PaymentId);

            modelBuilder.Entity<DockerHost>()
            .HasOne(p => p.AzureHost)
            .WithOne(i => i.DockerHost)
            .HasForeignKey<AzureHost>(b => b.DockerHostId);

            modelBuilder.Entity<DockerHost>()
.HasOne(p => p.AWSHost)
.WithOne(i => i.DockerHost)
.HasForeignKey<AWSHost>(b => b.DockerHostId);
        }

        private void DataSeed(ModelBuilder modelBuilder)
        {

        }
    }
}
