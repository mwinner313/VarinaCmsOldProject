using System.Threading.Tasks;

namespace VarinaCmsV2.Core.Services.Form
{
    public interface IFormService
    {
        Task<FormSubmitResponse> SubmitNewForm(FormSubmitRequest request);
        
        Task<FormListReponse> Get(FormListRequest request);
        Task<SimpleResonse> ChangeIsSeenState(FormChangeIsSeenStateRequest request);
    }
}
