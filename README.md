# Consistency Calculator

A desktop application for TCG's, made with Yu-Gi-Oh! deck consistency analysis, focused on probabilistic simulations and flexible logical conditions.

## Features

- Deck editor with card quantities and multiple tags per card;
- Deck import from .ydk files (Main Deck only);
- Imported decks have their cards curated from the [YgoProDeck API](https://ygoprodeck.com/api-guide/), bringing pre-selected tags such as attribute and race(type);
- Simultaneous simulation of 5-card and 6-card hands;
- Condition evaluation such as: 

        starter >= 1 && brick < 1

- Results shown as percentage per tag and overall draw rate;
- Clean architecture with a UI-independent Core, perfect for implementation of different UI's.

## How it works

The application uses statistical simulation (Monte Carlo) to estimate real hand probabilities based on tagged cards and user-defined conditions.

## Why Monte Carlo?

While exact hypergeometric formulas work well for simple scenarios, real Yu-Gi-Oh! decks often involve:

- Multiple overlapping categories (tags);
- Complex logical conditions (AND, OR, comparisons);
- Cards that belong to more than one functional role.

 Monte Carlo simulation allows the system to:

- Evaluate arbitrarily complex conditions;
- Support multiple tags per card;
- Scale naturally as deck logic becomes more expressive.

With a sufficiently high number of iterations, the results converge to accurate probability estimates while remaining flexible and easy to extend.

## Tech Stack And Architecture

- .NET / C#;
- WPF (MVVM);
- Standalone Core for parsing, rules, and simulation logic.

## Status

First functional version, ready for use and testing.

## Future Implementations and Improvements

Although I don't pretend to work on this project in the near future, I have the following list of improvements to the application:

 - Create a local repo for saved decks, so you can change between decks like in other simulators (think something like Ygopro deck selection);
 - Implement a typeahead to the card name field in the deck editor;
 - auto color lines in deck editor if using the .ydk import button;
 - general improvements to the UI, like adding placeholders in text fields (just noticed that lol), dark mode, general identation and coloring of text.

 ## Suggestions

 If you have any suggestions that are not cited above you can contact me via Discord @eh_o_teytey or [opening an issue here](https://github.com/matheusluizgarcia/ConsistencyCalculator/issues/new). 