# Phân tích vi phạm SOLID — ClubManager

Nguồn: `Ch02.Refactor/ClubManager.cs` (mã nguồn cố tình vi phạm SOLID).

## Tổng quan

`ClubManager` là God class: quản lý CLB, thành viên, sự kiện, gửi thông báo và ghi log trong một class. Vi phạm rõ rệt SRP, OCP, DIP (và ISP sau khi tách interface).

---

## Chi tiết các vi phạm

### 1. SRP — Single Responsibility Principle

| # | Vị trí (dòng) | Mô tả vi phạm | Hậu quả |
|---|---------------|---------------|---------|
| 1.1 | `CreateClub` (35-64) | Một method đồng thời làm 3 việc: validate tên CLB, lưu CLB vào danh sách, ghi log, gửi thông báo cho tổng hội. | Thay đổi luật validate hoặc kênh thông báo đều phải sửa cùng method → rủi ro hồi quy cao. |
| 1.2 | `AddMemberToClub` (67-84) | Làm cả nghiệp vụ (thêm thành viên, kiểm tra đầy) lẫn gửi thông báo (email, Slack). | Logic nghiệp vụ và thông báo dính liền nhau, không thể tái sử dụng độc lập. |
| 1.3 | `CreateEvent` (88-99) | Tạo sự kiện và đồng thời gửi email cho mọi thành viên của CLB. | Lớp biết quá nhiều về luồng gửi thông báo, khó kiểm thử tạo sự kiện độc lập. |
| 1.4 | `PrintClubReport` (103-113) | In báo cáo ra console đồng thời ghi log ra file. | Báo cáo và logging dính liền, không thể xuất báo cáo mà không ghi log. |
| 1.5 | Lớp `ClubManager` (28-117) | Một class phụ trách: quản lý CLB, quản lý thành viên, quản lý sự kiện, gửi thông báo, ghi log, in báo cáo. | God class — khó đọc, khó bảo trì, vi phạm trọn vẹn SRP. |

### 2. OCP — Open/Closed Principle

| # | Vị trí (dòng) | Mô tả vi phạm | Hậu quả |
|---|---------------|---------------|---------|
| 2.1 | `CreateClub` (52-63) | Chuỗi `if/else if/else` hardcode kênh thông báo theo domain email (@gmail.com, @yahoo.com, generic). | Muốn thêm SMS, push notification phải sửa method này → dễ làm hỏng code cũ. |
| 2.2 | `AddMemberToClub` (82-83) | `Console.WriteLine` hardcode welcome email và Slack notification. | Thêm kênh mới hay thay đổi nội dung thông báo phải can thiệp vào method nghiệp vụ. |
| 2.3 | `CreateEvent` (96-97) | Vòng lặp `foreach` gửi email trực tiếp trong method tạo sự kiện. | Thay đổi cách thông báo sự kiện phải sửa `CreateEvent`. |
| 2.4 | `PrintClubReport` (105-108) | Chỉ hỗ trợ in ra console. Muốn xuất CSV phải sửa method này. | Khả năng mở rộng báo cáo bị chặn, vi phạm OCP. |

### 3. DIP — Dependency Inversion Principle

| # | Vị trí (dòng) | Mô tả vi phạm | Hậu quả |
|---|---------------|---------------|---------|
| 3.1 | `CreateClub` (48) | `File.AppendAllText(_logFilePath, ...)` — ghi file trực tiếp, không qua abstraction. | Không thể thay thế logging (ví dụ: log đến DB, Serilog) mà không sửa `ClubManager`. |
| 3.2 | `AddMemberToClub` (79) | `File.AppendAllText` trực tiếp. | Cùng trên — coupling với hệ thống file. |
| 3.3 | `CreateEvent` (94) | `File.AppendAllText` trực tiếp. | C同上 — coupling với hệ thống file. |
| 3.4 | `PrintClubReport` (111) | `File.AppendAllText` trong lúc in báo cáo. | Logging dính vào báo cáo, không tách được. |
| 3.5 | Toàn bộ class | Sử dụng `Console.WriteLine` trực tiếp cho thông báo. | Không thể inject mock notification service khi viết unit test. |

### 4. ISP — Interface Segregation Principle

| # | Vị trí (dòng) | Mô tả vi phạm | Hậu quả |
|---|---------------|---------------|---------|
| 4.1 | (chưa có interface) | `ClubManager` expose tất cả method công khai. Client chỉ cần một phần nhưng bị buộc phải phụ thuộc vào toàn bộ API. | Không tách được các intéface nhỏ theo từng mục đích (repository, notification, report). |

### 5. LCP — Liskov Substitution Principle

| # | Vị trí | Mô tả | Hậu quả |
|---|--------|-------|---------|
| 5.1 | `GetAllClubs` (116) | Trả về `List<Club>` trực tiếp (tham chiếu nội bộ). Caller có thể thay đổi danh sách club bên ngoài, làm hỏng trạng thái nội bộ của `ClubManager`. | Không kiểm soát được sự thay đổi trạng thái, dễ gây lỗi không dễ theo dõi. |

---

## Tổng kết

`ClubManager` vi phạm 4/5 nguyên tắc SOLID (SRP, OCP, DIP, ISP, LCP). Nguyên nhân chủ yếu:
- **God class**: quá nhiều trách nhiệm trong một class.
- **Hardcode dependencies**: `File.AppendAllText`, `Console.WriteLine`, `if/else` kênh thông báo.
- **Không có abstraction**: thiếu interface/repository/notification service.

Đây là cơ sở để tách thành `ClubService`, `IClubRepository`, `EventService`, `ReportService`, `IAppLogger`, `INotificationService` và áp dụng Factory, Decorator, Strategy, Observer.
