Darecali
==
**DA**TE **RE**CURRENCE **CA**LCULATION **LI**BRARY

Pronounced **dah-rec-ah-lee**, like the word "directly" but without the "t".

Produces sequences of dates using a recurrence strategy.  Sequence controllers allow for finite and infinte sequences.

Based on Microsoft Outlook Implementation
--
Sequence controllers determine if and how a date sequence terminates:

* Non-terminating (infinite sequence; no end date)
* Terminates at a specific (end) date
* Terminates after a specified number of occurrences 

Recurrence strategies determine how a date sequence unfolds:

* Daily, every N days
* Weekly, every N weeks on specified days

Additional Features
--
 
* Sequence controllers can specify an end date **and** a number of occurrences; whichever comes first terminates the sequence.
* Create recurrence strategies using a strategy definition language (see below)

Strategy Definition Language
--

Use the Darecali `Factory` to create a `RecurrenceController` with either
* a concrete instance of an `IRecurrenceStrategy`, or
* use the Strategy Definition Language to have one created for you.  

Call `Factory.GetStrategyDefinitionUsage()` to get the usage message:

```
D[n]       - Daily : every n days, where n is an integer (default is 1)"
W[n]       - Weekly: every day, every n weeks, where n is an integer (default is 1)
W[1-127,n] - Weekly: flagged days (default is every day), every n weeks, where n is an integer (default is 1)
```

>NOTE: The useage message will be shown in an exception if the provided definition cannot be parsed.