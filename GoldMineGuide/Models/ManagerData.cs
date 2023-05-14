using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoldMineGuide.Models
{
    public class ManagerData
    {

        public int Mining_ID { get; set; }
       
        public string Mining_Place { get; set; }

     
        public string MethodInfoName { get; set; }

  
        public string ProcessType { get; set; }


        public string Business_Type { get; set; }


        public DateTime MiningProducedDate { get; set; }
    }
}
