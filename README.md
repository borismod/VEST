# VEST

VErtical Slice Testing, or VEST for short, is a software testing technique that recommends testing a whole component 
by looking at its external interface, API, UI and by replacing its dependencies with in-memory implementation.
These in-memory alternatives should be carefully chosen and this repo will try to collect them. 
If you know or own a library like that feel free to contribute via PR.

## In-Memory File System 

| Language 	| Library                               	|
|----------	|---------------------------------------	|
| C#       	| [System.IO.Abstractions.TestingHelpers][1] 	|
| Python   	| [pyfakefs][2]                              	|
| Java     	| [memoryfilesystem][3]                      	|
| Ruby     	| [Fakefs][4]                                	|


[1]: https://github.com/tathamoddie/System.IO.Abstractions
[2]: https://github.com/jmcgeheeiv/pyfakefs
[3]: https://github.com/marschall/memoryfilesystem
[4]: https://github.com/fakefs/fakefs

## In-Memory Database

| Language 	| Library                               |
|----------	|---------------------------------------|
| C#       	| [System.Data.SQLite][5] 				|
| Python   	| [sqlite3][6]                          |



[5]: [https://system.data.sqlite.org/index.html/doc/trunk/www/index.wiki]
[6]: [https://docs.python.org/2/library/sqlite3.html]

