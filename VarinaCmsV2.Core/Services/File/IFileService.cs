using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;

namespace VarinaCmsV2.Core.Services.File
{
    public interface IFileService:IService<FileGetRequest,FileGetListRequest,FileAddRequest,FileEditRequest,FileDeleteRequest,FileResponse,FileListResponse>
    {
       
    }
}
