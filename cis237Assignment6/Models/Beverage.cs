//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace cis237Assignment6.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Beverage
    {
        [Required(ErrorMessage = "ID is required")]
        public string id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; }

        [Required(ErrorMessage = "Pack is required")]
        public string pack { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal price { get; set; }

        public bool active { get; set; }
    }
}