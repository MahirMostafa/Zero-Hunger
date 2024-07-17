using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zero_Hunger.DTOS
{
    public class CollectRequestDTO
    {
        [Required]
        public string FoodName { get; set; }
        [Required]
        public int FoodQuantity { get; set; }
        [Required]
        public string FoodType { get; set; }
        [Required]
        public System.DateTime MaxPreserveTime { get; set; }

        [Required]
        public int RestaurantId { get; set;}

       
    }
}