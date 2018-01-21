using System;
using System.Collections.Generic;
using System.Text;

namespace Dto
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public QuestionType Type { get; set; }
        public IList<Answer> Answers { get; set; }
    }
}
