
using System;
using System.Collections.Generic;

namespace Presentation.Models
{

    public class Question : IDocument
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Category { get; set; }
        public QuestionLevel Level { get; set; }
        public List<AnswerOption> Options { get; set; }
        public bool IsActive { get; set; }
    }
}