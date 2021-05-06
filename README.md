# EARIN EX5
Program that is able to perform basic inference in Bayesian networks using the MCMC algorithm with Gibbs sampling algorithm.
Created in C# by Oskar Hącel & Marcin Lisowski  

Politechnika Warszawska, 03.2021

## Preparation
1. Download release package [here](https://github.com/KlivenPL/EARIN_EX5/releases) or build project yourself
1. Release package supports Windows machines
## Build
Build using newest version of Visual Studio 2019 (16.9.0) and .Net Core 3.0
## Usage
Run application (using command line) to display help, like:
```cmd
bayesiannet --help
```
Follow instructions to proceed

### Parameters:
```cmd

  -p, --network-path      Required. Bayesian network JSON file path

  -e, --evidence          Evidence, given in JSON object format without spaces, eg. {burglary:\"T\",alarm:\"T\"}. If defined, program expects query to be defined as well

  -m, --markov-blanket    If defined, program prints out Markov blanket for entered variable, eg. burglary

  -q, --queries           Selected query variables, eg. earthquake,John_calls

  -s, --steps             Required. The number of steps performed by MCMC algorithm

  --help                  Display this help screen.

  --version               Display version information.
```
### Example usage:
```cmd
bayesiannet -p Alarm.json -e {burglary:\"T\"} -m earthquake -q John_calls,earthquake,alarm,Marry_calls -s 1000000
```
(Please note escape sign ```\"T\"``` before quote marks
