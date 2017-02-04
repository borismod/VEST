## Mocking Frameworks are out. What's next?

#HSLIDE

##I am Boris Modylevsky

#HSLIDE

## What Mocking Frameworks are for?

#HSLIDE

### Mocking Frameworks' usage:
* **Replace external dependencies**
* Replace internal dependencies

#HSLIDE

```C#
public class MyClassC
{
  IFileSystem _fileSystem;
  
  public string GetContent(string file)
  { 
    if ( !_fileSystem.File.Exists(file) )
	{
	  throw new ArgumentException(file);
	}
    return _fileSystem.File.ReadAllText(file);
  }
}
```

#HSLIDE

```C#
[Test]
public void Test_With_MockingFramework()
{
    var fileSystem = A.Fake<IFileSystem>();
    A.CallTo(() => fileSystem.File.Exist(@"c:\myfile.txt"))
        .Returns(true);

    A.CallTo(() => fileSystem.File.ReadAllText(@"c:\myfile.txt"))
        .Returns("Testing is meh.");
}
```

#HSLIDE

## Lengthy setup in tests

#HSLIDE

```C#
public class MyClassC
{
  IFileSystem _fileSystem;
  
  public string GetContent(string file)
  { 
    if ( !_fileSystem.File.Exists(file) )
	{
	  throw new ArgumentException(file);
	}
	using (var stream = _fileSystem.File.Open(file, FileMode.Open))
    using(var streamReader = new StreamReader(stream))
    {
	  return streamReader.ReadToEnd();
	}
  }
}
```

#HSLIDE

## Prevents refactoring

#HSLIDE

```C#
FileStream Open(string path, FileMode mode);
FileStream Open(string path, FileMode mode, 
	FileAccess access);
FileStream Open(string path, FileMode mode, 
	FileAccess access, FileShare share);
```

#HSLIDE
```C#
[Test]
public void Test_With_MocksForFileOpen()
{
    var fileSystem = A.Fake<IFileSystem>();

    A.CallTo(() => fileSystem.File.Open(@"c:\myfile.txt", 
		FileMode.Open))
        .Returns(new MemoryStream(Encoding.Default.GetBytes("Testing is meh.")));

    A.CallTo(() => fileSystem.File.Open(@"c:\myfile.txt", 
		FileMode.Open, FileAccess.Read))
        .Returns(new MemoryStream(Encoding.Default.GetBytes("Testing is meh.")));

    A.CallTo(() => fileSystem.File.Open(@"c:\myfile.txt", 
		FileMode.Open, FileAccess.Read, FileShare.Read))
        .Returns(new MemoryStream(Encoding.Default.GetBytes("Testing is meh.")));
}
```

#HSLIDE

## A lot of test maintenance

#HSLIDE

```C#
public void CreateFile(string path, string content)
{
    _fileSystem.File.WriteAllText(path, content);
}

[Test]
public void Test_With_MocksForFileOpen()
{
    var fileSystem = A.Fake<IFileSystem>();

    A.CallTo(() => fileSystem.File.WriteAllText(
	@"c:\Dir\SubDir\myfile.txt", "file content"))
        .DoesNothing();
}
```

#HSLIDE

## Relies on wrong assumptions 

#HSLIDE

### Problems with mocking frameworks:
* Lengthy setup in tests
* Prevents refactoring
* A lot of test maintenance
* Relies on wrong assumptions 

#HSLIDE

## Use in-memory implementation

#HSLIDE

```C#
[Test]
public void GetContent_FileExists_ContentRead()
{
  var mockFileSystem = new MockFileSystem();
  mockFileSystem.AddFile(@"c:\myfile.txt", 
    new MockFileData("Testing is awesome."));
}

```
#HSLIDE

### In-memory file systems:
* .NET - System.IO.Abstractions.TestingHelpers
* Java - jimfs
* Python - pyfakefs
* Ruby - Fakefs

#HSLIDE

### In-memory databases:
* .NET - Effort for EF
* SQLite - cross platform 

#HSLIDE

### Mocking Frameworks' usage:
* Replace external dependencies
* **Replace internal dependencies**

#HSLIDE

### Problems with mocking frameworks:
* Lengthy setup in tests
* Prevents refactoring
* A lot of test maintenance
* Relies on wrong assumptions 

#HSLIDE

## Unit != Class

#HSLIDE

## Slice is a Unit

#HSLIDE

### **V**ertical **S**lice **T**esting

#HSLIDE

### **VEST** Principles
* Looking from the outside 
* Use in-memory implementation

#HSLIDE

###Unit Tests properties

* **F**ast
* **I**solated
* **R**epeatable
* **S**elf verifying
* **T**imely

#HSLIDE

## Thank you
github.com/borismod/vest