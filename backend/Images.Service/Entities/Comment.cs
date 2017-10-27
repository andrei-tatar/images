using System;
using Common;

namespace Images.Service.Entities
{
    public class Comment : IEntity
    {
        public Guid Id { get; set; }
        public Guid ImageGuid { get; set; }
        public DateTime Date { get; set; }
        public string CommentText { get; set; }
        public string UserId { get; set; }
    }
}
