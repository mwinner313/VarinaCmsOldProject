﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.DomainClasses.Contracts
{
    public interface IVisibliltyControlled
    {
        bool  IsVisible { get; set; }
    }
}
