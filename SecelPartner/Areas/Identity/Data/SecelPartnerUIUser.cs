using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SecelPartner.Core.Entities;

namespace SecelPartner.UI.Areas.Identity.Data;

// Add profile data for application users by adding properties to the SecelPartnerUIUser class
public class SecelPartnerUIUser : IdentityUser
{
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string? FirstName { get; set; }
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string? LastName { get; set; }
    [PersonalData]
    [DefaultValue("avatar.png")]
    public string? ProfilName { get; set; }
    [DefaultValue("Fichier\avatar.png")]
    public string? ProfilPath { get; set; }
    [NotMapped]
    public IFormFile? Profil { get; set; }
}

