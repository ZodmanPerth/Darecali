
# Darecali

**DA**TE **RE**CURRENCE **CA**LCULATION **LI**BRARY

Pronounced **dah-rec-ah-lee**, like "directly" but without the "t".

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
 * Every _{n}_ week(s) on specified days of the week
* Monthly
 * Day _{dayOfMonth}_ of every _{n}_ month(s)

## Specifying a strategy

Use the `Factory` to create a `SequenceController` with either:
* a concrete instance of an `IRecurrenceStrategy`, or
* use the Strategy Definition Language to have one created for you.

The `Factory` can also create concrete instances of `IRecurrenceStrategy` using the Strategy Definition Language.  

Call `Factory.GetStrategyDefinitionUsage()` to get the usage message, containing the syntax of the Strategy Definition Language:

```
D[n]                  - Daily  : every n day(s)
                                 where n is an integer (default is 1)
Dwd                   - Daily  : every weekday
Dwe                   - Daily  : every weekend day
W[n]                  - Weekly : every day, every n week(s)
                                 where n is an integer (default is 1)
W[1-127[,n]]          - Weekly : every n week(s) on specified days
                                 where specified days are bitwise flags (Sunday = 1)
                                       n is an integer (default is 1)
M[1-31[,n]]           - Monthly: on dayOfMonth every n month(s)
                                 where dayOfMonth is an integer 1-31 (default is 1)
                                       n is a positive integer (default is 1)
M1-4|L,1-7|d|wd|we,n: - Monthly: the frequency specialDay of every n months
                                 where frequency is 1-4 for First-Fourth
                                                 or 'L' for Last
                                       specialDay is 1-7 for Sunday-Monday
                                                  or 'd' for day
                                                  or 'wd' for weekday
                                                  or 'we' for weekend day
                                       n is a positive integer
Y[1-12[,1-31[,n]]]    - Yearly : every n year(s) on the specified month and day
                                 where month is 1-12 (default is 1)
                                 where day is 1-31 (default is 1)
                                 where n is a positive integer (default is 1)
```

>NOTE: The usage message will also be part of an `InvalidStrategyDefinitionException` if the provided strategy definition is invalid.