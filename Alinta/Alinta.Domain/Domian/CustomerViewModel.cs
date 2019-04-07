using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace Alinta.Core.Domain
{
    /// <summary>
    /// A account with users
    /// </summary>
    public class CustomerViewModel : BaseDomain
    {
        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(30)]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
    }

}


