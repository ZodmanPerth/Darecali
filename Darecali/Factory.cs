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
            int freq;
            Frequency frequency;
            int spec;
            SpecialDay specialDay;

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

                        if (args.Length < 1 || args.Length > 3)
                            throw new InvalidStrategyDefinitionException();

                        if (args.Length < 3)
                            if (!int.TryParse(args[0], out dayOfMonth))
                                throw new InvalidStrategyDefinitionException();

                        if (args.Length == 2)
                            if (!int.TryParse(args[1], out n))
                                throw new InvalidStrategyDefinitionException();

                        if (args.Length == 3)
                        {
                            if (args[0].Length != 1)
                                throw new InvalidStrategyDefinitionException();

                            if (int.TryParse(args[0], out freq))
                            {
                                if (freq < 1 || freq > 4)
                                    throw new InvalidStrategyDefinitionException();
                                frequency = (Frequency)freq;
                            }
                            else if (args[0] == "L")
                                frequency = Frequency.Last;
                            else
                                throw new InvalidStrategyDefinitionException();

                            if (int.TryParse(args[1], out spec))
                            {
                                if (spec < 1 || spec > 7)
                                    throw new InvalidStrategyDefinitionException();
                                specialDay = (SpecialDay)(spec - 1);
                            }
                            else if (args[1] == "d")
                                specialDay = SpecialDay.Day;
                            else if (args[1] == "wd")
                                specialDay = SpecialDay.WeekDay;
                            else if (args[1] == "we")
                                specialDay = SpecialDay.WeekendDay;
                            else
                                throw new InvalidStrategyDefinitionException();

                            if (!int.TryParse(args[2], out n))
                                throw new InvalidStrategyDefinitionException();

                            return new EveryFrequencySpecialDayOfEveryNthMonthStrategy(frequency, specialDay, n);
                        }
                    }

                    return new EveryDayOfMonthEveryNthMonthStrategy(dayOfMonth, n);

                case 'Y':
                    int day = 1;
                    int month = 1;
                    n = 1;
                    if (strategyDefinition.Length > 1)
                    {
                        var remainder = strategyDefinition.Substring(1);
                        var args = remainder.Split(',');

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

                            if (int.TryParse(args[0], out freq))
                            {
                                if (freq < 1 || freq > 4)
                                    throw new InvalidStrategyDefinitionException();
                                frequency = (Frequency)freq;
                            }
                            else if (args[0] == "L")
                                frequency = Frequency.Last;
                            else
                                throw new InvalidStrategyDefinitionException();

                            if (int.TryParse(args[1], out spec))
                            {
                                if (spec < 1 || spec > 7)
                                    throw new InvalidStrategyDefinitionException();
                                specialDay = (SpecialDay)(spec - 1);
                            }
                            else if (args[1] == "d")
                                specialDay = SpecialDay.Day;
                            else if (args[1] == "wd")
                                specialDay = SpecialDay.WeekDay;
                            else if (args[1] == "we")
                                specialDay = SpecialDay.WeekendDay;
                            else
                                throw new InvalidStrategyDefinitionException();

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

        public static string GetStrategyDefinitionUsage()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Usage:");
            sb.AppendLine("D[n]                       - Daily  : every n day(s)");
            sb.AppendLine("                                      where n is an integer (default is 1)");
            sb.AppendLine("Dwd                        - Daily  : every weekday");
            sb.AppendLine("Dwe                        - Daily  : every weekend day");
            sb.AppendLine("W[n]                       - Weekly : every day, every n week(s)");
            sb.AppendLine("                                      where n is an integer (default is 1)");
            sb.AppendLine("W[1-127[,n]]               - Weekly : every n week(s) on specified day(s)");
            sb.AppendLine("                                      where specified day(s) are bitwise flags (Sunday = 1)");
            sb.AppendLine("                                            n is an integer (default is 1)");
            sb.AppendLine("M[1-31[,n]]                - Monthly: on dayOfMonth every n month(s)");
            sb.AppendLine("                                      where dayOfMonth is an integer 1-31 (default is 1)");
            sb.AppendLine("                                            n is a positive integer (default is 1)");
            sb.AppendLine("M1-4|L,1-7|d|wd|we,n:      - Monthly: the frequency specialDay of every n month(s)");
            sb.AppendLine("                                      where frequency  is 1-4 for First-Fourth");
            sb.AppendLine("                                                       or 'L' for Last");
            sb.AppendLine("                                            specialDay is 1-7 for Sunday-Monday");
            sb.AppendLine("                                                       or 'd' for day");
            sb.AppendLine("                                                       or 'wd' for weekday");
            sb.AppendLine("                                                       or 'we' for weekend day");
            sb.AppendLine("                                            n is a positive integer");
            sb.AppendLine("Y[1-12[,1-31[,n]]]         - Yearly : every n year(s) on the specified month and day");
            sb.AppendLine("                                      where month is 1-12 (default is 1)");
            sb.AppendLine("                                      where day is 1-31 (default is 1)");
            sb.AppendLine("                                      where n is a positive integer (default is 1)");
            sb.AppendLine("Y1-4|L,1-7|d|wd|we,1-12,n: - Yearly : the frequency specialDay of monthIndex, every n year(s)");
            sb.AppendLine("                                      where frequency  is 1-4 for First-Fourth");
            sb.AppendLine("                                                       or 'L' for Last");
            sb.AppendLine("                                            specialDay is 1-7 for Sunday-Monday");
            sb.AppendLine("                                                       or 'd' for day");
            sb.AppendLine("                                                       or 'wd' for weekday");
            sb.AppendLine("                                                       or 'we' for weekend day");
            sb.AppendLine("                                            monthIndex is 1-12 for January-December");
            sb.AppendLine("                                            n is a positive integer");

            return sb.ToString();
        }
    }
}