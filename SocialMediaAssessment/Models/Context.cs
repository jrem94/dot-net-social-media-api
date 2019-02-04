using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialMediaAssessment.Models
{
    public class Context
    {
        public Tweet Target { get; set; }
        public List<Tweet> Before { get; set; }
        public List<Tweet> After { get; set; }

        public Context() { }

        public Context(Tweet target, List<Tweet> before, List<Tweet> after)
        {
            this.Target = target;
            this.Before = before;
            this.After = after;
        }

        public List<Tweet> FlattenBefore()
        {
            return this.Before;
        }

        public List<Tweet> FlattenAfter()
        {
            return this.After;
        }

        public List<Tweet> GetFullContext()
        {
            return this.Before;
        }
    }
}