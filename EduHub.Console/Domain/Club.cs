using System;
using System.Collections.Generic;
using System.Linq;

namespace EduHub.Console.Domain
{
    public class Club
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }

        
        private readonly List<Member> _members = new List<Member>();

        
        public IReadOnlyList<Member> Members => _members;

        private Club(Guid id, string name, string description, DateTime createdAt)
        {
            Id = id;
            Name = name;
            Description = description;
            CreatedAt = createdAt;
        }

        public static Club Create(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tên câu lạc bộ không được để trống");

            return new Club(Guid.NewGuid(), name, description, DateTime.Now);
        }

        
        public void AddMember(Member member)
        {
            ArgumentNullException.ThrowIfNull(member); // Không được null

            
            if (_members.Any(m => m.Id == member.Id))
                throw new InvalidOperationException($"{member.Name} đã là thành viên của câu lạc bộ này.");

            _members.Add(member);
        }

        public int MemberCount() => _members.Count;
    }
}