using Blaze.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blaze.Domain.Entities
{
    public class Company : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? LogoUrl { get; set; }
        public string? Address { get; set; }
        public string? Website { get; set; }

        public string? LinkedInUrl { get; set; }
        public string? FacebookUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? InstagramUrl { get; set; }

        public string PhoneNumber { get; set; }
        public string CompanyEmail { get; set; }
        public DateTime? EstablishedDate { get; set; }
        public int CompanySize { get; set; } =0;
        public string Industry { get; set; }
        public string? About { get; set; }

        // Added fields
        public string? Mission { get; set; }
        public string? Vision { get; set; }
        public string? CoreValues { get; set; }
        public int? NumberOfOffices { get; set; }
        public string? HeadquartersLocation { get; set; }

    }
}