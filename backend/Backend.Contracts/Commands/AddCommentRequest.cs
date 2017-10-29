using System;

namespace Backend.Contracts.Commands
{
    public class AddCommentRequest
    {
        public Guid ImageId { get; set; }
        public string Comment { get; set; }
    }
}