using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Dto
{
    public class TransactionDto
    {
        public Nullable<int> To_Account { get; set; }
        public string Transaction_Mode { get; set; }
        public string Transaction_Password { get; set; }
        public Nullable<int> Amount { get; set; }
        public string Remarks { get; set; }
    }
}