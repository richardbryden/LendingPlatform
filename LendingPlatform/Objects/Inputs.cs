using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendingPlatform.Objects
{
    public class Inputs
    {
        public double? loanAmountInput { get; set; }
        public double? assetValue { get; set; }
        public int? creditScore { get; set; }

        public double ltv;
    }
}
