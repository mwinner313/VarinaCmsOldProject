namespace VarinaCmsV2.Core.Services.Form
{
    public class FormSubmitResponse:IServiceResponse
    {
        public ResponseAccess Access { get; set; }
        public string Message { get; set; }
    }
}