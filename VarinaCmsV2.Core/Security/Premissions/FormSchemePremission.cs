

using System.Collections.Generic;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Security.Premissions
{
    public partial class FormSchemePremission : IPremissionList
    {

        public const string CanSee = "formScheme.see";
        public const string CanAdd = "formScheme.add";
        public const string CanUpdate = "formScheme.update";
        public const string CanDelete = "formScheme.delete";
        public List<string> List { get; } = new List<string>()
        {
          CanSee,
          CanAdd,
          CanUpdate,
          CanDelete
        };
    }
}
