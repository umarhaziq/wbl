using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using wbl.Models;

namespace wbl.Models.ViewModels
{
    public class ChildViewModel
    {
        public int ChildId { get; set; }

        [Display(Name = "IC Number")]
        public string Child_IC { get; set; }

        [Required(ErrorMessage = "Child Name is required")]
        [Display(Name = "Full Name")]
        public string Child_Name { get; set; }

        [Required(ErrorMessage = "Birthplace is required")]
        [Display(Name = "Birth Place")]
        public string Child_Birthplace { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime Child_DOB { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        [Display(Name = "Gender")]
        public string Child_Gender { get; set; }

        [Required(ErrorMessage = "Nationality is required")]
        [Display(Name = "Nationality")]

        public string Child_Nationality { get; set; }

        [Required(ErrorMessage = "Race is required")]
        [Display(Name = "Race")]

        public string Child_Race { get; set; }

        [Required(ErrorMessage = "Religion is required")]
        [Display(Name = "Religion")]

        public string Child_Religion { get; set; }

        [Required(ErrorMessage = "Program is required")]
        [Display(Name = "Program")]
        public int ProgramId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Category")]
        public string? Program_Category { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        public IEnumerable<SelectListItem>? ProgramsList { get; set; }

        [Display(Name = "Address")]
        public string Child_Address { get; set; }

        [Display(Name = "Father IC")]
        public string FatherIc { get; set; }


        public class CustomSelectListItem : SelectListItem
        {
            public string Program_Category { get; set; }
            public decimal Price { get; set; }
        }


    }
}

