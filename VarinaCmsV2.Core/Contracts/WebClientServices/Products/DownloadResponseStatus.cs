namespace VarinaCmsV2.Core.Contracts.WebClientServices.Products
{
    public enum DownloadResponseStatus
    {
        FileNotExists=5,
        DownLoadAllowed=10,
        DownLoadNotAllowedDueToDownLoadCountRestriction=15,
        DownLoadNotAllowedDueToUserNotPurchasedTheItemInSpecifiedOrder=20,
        DownLoadNotAllowedDueToExpirationDate=25,
        DownLoadNotAllowedDueToOrderNotExists=30,
    }
}