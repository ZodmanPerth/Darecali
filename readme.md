
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
 * Every _{n}_ days
 * Every weekday
 * Every weekend day
* Weekly
 * Every _{n}_ weeks on specified days of the week

## Specifying a strategy

Use the `Factory` to create a `SequenceController` with either:
* a concrete instance of an `IRecurrenceStrategy`, or
* use the Strategy Definition Language to have one created for you.

The `Factory` can also create instances of `IRecurrenceStrategy` using the Strategy Definition Language.  Alternatively, you can create them directly yourself..  

Call `Factory.GetStrategyDefinitionUsage()` to get the usage message, containing the syntax of the Strategy Definition Language:

```
D[n]       - Daily : every n days, where n is an integer (default is 1)"
Dwd        - Daily : every weekday
Dwe        - Daily : every weekend day
W[n]       - Weekly: every day, every n weeks, where n is an integer (default is 1)
W[1-127,n] - Weekly: on flagged days (default is every day), every n weeks, where n is an integer (default is 1)
```

>NOTE: The usage message will also be shown in an `InvalidStrategyDefinitionException` if the provided strategy definition is invalid.