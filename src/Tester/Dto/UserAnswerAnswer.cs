using System;
using System.Collections.Generic;
using System.Text;

namespace Dto
{
    public class UserAnswerAnswer
    {
        public int UserAnswerId { get; set; }
        public UserAnswer UserAnswer { get; set; }

        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
    }
}
