using System;
using System.Collections;
using System.Collections.Generic;

namespace VarinaCmsV2.Common
{
    public interface IHaveChild<T> where T :class
    {
        List<T> Childs { get; set; }      
        
    }
    public interface IHaveCollectionChild<T> where T : class
    {
        ICollection<T> Childs { get; set; }
        Guid Id { get; set; }

    }
}