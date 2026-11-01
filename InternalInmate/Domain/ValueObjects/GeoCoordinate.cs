using System;

namespace InternalInmate.Domain.ValueObjects
{
    public sealed class GeoCoordinate : IEquatable<GeoCoordinate>
    {
        public double Latitude { get; }
        public double Longitude { get; }

        private GeoCoordinate(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public static GeoCoordinate Create(double latitude, double longitude)
        {
            if (!IsValid(latitude, longitude))
            {
                throw new ArgumentOutOfRangeException(
                    nameof(latitude),
                    "Tọa độ không hợp lệ. Vĩ độ phải trong [-90, 90], kinh độ phải trong [-180, 180].");
            }

            return new GeoCoordinate(latitude, longitude);
        }

        public static bool IsValid(double latitude, double longitude)
        {
            return latitude >= -90.0 && latitude <= 90.0 && longitude >= -180.0 && longitude <= 180.0;
        }

        public bool Equals(GeoCoordinate other)
        {
            if (other == null) return false;
            return Math.Abs(Latitude - other.Latitude) < 0.0000001
                && Math.Abs(Longitude - other.Longitude) < 0.0000001;
        }

        public override bool Equals(object obj) => Equals(obj as GeoCoordinate);

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Latitude.GetHashCode();
                hash = hash * 23 + Longitude.GetHashCode();
                return hash;
            }
        }

        public override string ToString() => $"{Latitude:F6}, {Longitude:F6}";
    }
}
