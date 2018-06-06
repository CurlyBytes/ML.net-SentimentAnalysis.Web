namespace SentimentAnalysis.Web.Interfaces
{
    using System.IO;
    using System.Threading.Tasks;
    public interface IBlobStorageService
    {
        Task<Stream> DownloadModelAsync();
    }
}
