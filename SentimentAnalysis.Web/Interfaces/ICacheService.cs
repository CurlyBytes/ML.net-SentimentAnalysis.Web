using System.Threading.Tasks;

namespace SentimentAnalysis.Web.Interfaces
{
    public interface ICacheService
    {
        object GetItem(object key);
        void SetItem(object key, object value);
    }
}
