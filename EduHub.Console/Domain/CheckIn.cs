using System;

namespace EduHub.Console.Domain
{
    public class CheckIn
    {
        public Guid Id { get; private set; }
        public Guid RegistrationId { get; private set; } // Khóa ngoại trỏ về Registration
        public DateTime CheckedInAt { get; private set; }
        public string Method { get; private set; } // Phương thức điểm danh 

        private CheckIn(Guid id, Guid registrationId, DateTime checkedInAt, string method)
        {
            Id = id;
            RegistrationId = registrationId;
            CheckedInAt = checkedInAt;
            Method = method;
        }

       
        public static CheckIn Create(Registration registration, string method)
        {
            ArgumentNullException.ThrowIfNull(registration);

            
            if (registration.Status != RegistrationStatus.Confirmed)
            {
                throw new InvalidOperationException("Chỉ có thể điểm danh khi phiếu đăng ký đã được xác nhận (Confirmed).");
            }

            return new CheckIn(Guid.NewGuid(), registration.Id, DateTime.Now, method);
        }

        
        public string Summary()
        {
            return $"Đã điểm danh lúc {CheckedInAt.ToString("HH:mm:ss")} qua {Method}.";
        }
    }
}