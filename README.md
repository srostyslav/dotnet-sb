# Smartbonus package for C# language

This package provides C# implementation of Smartbonus api. Supported all public api of smartbonus.

With 100% test coverage.

## Installation

Package availble on [NuGet](https://www.nuget.org/packages/SBonus/)

## Requirements

Newtonsoft.json (JSON.NET)

## Example

```csharp
using SBonus;
using System;

var store = new Store("your store id", "https://your.smartbonus.com/api/v2/");

// Get smartbonus info about client
var client = store.GetClient("0555555555");

Console.WriteLine($"Client <phone: {client.Phone}; name: {client.Name}; balance: {client.Balance}>");

// see tests for more

```
