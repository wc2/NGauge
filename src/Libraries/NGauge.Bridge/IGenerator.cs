using System.Threading.Tasks;

namespace NGauge.Bridge
{
    public interface IGenerator
    {
        Task<string> CreateOrUpdateAsync(string projectPath);
    }
}