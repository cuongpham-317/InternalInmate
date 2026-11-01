using InternalInmate.Application.DTOs;
using InternalInmate.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InternalInmate.Application.Validators
{
    public class ValidationResult
    {
        public List<string> Errors { get; } = new List<string>();
        public bool IsValid => Errors.Count == 0;
    }

    public static class SaveProfileValidator
    {
        public static ValidationResult Validate(SaveProfileRequest request)
        {
            var result = new ValidationResult();

            if (request == null)
            {
                result.Errors.Add("Yêu cầu không được để trống.");
                return result;
            }

            if (string.IsNullOrWhiteSpace(request.Code))
            {
                result.Errors.Add("Mã hồ sơ là bắt buộc.");
            }
            else if (request.Code.Trim().Length > 50)
            {
                result.Errors.Add("Mã hồ sơ tối đa 50 ký tự.");
            }

            if (string.IsNullOrWhiteSpace(request.FullName))
            {
                result.Errors.Add("Họ tên là bắt buộc.");
            }
            else if (request.FullName.Trim().Length > 200)
            {
                result.Errors.Add("Họ tên tối đa 200 ký tự.");
            }

            if (request.CategoryId <= 0)
            {
                result.Errors.Add("Loại hồ sơ là bắt buộc.");
            }

            if (request.DateOfBirth.HasValue && request.DateOfBirth.Value.Date > DateTime.Today)
            {
                result.Errors.Add("Ngày sinh không được lớn hơn ngày hiện tại.");
            }

            if (!string.IsNullOrWhiteSpace(request.IdentityNumber) && request.IdentityNumber.Trim().Length > 50)
            {
                result.Errors.Add("Số định danh tối đa 50 ký tự.");
            }

            if (!string.IsNullOrEmpty(request.Notes) && request.Notes.Length > 2000)
            {
                result.Errors.Add("Ghi chú tối đa 2000 ký tự.");
            }

            var addresses = request.Addresses ?? new List<ProfileAddressDto>();
            var primaryCount = addresses.Count(a => a.IsPrimary);
            if (addresses.Count > 0 && primaryCount == 0)
            {
                result.Errors.Add("Phải chọn một địa chỉ chính.");
            }

            if (primaryCount > 1)
            {
                result.Errors.Add("Chỉ được có một địa chỉ chính.");
            }

            foreach (var address in addresses)
            {
                if (string.IsNullOrWhiteSpace(address.AddressLine))
                {
                    result.Errors.Add("Địa chỉ không được để trống.");
                }

                if (address.Latitude.HasValue || address.Longitude.HasValue)
                {
                    if (!address.Latitude.HasValue || !address.Longitude.HasValue)
                    {
                        result.Errors.Add("Cần đủ cả vĩ độ và kinh độ.");
                    }
                    else if (!GeoCoordinate.IsValid(address.Latitude.Value, address.Longitude.Value))
                    {
                        result.Errors.Add("Tọa độ nằm ngoài phạm vi hợp lệ (Vĩ độ: [-90, 90], Kinh độ: [-180, 180]).");
                    }
                }
            }

            return result;
        }
    }
}
