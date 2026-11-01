# Quy trình xây dựng phần mềm WinForms C# quản lý hồ sơ

## 1. Mục tiêu dự án

Xây dựng ứng dụng desktop trên Windows bằng C# WinForms, sử dụng SQL Server Local để lưu dữ liệu, quản lý hồ sơ, tài liệu đính kèm và vị trí địa lý trên bản đồ.

### Phạm vi chức năng

- Quản lý hồ sơ: thêm, sửa, xem chi tiết, tìm kiếm, lọc và phân loại.
- Quản lý địa chỉ: lưu địa chỉ và tọa độ địa lý.
- Quản lý tài liệu: đính kèm, mở, tải bản sao và xóa file.
- Bản đồ: hiển thị vị trí, chọn marker và lưu tọa độ.
- Dữ liệu cục bộ: kết nối SQL Server Local, backup và restore.
- Triển khai: đóng gói cài đặt và cập nhật phần mềm.

---

## 2. Quy trình tổng thể

```text
Phân tích yêu cầu
      |
      v
Thiết kế Domain
      |
      v
Tạo Solution và phân lớp
      |
      v
Thiết kế SQL Server Local
      |
      v
Kết nối database và Repository
      |
      v
Xây dựng Application Services
      |
      v
Xây dựng giao diện quản lý hồ sơ
      |
      v
Quản lý tài liệu đính kèm
      |
      v
Tích hợp bản đồ và geocoding
      |
      v
Kiểm thử, backup và bảo mật
      |
      v
Đóng gói và triển khai
```

## 3. Giai đoạn 1 — Phân tích nghiệp vụ

### Bước 1. Xác định yêu cầu

Làm rõ các nội dung sau:

1. Phần mềm quản lý loại hồ sơ nào.
2. Các trường thông tin bắt buộc của hồ sơ.
3. Các danh mục và trạng thái hồ sơ.
4. Một hồ sơ có thể có bao nhiêu địa chỉ.
5. Những định dạng tài liệu được phép đính kèm.
6. Cách tìm kiếm, lọc và phân loại hồ sơ.
7. Phần mềm chỉ dùng trên một máy hay nhiều máy cần dùng chung dữ liệu.
8. Bản đồ cần trực tuyến hay phải sử dụng offline.
9. Có cần tài khoản, phân quyền và lịch sử thao tác hay không.

### Đầu ra

- Danh sách chức năng.
- Danh sách trường dữ liệu.
- Quy tắc nghiệp vụ.
- Phạm vi phiên bản đầu tiên (MVP).

---

## 4. Giai đoạn 2 — Thiết kế Domain

### Bước 2. Tạo các Entity

Các Entity dự kiến:

| Entity | Trách nhiệm |
|---|---|
| `Profile` | Thông tin hồ sơ chính |
| `ProfileAddress` | Địa chỉ, vĩ độ và kinh độ |
| `ProfileDocument` | Metadata tài liệu đính kèm |
| `ProfileCategory` | Danh mục loại hồ sơ |
| `AppUser` | Người dùng, nếu có đăng nhập |
| `AuditLog` | Lịch sử thao tác, nếu cần |

Quan hệ dữ liệu cơ bản:

```text
Profile (1) -------- (N) ProfileAddress
Profile (1) -------- (N) ProfileDocument
ProfileCategory (1) - (N) Profile
```

### Bước 3. Xây dựng quy tắc Domain

- Định nghĩa các trường bắt buộc.
- Định nghĩa trạng thái hồ sơ bằng `enum`.
- Quy định mã hồ sơ có duy nhất hay không.
- Xác định quy tắc xóa mềm.
- Xác định quy tắc địa chỉ chính.
- Kiểm tra tính hợp lệ của tọa độ.
- Tách quy tắc nghiệp vụ khỏi giao diện WinForms.

### Đầu ra

- Các Entity, Enum và Value Object cần thiết.
- Quan hệ giữa các Entity.
- Quy tắc nghiệp vụ được thống nhất.

---

## 5. Giai đoạn 3 — Tạo Solution và project

### Bước 4. Tạo cấu trúc Solution

```text
ProfileManagement.sln
|
+-- ProfileManagement.WinForms
|   +-- Forms
|   +-- Controls
|   +-- Program.cs
|
+-- ProfileManagement.Domain
|   +-- Entities
|   +-- Enums
|   +-- Interfaces
|
+-- ProfileManagement.Application
|   +-- DTOs
|   +-- Services
|   +-- Validators
|   +-- Interfaces
|
+-- ProfileManagement.Infrastructure
|   +-- Data
|   +-- Configurations
|   +-- Repositories
|   +-- FileStorage
|   +-- Maps
|
+-- ProfileManagement.Tests
    +-- DomainTests
    +-- ServiceTests
    +-- RepositoryTests
```

### Trách nhiệm các project

- `Domain`: entity và quy tắc nghiệp vụ.
- `Application`: điều phối nghiệp vụ và kiểm tra dữ liệu.
- `Infrastructure`: SQL Server, lưu file và tích hợp bản đồ.
- `WinForms`: giao diện và tương tác với người dùng.
- `Tests`: kiểm thử tự động.

### Nguyên tắc phụ thuộc

- WinForms gọi Application.
- Application sử dụng Domain.
- Infrastructure triển khai các interface cần thiết của các lớp bên trong.
- Không viết truy vấn SQL trực tiếp trong sự kiện của Form.
- Không để Domain phụ thuộc WinForms hoặc SQL Server.

### Đầu ra

Solution có cấu trúc rõ ràng và build thành công.

---

## 6. Giai đoạn 4 — Thiết kế và tạo database

### Bước 5. Chọn SQL Server Local

Hai lựa chọn phổ biến:

| Tiêu chí | SQL Server Express LocalDB | SQL Server Express |
|---|---|---|
| Mục đích phù hợp | Ứng dụng cục bộ đơn giản | Ứng dụng cần dịch vụ database thường trực |
| Cách chạy | Khởi động theo nhu cầu và Windows user | Chạy như dịch vụ |
| Dùng chung nhiều máy | Không phù hợp làm database dùng chung | Có thể cấu hình để nhiều máy truy cập |
| Triển khai | Cần cài LocalDB và khởi tạo instance phù hợp | Cần cài và cấu hình SQL Server Express |

Nếu ứng dụng chỉ dùng trên một máy, LocalDB có thể phù hợp. Nếu nhiều máy cần truy cập cùng dữ liệu, nên cân nhắc SQL Server Express trên máy chủ hoặc một database server phù hợp.

### Bước 6. Thiết kế schema

Các bảng dự kiến:

- `Profile`
- `ProfileAddress`
- `ProfileDocument`
- `ProfileCategory`
- `AppUser` (nếu cần)
- `AuditLog` (nếu cần)

Cần xác định:

1. Khóa chính và khóa ngoại.
2. Kiểu dữ liệu và độ dài từng trường.
3. Index cho các trường tìm kiếm.
4. Unique constraint cho mã hồ sơ.
5. Quy tắc xóa và cập nhật dữ liệu liên quan.
6. Dữ liệu danh mục mặc định.
7. Chiến lược migration và nâng cấp schema.

### Đầu ra

- Script tạo database.
- Schema các bảng.
- Dữ liệu danh mục khởi tạo.
- Quy tắc backup/restore ban đầu.

---

## 7. Giai đoạn 5 — Kết nối dữ liệu

### Bước 7. Cấu hình Data Access

Có thể dùng Entity Framework Core hoặc ADO.NET. Nếu dùng Entity Framework Core:

1. Cài package `Microsoft.EntityFrameworkCore.SqlServer`.
2. Tạo `AppDbContext`.
3. Cấu hình Entity bằng Fluent API.
4. Cấu hình connection string.
5. Tạo migration và database.
6. Đăng ký `DbContext` trong Dependency Injection.
7. Kiểm thử kết nối và các thao tác CRUD.

Ví dụ connection string cho LocalDB:

```csharp
string connectionString =
    @"Server=(localdb)\MSSQLLocalDB;
      Database=ProfileManagementDb;
      Integrated Security=true;
      Encrypt=true;
      TrustServerCertificate=true;";
```

Ví dụ này cần được điều chỉnh theo môi trường cài đặt thực tế. Không nên hard-code thông tin bí mật trong source code.

### Bước 8. Tạo Repository và Service

Repository dự kiến:

- `ProfileRepository`
- `ProfileAddressRepository`
- `ProfileDocumentRepository`

Service dự kiến:

- `ProfileService`
- `ProfileAddressService`
- `DocumentService`
- `MapService` (nếu cần tách riêng tích hợp bản đồ)

Service thực hiện kiểm tra nghiệp vụ và điều phối thao tác dữ liệu. Repository tập trung truy vấn và cập nhật database.

### Đầu ra

Có thể thêm, sửa, xem, xóa và tìm kiếm dữ liệu qua lớp nghiệp vụ mà không cần gọi SQL trực tiếp từ Form.

---

## 8. Giai đoạn 6 — Xây dựng chức năng quản lý hồ sơ

### Bước 9. Xây dựng giao diện

Các Form dự kiến:

| Form | Chức năng |
|---|---|
| `FrmMain` | Màn hình chính và điều hướng |
| `FrmProfileList` | Danh sách, tìm kiếm và lọc |
| `FrmProfileDetail` | Thêm mới, chỉnh sửa và xem chi tiết |
| `FrmDocumentManager` | Quản lý tài liệu đính kèm |
| `FrmMapPicker` | Chọn vị trí và hiển thị bản đồ |

### Bước 10. Hoàn thiện CRUD và tìm kiếm

- Tạo hồ sơ mới.
- Chỉnh sửa hồ sơ.
- Xem chi tiết hồ sơ.
- Xóa mềm hoặc ngừng sử dụng hồ sơ.
- Tìm kiếm theo mã, tên, địa chỉ hoặc loại hồ sơ.
- Lọc theo trạng thái và thời gian.
- Kiểm tra dữ liệu bắt buộc.
- Hiển thị lỗi rõ ràng và không làm mất dữ liệu người dùng đã nhập khi có lỗi.

### Đầu ra

Người dùng có thể quản lý hồ sơ qua giao diện WinForms.

---

## 9. Giai đoạn 7 — Quản lý tài liệu đính kèm

### Bước 11. Xây dựng File Storage Service

Quy trình đính kèm:

1. Người dùng chọn file bằng `OpenFileDialog`.
2. Kiểm tra phần mở rộng, dung lượng và quyền truy cập.
3. Tạo tên lưu nội bộ duy nhất.
4. Sao chép file vào thư mục dữ liệu.
5. Ghi metadata vào bảng `ProfileDocument`.
6. Cập nhật danh sách tài liệu trên giao diện.

Các chức năng bổ sung:

- Mở file bằng ứng dụng mặc định.
- Xuất bản sao file ra thư mục do người dùng chọn.
- Xóa tài liệu theo quy tắc nghiệp vụ.
- Kiểm tra file còn tồn tại trước khi mở.
- Ghi log lỗi khi thao tác file thất bại.

### Nguyên tắc lưu trữ

Nên lưu file trong thư mục dữ liệu riêng và lưu đường dẫn tương đối cùng metadata trong SQL Server. Tránh lưu file người dùng trong thư mục cài đặt ứng dụng vì thư mục này có thể bị thay thế hoặc xóa khi nâng cấp.

Cần có cơ chế xử lý tình huống file đã được sao chép nhưng database ghi thất bại, hoặc database đã có metadata nhưng file không còn tồn tại.

### Đầu ra

Một hồ sơ có thể có nhiều tài liệu đính kèm, xem lại được và có thể backup.

---

## 10. Giai đoạn 8 — Tích hợp bản đồ

### Bước 12. Chọn thư viện bản đồ

**Phương án A — GMap.NET**

- Tích hợp theo hướng desktop .NET.
- Hỗ trợ hiển thị bản đồ và marker.
- Cần xác minh target framework, phiên bản thư viện và nhà cung cấp tile tương thích.

**Phương án B — WebView2 + Leaflet**

- Nhúng bản đồ web vào WinForms.
- Hỗ trợ marker, zoom, pan và sự kiện nhấp chọn.
- Linh hoạt khi tùy chỉnh giao diện.

### Bước 13. Xây dựng luồng chọn địa điểm

1. Hiển thị bản đồ trong `FrmMapPicker`.
2. Cho phép di chuyển bản đồ và chọn vị trí.
3. Lấy vĩ độ, kinh độ từ điểm được chọn.
4. Hiển thị địa chỉ và tọa độ trên giao diện.
5. Lưu tọa độ vào `ProfileAddress`.
6. Khi mở lại hồ sơ, đọc tọa độ và hiển thị marker.

### Bước 14. Tích hợp geocoding nếu cần

Geocoding là chức năng chuyển địa chỉ dạng chữ thành tọa độ. Đây là dịch vụ riêng, không tự động có chỉ vì sử dụng dữ liệu OpenStreetMap.

Cần kiểm tra giới hạn truy cập, điều khoản dịch vụ, chính sách lưu cache và yêu cầu ghi công dữ liệu bản đồ.

OpenStreetMap cung cấp dữ liệu mở, nhưng máy chủ tile công cộng không phải dịch vụ miễn phí có cam kết vận hành. Không nên tải hàng loạt tile từ máy chủ công cộng để tạo bản đồ offline. Nếu cần offline, phải chuẩn bị nguồn dữ liệu và phương án triển khai riêng.

### Đầu ra

Người dùng có thể chọn, lưu và xem lại vị trí của hồ sơ trên bản đồ.

---

## 11. Giai đoạn 9 — Kiểm thử

### Bước 15. Kiểm thử nghiệp vụ

- Thêm, sửa, tìm kiếm và lọc hồ sơ.
- Mã hồ sơ trùng hoặc dữ liệu bắt buộc bị thiếu.
- Hồ sơ có nhiều địa chỉ và tài liệu.
- Tọa độ nằm ngoài phạm vi hợp lệ.
- Xóa hồ sơ có tài liệu liên quan.
- Quyền truy cập không đủ.

### Bước 16. Kiểm thử lỗi và khả năng phục hồi

- SQL Server không khởi động hoặc không kết nối được.
- Database chưa được khởi tạo.
- File đính kèm không tồn tại hoặc không có quyền truy cập.
- Sao chép file thành công nhưng ghi database thất bại.
- Dịch vụ bản đồ hoặc geocoding không truy cập được.
- Ứng dụng bị đóng bất ngờ trong khi đang lưu dữ liệu.

### Đầu ra

Bộ kiểm thử và danh sách lỗi được xử lý trước khi phát hành.

---

## 12. Giai đoạn 10 — Backup, bảo mật và triển khai

### Bước 17. Backup và restore

- Xây dựng chức năng backup database.
- Sao lưu thư mục tài liệu.
- Kiểm thử phục hồi database và file.
- Quy định vị trí lưu backup và thời gian giữ bản sao.
- Nếu cần, hỗ trợ xuất backup ra ổ đĩa khác.

Database và thư mục tài liệu cần được sao lưu như một bộ dữ liệu thống nhất. Chỉ backup database là chưa đủ khi file được lưu bên ngoài SQL Server.

### Bước 18. Bảo mật và logging

- Phân quyền thao tác nếu có nhiều người dùng.
- Bảo vệ thư mục dữ liệu và file đính kèm.
- Không lưu thông tin bí mật trực tiếp trong source code.
- Ghi log lỗi database, file và bản đồ.
- Ghi lịch sử thao tác đối với nghiệp vụ cần truy vết.

### Bước 19. Đóng gói ứng dụng

- Tạo bộ cài đặt.
- Kiểm tra yêu cầu .NET Runtime.
- Cài đặt hoặc kiểm tra SQL Server trên máy đích.
- Khởi tạo database lần đầu.
- Cấu hình thư mục dữ liệu và quyền truy cập.
- Kiểm tra nâng cấp ứng dụng mà không làm mất database và tài liệu.
- Viết hướng dẫn cài đặt, backup và phục hồi.

### Đầu ra

Phần mềm có thể được cài đặt, chạy và bảo trì trên máy Windows đích.

---

## 13. Ước tính thời gian

Ước tính cho một lập trình viên đã có kinh nghiệm C#, với phạm vi cơ bản và chưa có yêu cầu nghiệp vụ đặc biệt phức tạp.

| Công việc | Thời gian dự kiến |
|---|---:|
| Phân tích yêu cầu và Domain | 1–2 ngày |
| Tạo Solution và database | 1–2 ngày |
| Kết nối SQL, Repository và Service | 2–3 ngày |
| Giao diện quản lý hồ sơ | 2–4 ngày |
| Quản lý file đính kèm | 1–3 ngày |
| Tích hợp bản đồ và geocoding | 2–4 ngày |
| Kiểm thử, backup và đóng gói | 2–3 ngày |
| **Tổng dự kiến** | **11–21 ngày công** |

Đây là ước tính ban đầu. Thời gian thực tế phụ thuộc vào loại hồ sơ, số trường, số lượng file, yêu cầu phân quyền, quy mô dữ liệu và việc có cần bản đồ offline hay không.

---

## 14. Thứ tự ưu tiên để bắt đầu lập trình

Nên hoàn thành một luồng chức năng nhỏ từ đầu đến cuối trước, thay vì phát triển tất cả module cùng lúc.

1. Chốt yêu cầu nghiệp vụ và trường dữ liệu.
2. Xây dựng Domain.
3. Tạo Solution và cấu hình project reference.
4. Thiết kế database và kết nối SQL Server Local.
5. Xây dựng chức năng thêm, sửa, xem và tìm kiếm hồ sơ.
6. Thêm quản lý tài liệu đính kèm.
7. Thêm địa chỉ và bản đồ.
8. Kiểm thử, backup và đóng gói.

## 15. Tiêu chí hoàn thành phiên bản đầu tiên (MVP)

- [ ] Ứng dụng chạy được trên Windows.
- [ ] Database được khởi tạo và kết nối thành công.
- [ ] Có thể thêm, sửa, xem và tìm kiếm hồ sơ.
- [ ] Có thể đính kèm, mở và tải tài liệu.
- [ ] Có thể lưu địa chỉ và tọa độ.
- [ ] Bản đồ hiển thị marker của hồ sơ đã lưu.
- [ ] Có thông báo lỗi rõ ràng khi database, file hoặc dịch vụ bản đồ gặp sự cố.
- [ ] Có quy trình backup và restore đã được kiểm thử.
- [ ] Có bộ cài đặt và hướng dẫn triển khai.

**Khuyến nghị:** bắt đầu từ yêu cầu nghiệp vụ và Domain, sau đó tạo database và hoàn thiện một luồng CRUD cơ bản. Chỉ khi luồng này ổn định mới mở rộng sang quản lý file và bản đồ.
