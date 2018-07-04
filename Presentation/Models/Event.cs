using System;
using System.Collections.Generic;

namespace Presentation.Models
{
    public class Event : IDocument
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime StartIn { get; set; }
        public DateTime EndIn { get; set; }

        public bool IsPublished { get; set; }
        public bool IsActive { get; set; }
        public int QuestionsLimit { get; set; }
        public List<Culture> Cultures { get; set; }
    }
}