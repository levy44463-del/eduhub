using System;
using EduHub.Console.Domain;

Console.WriteLine("=== EDUHUB CONSOLE DEMO ===");

// 1. Tạo CLB
var club = Club.Create("IT Club", "Câu lạc bộ Lập trình");
Console.WriteLine($"1. Đã tạo CLB: {club.Name}");

// 2. Thêm thành viên
var member = Member.Create("Nguyễn Văn A", "nva@eduhub.com", "SV001");
club.AddMember(member);
Console.WriteLine($"2. Đã thêm thành viên {member.Name}. Tổng số TV CLB: {club.MemberCount()}");

// 3. Tạo sự kiện
var techEvent = Event.Create(club.Id, "Workshop Clean Architecture", DateTime.Now.AddDays(7), 100, false);
Console.WriteLine($"3. Đã tạo sự kiện: {techEvent.Title} (Sức chứa: {techEvent.Capacity})");

// 4. Đăng ký tham gia (Pending)
var registration = Registration.Create(member.Id, techEvent.Id);
Console.WriteLine($"4. Đăng ký sự kiện thành công. Trạng thái phiếu: {registration.Status}");

// 5. Xác nhận (Confirmed)
registration.Confirm();
Console.WriteLine($"5. Đã duyệt phiếu tham gia. Trạng thái phiếu: {registration.Status}");

// 6 & 7. Check-in qua QR và Báo cáo
var checkIn = CheckIn.Create(registration, "QR Code");
Console.WriteLine($"6 & 7. Điểm danh thành công: {checkIn.Summary()}");

// 8. Cố tình vi phạm luật nghiệp vụ (Demo bắt lỗi)
Console.WriteLine("\n--- THỬ VI PHẠM LUẬT NGHIỆP VỤ ---");
try
{
    // Cố tình thêm lại đúng thành viên A vào CLB (vi phạm luật chống trùng lặp)
    Console.WriteLine("Thử thêm lại sinh viên A vào CLB lần nữa...");
    club.AddMember(member);
}
catch (Exception ex)
{
    Console.WriteLine($"[HỆ THỐNG ĐÃ CHẶN]: {ex.Message}");
}

try
{
    // Tạo một phiếu đăng ký mới (đang Pending) và đòi Check-in ngay
    Console.WriteLine("\nThử tạo phiếu mới và điểm danh khi chưa được duyệt...");
    var invalidReg = Registration.Create(member.Id, techEvent.Id);
    var failedCheckIn = CheckIn.Create(invalidReg, "Manual");
}
catch (Exception ex)
{
    Console.WriteLine($"[HỆ THỐNG ĐÃ CHẶN]: {ex.Message}");
}