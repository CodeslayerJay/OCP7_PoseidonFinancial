﻿using System;
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
        public string Account { get; set; }

        public string Type { get; set; }

        public string BidQuantity { get; set; }
    }

}
