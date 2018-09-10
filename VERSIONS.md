# xDGA

## Version 0.8
### September 2018

#### xDGA.CORE
* Implemented draft IEEE PC57.104/D4.1, October 2017

#### xDGA.ADDIN
* Added IEEE_C57104() funciton

## Version 0.7
### August 2018

#### xDGA.CORE
* Updated License copyright period.
* Fixed bug on TriangleFourRule. PD zone failure mode was missing a condition.
* Ensured consistency in Triangle and Pentagon areas rules by using only '<' and '>='.

## Version 0.6
### October 2017

#### xDGA.CORE
* Corrected Duval Pentagons areas. Thanks to Nick and Ania from work who
  pointed out the issue and suggested a solution.

## Version 0.5
### August 2017

#### xDGA.CORE
* Fixed error on Rogers Ratio FailureType calculation function
* Updated README file and fixed typos

## Version 0.4
### August 2017License HEader

#### xDGA.CORE
* Added Rogers Ratios algorithm

#### xDGA.ADDIN
* Added ROGERSRATIOS() funciton

## Version 0.3
### August 2017

#### xDGA.CORE
* Implemented Pentagonal coordinate systems and transformation functions
* Implemented tests for every Duval Triangle and Pentagone Zone
* Added Duval Pentagons algorithm

#### xDGA.ADDIN
* Added DUVALPENTAGONS() function

## Version 0.2
### July 2017

* Added Duval Triangles for Transformers, Reactors, Cables and On-Load Tap Changers
* Refactored some functions
* Added Failure Types (Codes and Descriptions) for OLTCs

## Version 0.1
### July 2017

* First version released
* Includes two functions: SERIALIZEDGA() and IEC_60599()
* Uses [ExcelDna](https://excel-dna.net/) and [Json.NET](http://www.newtonsoft.com/json)