using System;

namespace Presentation.Models
{
    public class Answer {
        public Guid QuestionId { get; set; }
        public string Option { get; set; }
        public bool IsCorrect { get; set; }
        public DateTime AnswerAt { get; set; }
    }
}