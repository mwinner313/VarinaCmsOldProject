﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.ViewModel.Eshop.Review
{
    public class ReviewAddOrUpdateViewModel:BaseVeiwModel
    {
        public Guid ProductId { get; set; }
        public bool IsApproved { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
    }
}
