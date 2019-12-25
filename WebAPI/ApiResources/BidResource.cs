using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ApiResources
{
    public class BidResource
    {
        public int BidListId { get; set; }
        public string Account { get; set; }
        public string Type { get; set; }
        public double BidQuantity { get; set; }
    }

    public class EditBidResource
    {
        [Required]
        [StringLength(20)]
        public string Account { get; set; }

        [Required]
        [StringLength(20)]
        public string Type { get; set; }

        [RegularExpression("([0-9]+)", ErrorMessage = "Must be a number 0-9")]
        public double BidQuantity { get; set; }
    }

}
