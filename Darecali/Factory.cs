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
        public static SequenceController CreateController(DateTime startDate, string strategyDefinition, DateTime? endDate = null, int? numberOfOccurrences = null)
        {
            return CreateController(startDate, CreateStrategy(strategyDefinition), endDate, numberOfOccurrences);
        }

        public static SequenceController CreateController(DateTime startDate, IRecurrenceStrategy strategy, DateTime? endDate = null, int? numberOfOccurrences = null)
        {
            if (strategy == null) throw new ArgumentNullException("strategy");
            return new SequenceController(startDate, strategy, endDate, numberOfOccurrences);
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
                            return new EveryNthWeekOnSpecificDaysStrategy(DayOfWeekFlags.WeekDays);

                        if (remainder == "we")
                            return new EveryNthWeekOnSpecificDaysStrategy(DayOfWeekFlags.WeekendDays);

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

                case 'M':
                    int dayOfMonth = 1;
                    n = 1;
                    if (strategyDefinition.Length > 1)
                    {
                        var remainder = strategyDefinition.Substring(1);
                        var args = remainder.Split(',');

                        if (args.Length < 1 || args.Length > 2)
                            throw new InvalidStrategyDefinitionException();

                        if (args.Length == 1)
                            if (!int.TryParse(args[0], out dayOfMonth))
                                throw new InvalidStrategyDefinitionException();

                        if (args.Length == 2)
                            if
                            (
                                !int.TryParse(args[0], out dayOfMonth)
                                || !int.TryParse(args[1], out n)
                            )
                                throw new InvalidStrategyDefinitionException();
                    }

                    return new EveryDayOMonthEveryNthMonthStrategy(dayOfMonth, n);
            }

            throw new InvalidStrategyDefinitionException();
        }

        public static string GetStrategyDefinitionUsage()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Usage:");
            sb.AppendLine("D[n]              - Daily  : every n days, where n is an integer (default is 1)");
            sb.AppendLine("Dwd               - Daily  : every weekday");
            sb.AppendLine("Dwe               - Daily  : every weekend day");
            sb.AppendLine("W[n]              - Weekly : every day, every n weeks, where n is an integer (default is 1)");
            sb.AppendLine("W[1-127[,n]]      - Weekly : on flagged days (default is every day), every n weeks, where n is an integer (default is 1)");
            sb.AppendLine("M[dayOfMonth[,n]] - Monthly: on dayOfMonth, every n months, where dayOfMonth is an integer between 1 and 31 inclusive (default is 1), and n is a positive integer (default is 1)");

            return sb.ToString();
        }
    }
}