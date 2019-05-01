using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.Core.Services.Entity
{
    public interface  IEntityService
    {
        Task<EntityResponse >Get(EntityGetRequest request);
        Task<EntityListResponse> Get(EntityListRequest request);
        Task<EntityResponse> Add(EntityAddRequest request);
        Task<EntityResponse> Update(EntityUpdateRequest request);
        Task<EntityResponse> Delete(EntityDeleteRequest request);
    }
}
