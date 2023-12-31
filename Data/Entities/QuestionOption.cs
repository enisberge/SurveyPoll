﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveryPoll.DataAccess.Entities
{
    public class QuestionOption
    {
        public int Id { get; set; }
        public string OptionText { get; set;}
        public DateTime CreatedDate { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
