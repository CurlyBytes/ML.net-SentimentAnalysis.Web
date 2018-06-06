namespace SentimentAnalysis.Web.Interfaces
{
    using SentimentAnalysis.Web.Models;
    using System.Threading.Tasks;
    public interface IClassifyService
    {
        Task<string> ClassifySentiment(string usersUtterance);
    }
}
