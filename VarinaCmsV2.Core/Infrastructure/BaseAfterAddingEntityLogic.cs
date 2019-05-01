﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using StructureMap.Attributes;

namespace VarinaCmsV2.Core.Infrastructure
{
    public abstract  class BaseAfterAddingEntityLogic
    {
        [SetterProperty]
        public IContainer Container { get; set; }
        public abstract Task ApplyLogicAsync<T>(T obj,IPrincipal requestOwner);
    }
}
