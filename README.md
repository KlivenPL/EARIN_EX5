# EARIN EX5
Genetic algorithm program, used to maximize a function   
Created in C# by Oskar Hącel & Marcin Lisowski  

Politechnika Warszawska, 03.2021

## Preparation
1. Download release package [here](https://github.com/KlivenPL/EARIN_EX5/releases) or build project yourself
1. Release package supports Windows machines
1. Be sure, that decimal separator is set to "." (dot) in Windows Control Panel
## Build
Build using newest version of Visual Studio 2019 (16.9.0) and .Net Core 3.0
## Usage
Run application (using command line) to display help, like:
```cmd
geneticalg --help
```
Follow instructions to proceed

### Parameters:
```cmd
  -D, --dimensions         Required. Specifies dimensionality

  -d, --d-num              Required. Range of searched integers, [-2^d, 2^d)

  -a, --a-mat              Required. Defines A matrix

  -b, --b-vec              Required. Defines b vector

  -c, --c-num              Required. Defines c real number

  -p, --population-size    Required. Defines population size

  -C, --crossover-prob     Required. Defines crossover probability

  -m, --mutation-prob      Required. Defines mutation probability

  -i, --iterations         Required. Defines number of iterations

  --help                   Display help screen.

  --version                Display version information.
```
### Example usage:
```cmd
geneticalg -D 3 -d 3 -a -2,1,0;1,-2,1;0,1,-2 -b -14,14,-2 -c -23.5 -p 50 -C 0.9 -m 0.05 -i 100
```
