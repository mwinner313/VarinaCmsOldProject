using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.Core.Services.EntityScheme
{
    public interface IEntitySchemeService
    {
        EntitySchemeListResponse GetAsync(EntitySchemeListRequest request);
        Task<EntitySchemeResponse> GetByIdAsync(EntitySchemeGetRequest request);
        Task<EntitySchemeResponse> GetByHandleAsync(EntitySchemeGetRequest request);
        Task<EntitySchemeResponse> AddAsync(EntitySchemeAddRequest request);
        Task<EntitySchemeResponse> UpdateAsync(EntitySchemeUpdateRequest request);
        Task<EntitySchemeResponse> DeleteAsync(EntitySchemeDeleteRequest request);
    }
}
