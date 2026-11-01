using InternalInmate.Application.DTOs;
using InternalInmate.Application.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace InternalInmate.Infrastructure.Maps
{
    public class NominatimMapService : IMapService
    {
        private readonly HttpClient _http;
        private static readonly Dictionary<string, IReadOnlyList<GeocodeResult>> Cache = new Dictionary<string, IReadOnlyList<GeocodeResult>>(StringComparer.OrdinalIgnoreCase);

        public NominatimMapService(HttpClient http = null)
        {
            _http = http ?? new HttpClient();
            if (!_http.DefaultRequestHeaders.UserAgent.Any())
            {
                _http.DefaultRequestHeaders.UserAgent.ParseAdd("InternalInmate/1.0 (local profile management)");
            }
        }

        public string GetMapHtml()
        {
            var assembly = typeof(NominatimMapService).Assembly;
            var name = assembly.GetManifestResourceNames()
                .FirstOrDefault(n => n.EndsWith("map.html", StringComparison.OrdinalIgnoreCase));
            if (name != null)
            {
                using (var stream = assembly.GetManifestResourceStream(name))
                {
                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }

            // Fallback: check file directly
            var localPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Infrastructure", "Maps", "map.html");
            if (File.Exists(localPath))
            {
                return File.ReadAllText(localPath);
            }

            return @"<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8' />
    <meta http-equiv='X-UA-Compatible' content='IE=edge' />
    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
    <link rel='stylesheet' href='https://unpkg.com/leaflet@1.9.4/dist/leaflet.css' />
    <style>html, body, #map { height: 100%; margin: 0; padding: 0; }</style>
</head>
<body>
    <div id='map'></div>
    <script src='https://unpkg.com/leaflet@1.9.4/dist/leaflet.js'></script>
    <script>
        var map = L.map('map').setView([16.0471, 108.2062], 6);
        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { maxZoom: 19 }).addTo(map);
        var marker = null;
        function placeMarker(lat, lng) {
            if (marker) { marker.setLatLng([lat, lng]); }
            else { 
                marker = L.marker([lat, lng], { draggable: true }).addTo(map); 
                marker.on('dragend', function() {
                    var p = marker.getLatLng();
                    notify(p.lat, p.lng);
                });
            }
            map.setView([lat, lng], Math.max(map.getZoom(), 14));
            notify(lat, lng);
        }
        function notify(lat, lng) {
            if (window.external && window.external.OnLocationPicked) {
                window.external.OnLocationPicked(lat, lng);
            }
        }
        map.on('click', function(e) {
            placeMarker(e.latlng.lat, e.latlng.lng);
        });
        window.setMapLocation = function(lat, lng) {
            placeMarker(lat, lng);
        };
    </script>
</body>
</html>";
        }

        public async Task<IReadOnlyList<GeocodeResult>> SearchAddressAsync(string query, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new List<GeocodeResult>();
            }

            var key = query.Trim();
            lock (Cache)
            {
                if (Cache.TryGetValue(key, out var cached))
                {
                    return cached;
                }
            }

            try
            {
                var url = "https://nominatim.openstreetmap.org/search?format=jsonv2&limit=8&q=" + Uri.EscapeDataString(key);
                var response = await _http.GetAsync(url, cancellationToken);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();

                var serializer = new JavaScriptSerializer();
                var parsed = serializer.Deserialize<ArrayList>(json);
                var results = new List<GeocodeResult>();

                if (parsed != null)
                {
                    foreach (Dictionary<string, object> item in parsed)
                    {
                        var displayName = item.ContainsKey("display_name") ? item["display_name"]?.ToString() : string.Empty;
                        var latStr = item.ContainsKey("lat") ? item["lat"]?.ToString() : string.Empty;
                        var lonStr = item.ContainsKey("lon") ? item["lon"]?.ToString() : string.Empty;

                        if (double.TryParse(latStr, NumberStyles.Float, CultureInfo.InvariantCulture, out double lat)
                            && double.TryParse(lonStr, NumberStyles.Float, CultureInfo.InvariantCulture, out double lon))
                        {
                            results.Add(new GeocodeResult
                            {
                                DisplayName = displayName,
                                Latitude = lat,
                                Longitude = lon
                            });
                        }
                    }
                }

                lock (Cache)
                {
                    Cache[key] = results;
                }

                return results;
            }
            catch (Exception ex)
            {
                throw new HttpRequestException("Không truy cập được dịch vụ geocoding: " + ex.Message, ex);
            }
        }
    }
}
