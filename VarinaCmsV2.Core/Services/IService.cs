using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.Core.Services
{
    public interface IService<in TGetRequest, in TGetListRequest, in TAddRequest, in TEditRequest, in TDeleteRequest,
        TResponse, TListResponse> 
        where TGetRequest :IServiceRequest
        where TGetListRequest : IServiceRequest
        where TAddRequest : IServiceRequest
        where TEditRequest : IServiceRequest
        where TDeleteRequest : IServiceRequest
        where TResponse : IServiceResponse
        where TListResponse : IServiceResponse
    {
        Task<TListResponse> Get(TGetListRequest request);
        Task<TResponse> Get(TGetRequest request);
        Task<TResponse> Add(TAddRequest request);
        Task<TResponse> Edit(TEditRequest request);
        Task<TResponse> Delete(TDeleteRequest request);
    }
}