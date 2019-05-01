using System;
using System.Collections.Generic;
using DotLiquid;
using VarinaCmsV2.Common;
using VarinaCmsV2.ViewModel.User.Account;

namespace VarinaCmsV2.ViewModel.Comment
{
    public class CommentLiquidViewModel : Drop
    {
        public string Text { get; set; }
        public Guid Id { get; set; }
        public LiquidDateTime CreateDateTime { get; set; }
        public UserLiquidViewModel Creator { get; set; }
        public List<CommentLiquidViewModel> Childs { get; set; }
    }
}