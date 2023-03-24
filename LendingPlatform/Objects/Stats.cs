using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendingPlatform.Objects
{
    public class Stats
    {
        public double? loanTotal { get; set; }
        public int succesfulApplications { get; set; }
        public int unsuccesfulApplications { get; set; }
        public int applications { get; set; }
        public double averageLTV { get; set; }

    }
}
