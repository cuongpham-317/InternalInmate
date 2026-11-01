using InternalInmate.Domain.ValueObjects;
using System;
using System.Linq;

namespace InternalInmate.Domain.Entities
{
    public class ProfileAddress
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string AddressLine { get; set; } = string.Empty;
        public string Ward { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string Country { get; set; } = "Việt Nam";
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public bool IsPrimary { get; set; }
        public string Note { get; set; }

        public Profile Profile { get; set; }

        public void SetCoordinate(GeoCoordinate coordinate)
        {
            if (coordinate != null)
            {
                Latitude = coordinate.Latitude;
                Longitude = coordinate.Longitude;
            }
        }

        public bool HasCoordinate => Latitude.HasValue && Longitude.HasValue;

        public string DisplayText
        {
            get
            {
                var parts = new[] { AddressLine, Ward, District, Province, Country }
                    .Where(p => !string.IsNullOrWhiteSpace(p));
                return string.Join(", ", parts);
            }
        }
    }
}
