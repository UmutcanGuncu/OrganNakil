using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrganNakil.Domain.Entities;
namespace OrganNakil.Persistence.Context
{
    public class OrganNakilDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public OrganNakilDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Organ> Organs { get; set; }
        public DbSet<OrganDonationRequest> OrganDonationRequests { get; set; }
    }
}

