using InternalInmate.Application.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InternalInmate.Application.Interfaces
{
    public interface IMapService
    {
        string GetMapHtml();
        Task<IReadOnlyList<GeocodeResult>> SearchAddressAsync(string query, CancellationToken cancellationToken = default);
    }
}

