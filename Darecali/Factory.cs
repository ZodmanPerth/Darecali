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
            var args = strategyDefinition
                .Substring(1)
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            int n = 1;
            int day = 1;
            int month = 1;
            int dayFlags = (int)DayOfWeekFlags.EveryDay;
            Frequency frequency;
            SpecialDay specialDay;

            switch (strategyDefinition[0])
            {
                case 'D':
                    if (args.Any())
                    {
                        if (args.Length > 1)
                            throw new InvalidStrategyDefinitionException();

                        if (args[0] == "wd")
                            return new EveryNthWeekOnSpecificDaysStrategy(DayOfWeekFlags.WeekDays);
                        else if (args[0] == "we")
                            return new EveryNthWeekOnSpecificDaysStrategy(DayOfWeekFlags.WeekendDays);

                        if (!int.TryParse(args[0], out n))
                            throw new InvalidStrategyDefinitionException();
                    }
                    return new EveryNthDayStrategy(n);

                case 'W':
                    if (args.Any())
                    {
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
                    if (args.Any())
                    {
                        if (args.Length < 1 || args.Length > 3)
                            throw new InvalidStrategyDefinitionException();

                        if (args.Length < 3)
                            if (!int.TryParse(args[0], out day))
                                throw new InvalidStrategyDefinitionException();

                        if (args.Length == 2)
                            if (!int.TryParse(args[1], out n))
                                throw new InvalidStrategyDefinitionException();

                        if (args.Length == 3)
                        {
                            if (args[0].Length != 1)
                                throw new InvalidStrategyDefinitionException();

                            frequency = ParseFrequencyOrThrow(args[0]);
                            specialDay = ParseSpecialDayOrThrow(args[1]);

                            if (!int.TryParse(args[2], out n))
                                throw new InvalidStrategyDefinitionException();

                            return new EveryFrequencySpecialDayOfEveryNthMonthStrategy(frequency, specialDay, n);
                        }
                    }

                    return new EveryDayOfMonthEveryNthMonthStrategy(day, n);

                case 'Y':
                    if (args.Any())
                    {
                        if (args.Length > 4)
                            throw new InvalidStrategyDefinitionException();

                        if (args.Length < 4)
                        {
                            if (!int.TryParse(args[0], out month))
                                throw new InvalidStrategyDefinitionException();

                            if (args.Length > 1)
                                if (!int.TryParse(args[1], out day))
                                    throw new InvalidStrategyDefinitionException();

                            if (args.Length > 2)
                                if (!int.TryParse(args[2], out n))
                                    throw new InvalidStrategyDefinitionException();
                        }
                        else
                        {
                            if (args[0].Length != 1)
                                throw new InvalidStrategyDefinitionException();

                            frequency = ParseFrequencyOrThrow(args[0]);
                            specialDay = ParseSpecialDayOrThrow(args[1]);

                            if (!int.TryParse(args[2], out month))
                                throw new InvalidStrategyDefinitionException();

                            if (month < 1 || month > 12)
                                throw new InvalidStrategyDefinitionException();

                            if (!int.TryParse(args[3], out n))
                                throw new InvalidStrategyDefinitionException();

                            return new EveryFrequencySpecialDayOfMonthEveryNthYearStrategy(frequency, specialDay, month, n);
                        }
                    }

                    return new EveryNthYearOnSpecificMonthAndDayStrategy(month, day, n);
            }

            throw new InvalidStrategyDefinitionException();
        }

        static Frequency ParseFrequencyOrThrow(string arg)
        {
            int freq;
            if (int.TryParse(arg, out freq))
            {
                if (freq < 1 || freq > 4)
                    throw new InvalidStrategyDefinitionException();
                return (Frequency)freq;
            }
            else if (arg == "L")
                return Frequency.Last;
            else
                throw new InvalidStrategyDefinitionException();
        }

        static SpecialDay ParseSpecialDayOrThrow(string arg)
        {
            int spec;
            if (int.TryParse(arg, out spec))
            {
                if (spec < 1 || spec > 7)
                    throw new InvalidStrategyDefinitionException();
                return (SpecialDay)(spec - 1);
            }
            else if (arg == "d")
                return SpecialDay.Day;
            else if (arg == "wd")
                return SpecialDay.WeekDay;
            else if (arg == "we")
                return SpecialDay.WeekendDay;
            else
                throw new InvalidStrategyDefinitionException();
        }

        public static string GetStrategyDefinitionUsage()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Usage:");
            sb.AppendLine("D[n]                     - Daily  : every n day(s)");
            sb.AppendLine("                                    where n is an integer (default is 1)");
            sb.AppendLine("Dwd                      - Daily  : every weekday");
            sb.AppendLine("Dwe                      - Daily  : every weekend day");
            sb.AppendLine("W[n]                     - Weekly : every day, every n week(s)");
            sb.AppendLine("                                    where n is an integer (default is 1)");
            sb.AppendLine("Wdays,n                  - Weekly : every n week(s) on specified day(s)");
            sb.AppendLine("                                    where days are bitwise flags 1-127 (Sunday = 1)");
            sb.AppendLine("                                          n is an integer");
            sb.AppendLine("M[day[,n]]               - Monthly: every n month(s) on specified day");
            sb.AppendLine("                                    where day is an integer 1-31 (default is 1)");
            sb.AppendLine("                                          n is a positive integer (default is 1)");
            sb.AppendLine("Mfrequency,day,n         - Monthly: every frequency day of every n month(s)");
            sb.AppendLine("                                    where frequency is 1-4 for First-Fourth");
            sb.AppendLine("                                                    or 'L' for Last");
            sb.AppendLine("                                          day is 1-7 for Sunday-Monday");
            sb.AppendLine("                                              or 'd' for day");
            sb.AppendLine("                                              or 'wd' for weekday");
            sb.AppendLine("                                              or 'we' for weekend day");
            sb.AppendLine("                                          n is a positive integer");
            sb.AppendLine("Y[month[,day[,n]]]       - Yearly : every n year(s) on the specified day and month");
            sb.AppendLine("                                    where month is 1-12 (default is 1)");
            sb.AppendLine("                                          day is 1-31 (default is 1)");
            sb.AppendLine("                                          n is a positive integer (default is 1)");
            sb.AppendLine("Yfrequency,day,month,n   - Yearly : the frequency day of specified month, every n year(s)");
            sb.AppendLine("                                    where frequency is 1-4 for First-Fourth");
            sb.AppendLine("                                                    or 'L' for Last");
            sb.AppendLine("                                          day is 1-7 for Sunday-Monday");
            sb.AppendLine("                                              or 'd' for day");
            sb.AppendLine("                                              or 'wd' for weekday");
            sb.AppendLine("                                              or 'we' for weekend day");
            sb.AppendLine("                                          month is 1-12 for January-December");
            sb.AppendLine("                                          n is a positive integer");

            return sb.ToString();
        }
    }
}