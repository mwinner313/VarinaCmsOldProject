using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.Core.Services.FormScheme
{
    public interface IFormSchemeService :
        IService
        <FormSchemeGetRequest, FormSchemeGetListRequest, FormSchemeAddReqest, FormSchemeEditRequest,
            FormSchemeDeleteRequest, FormSchemeResponse, FormSchemeListReponse>
    {
    }
}