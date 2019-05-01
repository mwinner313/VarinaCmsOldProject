namespace VarinaCmsV2.Core.Services
{
    public class SimpleResonse: IServiceResponse
    {
        public ResponseAccess Access { get; set; }
        public string Message { get; set; }
    }
}