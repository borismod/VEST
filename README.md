# VEST

VErtical Slice Testing, or VEST for short, is a software testing technique that recommends testing a whole component 
by looking at its external interface, API, UI and by replacing its dependencies with in-memory implementation.
These in-memory alternatives should be carefully chosen and this repo will try to collect them. 
If you know or own a library like that feel free to contribute via PR.

## In-Memory File System 

| Language 	| Library                               	| Project Site                                              |
|----------	|-------------------------------------------|-----------------------------------------------------------|
| C#       	| System.IO.Abstractions.TestingHelpers 	| https://github.com/tathamoddie/System.IO.Abstractions     |
| Python   	| pyfakefs                                  | https://github.com/jmcgeheeiv/pyfakefs                    |
| Java     	| memoryfilesystem                          | https://github.com/marschall/memoryfilesystem             |
| Ruby     	| Fakefs                                    | https://github.com/fakefs/fakefs                          |


## In-Memory Database

| Language 	            | Library                               |                                                                       |
|-----------------------|---------------------------------------|-----------------------------------------------------------------------|
| C# + Entity Framework | Effort                           	    | https://github.com/tamasflamich/effort                                |
| C# + NHibernate       | SQLite + NHibernate                   | https://gist.github.com/akimboyko/4319926                             |
| Python   	            | sqlite3                               | https://docs.python.org/2/library/sqlite3.html                        |
| Cross-platform        | SQLite                                | https://system.data.sqlite.org/index.html/doc/trunk/www/index.wiki    |


