using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentation.Models
{
    public class Quiz {
        public Quiz()
        {
            Answers = new List<Answer>();
        }
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public Guid AttendantId { get; set; }
        public int MaxQuestions { get; set; }
        public int Total { get { return Answers.Count(a => a.IsCorrect); } }
        public List<Answer> Answers { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
    }
}