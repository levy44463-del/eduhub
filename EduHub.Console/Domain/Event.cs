using System;

namespace EduHub.Console.Domain
{
    public class Event
    {
        public Guid Id { get; private set; }
        public Guid ClubId { get; private set; } 
        public string Title { get; private set; }
        public DateTime Date { get; private set; }
        public int Capacity { get; private set; } 
        public bool IsOnline { get; private set; }

        private Event(Guid id, Guid clubId, string title, DateTime date, int capacity, bool isOnline)
        {
            Id = id;
            ClubId = clubId;
            Title = title;
            Date = date;
            Capacity = capacity;
            IsOnline = isOnline;
        }

        public static Event Create(Guid clubId, string title, DateTime date, int capacity, bool isOnline)
        {
            if (capacity <= 0)
                throw new ArgumentException("Sức chứa của sự kiện phải lớn hơn 0");

            return new Event(Guid.NewGuid(), clubId, title, date, capacity, isOnline);
        }

        
        public bool HasCapacity(int currentRegistrationsCount)
        {
            return currentRegistrationsCount < Capacity;
        }
    }
}