using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoldMineGuide.Models
{
    public class Flower
    {
        [Key]
        public int Mining_ID { get; set; }
        [Required]
        [Display(Name = "Mining Place")]
        public string Mining_Place { get; set; }
        
        [Required]
        [Display(Name = "Mining Name")]
        public string Method_Name { get; set; }
        
        [Required]
        [Display(Name = "Process Type")]
        public string Process_Type { get; set; }
        
        [Required]
        [Display(Name = "Business Type")]
        public string Business_Type { get; set; }
        

        [Required]
        [Display(Name = "Mining Produced Date")]
        public DateTime Mining_Produced_Date { get; set; }
    }
}
