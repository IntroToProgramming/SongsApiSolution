using SongsApi.Controllers;

namespace SongsApi
{
    public interface IProvideServerStatus
    {
        GetStatusResponse GetMyStatus();
    }
}