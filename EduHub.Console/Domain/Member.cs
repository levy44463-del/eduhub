using System;

namespace EduHub.Console.Domain
{
    public class Member
    {
        
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string StudentCode { get; private set; } 
        public DateTime JoinedAt { get; private set; }
        // public MemberStatus Status { get; private set; } // Tạm thời ẩn Status để đơn giản hóa ở bước đầu

        // Constructor private
        private Member(Guid id, string name, string email, string studentCode, DateTime joinedAt)
        {
            Id = id;
            Name = name;
            Email = email;
            StudentCode = studentCode;
            JoinedAt = joinedAt;
        }

        
        public static Member Create(string name, string email, string studentCode)
        {
           
            return new Member(Guid.NewGuid(), name, email, studentCode, DateTime.Now);
        }
    }
}