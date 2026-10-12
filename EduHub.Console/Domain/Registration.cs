using System;

namespace EduHub.Console.Domain
{
    // Sử dụng Enum thay vì dùng chuỗi (string) để quản lý trạng thái chặt chẽ hơn
    public enum RegistrationStatus
    {
        Pending,    // Chờ xác nhận
        Confirmed,  // Đã xác nhận
        Cancelled   // Đã huỷ
    }

    public class Registration
    {
        public Guid Id { get; private set; }
        public Guid MemberId { get; private set; } // Khoá ngoại trỏ về Member
        public Guid EventId { get; private set; }  // Khoá ngoại trỏ về Event
        public DateTime RegisteredAt { get; private set; }
        public RegistrationStatus Status { get; private set; }

        private Registration(Guid id, Guid memberId, Guid eventId, DateTime registeredAt, RegistrationStatus status)
        {
            Id = id;
            MemberId = memberId;
            EventId = eventId;
            RegisteredAt = registeredAt;
            Status = status;
        }

        
        public static Registration Create(Guid memberId, Guid eventId)
        {
           
            return new Registration(Guid.NewGuid(), memberId, eventId, DateTime.Now, RegistrationStatus.Pending);
        }

        // Hành vi: Xác nhận tham gia
        public void Confirm()
        {
            
            if (Status == RegistrationStatus.Cancelled)
                throw new InvalidOperationException("Không thể xác nhận phiếu đang ở trạng thái Cancelled.");
                
            Status = RegistrationStatus.Confirmed;
        }

        // Hành vi: Huỷ đăng ký
        public void Cancel()
        {
            Status = RegistrationStatus.Cancelled;
        }
    }
}