
# Darecali

**DA**TE **RE**CURRENCE **CA**LCULATION **LI**BRARY

>Pronounced **dah-rec-ah-lee**

Produces sequences of dates using a specified recurrence strategy.  Sequence controllers determine if and how a date sequence terminates.

Provides support for all recurrence strategies available in Microsoft Outlook, and then some.

## Sequence control options

* Non-terminating (infinite sequence; no end date)
* Terminates at a specified date
* Terminates after a specified number of occurrences
* Terminates at either an end date or a number of occurrences, whichever comes first

## Recurrence strategies

* Daily
 * Every _{n}_ day(s)
 * Every weekday
 * Every weekend day
* Weekly
 * Every day, every _{n}_ week(s)
 * Every _{n}_ week(s) on specified day(s)
* Monthly
 * Every _{n}_ month(s) on specified day(s)
 * Every _{frequency}_ _{day}_ of every _{n}_ month(s)
* Yearly
 * Every _{n}_ year(s) on the specified day and month
 * Every _{frequency}_ _{day}_ of every specified month, every _{n}_ years
 
## Considerations for consumers

### Start date influences first match
The start date affects the start of the period where recurrences are possible for the first match.  This can result in a first match that may seem counter intuitive. 

>Example: "Recur the 1st day of every month" matches the start date for the first match, no matter what day of the month the start date is.

### Strategies employ slippage
Strategies make no guarantee there will be a match in a particular period, which may seem counter intuitive.  

>Example: "Recur every month on the 30th day of the month" will never match in February.

## Specifying a strategy

Use the `Factory` to create a `SequenceController` with either:
* a concrete instance of an `IRecurrenceStrategy`, or
* use the Strategy Definition Language to have one created for you.

The `Factory` can also create concrete instances of `IRecurrenceStrategy` using the Strategy Definition Language.  

Call `Factory.GetStrategyDefinitionUsage()` to get the usage message, containing the syntax of the Strategy Definition Language:

```
Usage:
D[n]                     - Daily  : every n day(s)
                                    where n is an integer (default is 1)
Dwd                      - Daily  : every weekday
Dwe                      - Daily  : every weekend day
W[n]                     - Weekly : every day, every n week(s)
                                    where n is an integer (default is 1)
Wdays,n                  - Weekly : every n week(s) on specified day(s)
                                    where days are bitwise flags 1-127 (Sunday = 1)
                                          n is an integer
M[day[,n]]               - Monthly: every n month(s) on specified day
                                    where day is an integer 1-31 (default is 1)
                                          n is a positive integer (default is 1)
Mfrequency,day,n         - Monthly: every frequency day of every n month(s)
                                    where frequency is 1-4 for First-Fourth
                                                    or 'L' for Last
                                          day is 1-7 for Sunday-Monday
                                              or 'd' for day
                                              or 'wd' for weekday
                                              or 'we' for weekend day
                                          n is a positive integer
Y[month[,day[,n]]]       - Yearly : every n year(s) on the specified day and month
                                    where month is 1-12 (default is 1)
                                          day is 1-31 (default is 1)
                                          n is a positive integer (default is 1)
Yfrequency,day,month,n   - Yearly : the frequency day of specified month, every n year(s)
                                    where frequency is 1-4 for First-Fourth
                                                    or 'L' for Last
                                          day is 1-7 for Sunday-Monday
                                              or 'd' for day
                                              or 'wd' for weekday
                                              or 'we' for weekend day
                                          month is 1-12 for January-December
                                          n is a positive integer
```

>NOTE: The usage message will also be part of an `InvalidStrategyDefinitionException` if the provided strategy definition is invalid.