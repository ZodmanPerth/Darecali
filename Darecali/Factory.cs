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
            int n;
            switch (strategyDefinition[0])
            {
                case 'D':
                    n = 1;
                    if (strategyDefinition.Length > 1)
                    {
                        var remainder = strategyDefinition.Substring(1);
                        if (remainder == "wd")
                            return new EveryWeekDayStrategy();
                        if (!int.TryParse(remainder, out n))
                            throw new InvalidStrategyDefinitionException();
                    }
                    return new EveryNthDayStrategy(n);

                case 'W':
                    int dayFlags = (int)DayOfWeekFlags.EveryDay;
                    n = 1;
                    if (strategyDefinition.Length > 1)
                    {
                        var remainder = strategyDefinition.Substring(1);
                        var args = remainder.Split(',');

                        if (args.Length < 1 || args.Length > 2)
                            throw new InvalidStrategyDefinitionException();

                        if (args.Length == 1)
                            if (!int.TryParse(args[0], out n))
                                throw new InvalidStrategyDefinitionException();

                        if (args.Length == 2)
                            if
                            (
                                !int.TryParse(args[0], out dayFlags)
                                || dayFlags < 1
                                || dayFlags > 127
                                || !int.TryParse(args[1], out n)
                            )
                                throw new InvalidStrategyDefinitionException();
                    }

                    return new EveryNthWeekOnSpecificDaysStrategy((DayOfWeekFlags)dayFlags, n);
            }

            throw new InvalidStrategyDefinitionException();
        }

        public static string GetStrategyDefinitionUsage()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Usage:");
            sb.AppendLine("D[n]       - Daily : every n days, where n is an integer (default is 1)");
            sb.AppendLine("Dwd        - Daily : every weekday");
            sb.AppendLine("W[n]       - Weekly: every day, every n weeks, where n is an integer (default is 1)");
            sb.AppendLine("W[1-127,n] - Weekly: flagged days (default is every day), every n weeks, where n is an integer (default is 1)");

            return sb.ToString();
        }
    }
}