using System;
using System.Collections.Generic;
using System.Text;

namespace Dto
{
    public class UserAnswer
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public string CustomAnswer { get; set; }
        public IList<UserAnswerAnswer> Answers { get; set; }
    }
}
