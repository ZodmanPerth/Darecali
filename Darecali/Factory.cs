using Darecali.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darecali
{
    public static class Factory
    {
        public static RecurrenceController CreateController(DateTime startDate, string strategyDefinition, DateTime? endDate = null, int? numberOfOccurrences = null)
        {
            return CreateController(startDate, CreateStrategy(strategyDefinition), endDate, numberOfOccurrences);
        }

        public static RecurrenceController CreateController(DateTime startDate, IRecurrenceStrategy strategy, DateTime? endDate = null, int? numberOfOccurrences = null)
        {
            if (strategy == null) throw new ArgumentNullException("strategy");
            return new RecurrenceController(startDate, strategy, endDate, numberOfOccurrences);
        }

        public static IRecurrenceStrategy CreateStrategy(string strategyDefinition)
        {
            if (string.IsNullOrWhiteSpace(strategyDefinition))
                throw new InvalidStrategyDefinitionException();

            strategyDefinition = strategyDefinition.Trim();
            switch (strategyDefinition[0])
            {
                case 'D':
                    int n = 1;
                    if (strategyDefinition.Length > 1)
                    {
                        var remainder = strategyDefinition.Substring(1);
                        if (!int.TryParse(remainder, out n))
                            throw new InvalidStrategyDefinitionException();
                    }
                    return new EveryNthDayStrategy(n);
            }

            throw new InvalidStrategyDefinitionException();
        }

        public static string GetStrategyDefinitionUsage()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Usage:");
            sb.AppendLine("D[n] - Daily: every n days, where n is an integer (default is 1)");

            return sb.ToString();
        }
    }
}