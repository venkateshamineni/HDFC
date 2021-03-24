using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication8
{
    public class Transaction
    {
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public int Amount { get; set; }
    }
}