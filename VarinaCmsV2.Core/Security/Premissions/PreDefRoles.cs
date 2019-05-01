using System.Collections.Generic;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Security.Premissions
{
  public  class PreDefRoles:IRoleList
  {
      public const string Developer = "Developer";
      public const string PrincipalAdministrator = "PrincipalAdministrator";
      public const string Administrator = "Administrator";
      public const string Supervisor = "Supervisor";
      public const string Author = "Author";
      public const string WebSiteUser = "WebSiteUser";
      public const string Guest = "Guest";
      public List<string> List { get; set; }=new List<string>()
      {
          PrincipalAdministrator,
          Developer,
          Supervisor,
          Author,
          WebSiteUser,
          Administrator,
          Guest,
      };
  }
}
