﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ApiResources
{
    public class CurveResource
    {
        public int Id { get; set; }
        public int CurveId { get; set; }
        public double Term { get; set; }
        public double Value { get; set; }
    }

    public class EditCurveResource
    {
        public string CurveId { get; set; }
        public string Term { get; set; }
        public string Value { get; set; }
    }

    
}
