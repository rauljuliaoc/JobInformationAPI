﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobInformationAPI.Models
{
    public partial class Information
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [Unicode(false)]
        public string Name { get; set; }
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }
        [Column("didFinish")]
        public bool? DidFinish { get; set; }
        public int? CountryId { get; set; }
        public int? JobId { get; set; }

        [ForeignKey("CountryId")]
        [InverseProperty("Information")]
        public virtual CountryInfo Country { get; set; }
        [ForeignKey("JobId")]
        [InverseProperty("Information")]
        public virtual JobInformation Job { get; set; }
    }
}