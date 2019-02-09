using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace LedMatrixWeb.App.Services
{
    internal interface ILedMatrixService : IHostedService
    {
        int x { get; set; }
    }
}