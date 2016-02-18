using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darecali
{
    public class InvalidStrategyDefinitionException : Exception
    {
        public InvalidStrategyDefinitionException()
            : base("The strategy definition is invalid\n" + Factory.GetStrategyDefinitionUsage())
        { }

        public InvalidStrategyDefinitionException(string strategyDefinition)
            : base(string.Format("The strategy definition '{0}' is invalid\n{1}", strategyDefinition, Factory.GetStrategyDefinitionUsage()))
        { }
    }
}
