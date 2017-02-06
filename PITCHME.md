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
using System.IO.Abstractions;

public class MyClass
{
  private IFileSystem _fileSystem;
  ...
  public string GetContent(string file)
  { 
    if ( !_fileSystem.File.Exists(file) )
	  throw new ArgumentException(file);
	  
    return _fileSystem.File.ReadAllText(file);
  }
}
```

#HSLIDE

```C#
using FakeItEasy;
[Test] 
public void GetContent_FileExists_ReturnsContent()
{
  IFileSystem fileSystem = A.Fake<IFileSystem>();
  A.CallTo(() => fileSystem.File.Exists(@"c:\myfile.txt"))
     .Returns(true);
  A.CallTo(() => fileSystem.File.ReadAllText(@"c:\myfile.txt"))
        .Returns("Testing is meh.");
		
  MyClass myClass = new MyClass(fileSystem);
  string actualContent = myClass.GetContent(@"c:\myfile.txt");
  
  Assert.AreEqual(actualContent, @"c:\myfile.txt")
}
```

#HSLIDE

## Lengthy setup in tests

#HSLIDE

```C#
using System.IO.Abstractions;
public class MyClass
{
  private IFileSystem _fileSystem;
  
  public string GetContent(string file)
  { 
    if ( !_fileSystem.File.Exists(file) )
	  throw new ArgumentException(file);

	using (var stream = _fileSystem.File.Open(file, FileMode.Open))
    using(var streamReader = new StreamReader(stream))
	  return streamReader.ReadToEnd();
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
public void GetContent_FileExists_ReturnsContent()
{
  MemoryStream stream = new MemoryStream(Encoding.Default.GetBytes("Testing is meh."));
  IFileSystem fileSystem = A.Fake<IFileSystem>();
  A.CallTo(() => fileSystem.File.Open(@"c:\myfile.txt", 
    FileMode.Open)).Returns(stream);
  A.CallTo(() => fileSystem.File.Open(@"c:\myfile.txt", 
    FileMode.Open, FileAccess.Read))
    .Returns(stream);
  A.CallTo(() => fileSystem.File.Open(@"c:\myfile.txt", 
    FileMode.Open, FileAccess.Read, FileShare.Read))
    .Returns(stream);
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
public void CreateFile_DirDoesNotExist_FileCreated()
{
    IFileSystem fileSystem = A.Fake<IFileSystem>();

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
using System.IO.Abstractions.TestingHelpers;
[Test]
public void GetContent_FileExists_ContentRead()
{
  MockFileSystem mockFileSystem = new MockFileSystem();
  mockFileSystem.AddFile(@"c:\myfile.txt", 
    new MockFileData("Testing is awesome."));
  
  MyClass myClass = new MyClass(mockFileSystem);
  string actualContent = myClass.GetContent(@"c:\myfile.txt");
  
  Assert.AreEqual(actualContent, "Testing is awesome."); 
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

## What is unit test?

#HSLIDE

###Unit Tests properties

* **F**ast
* **I**solated
* **R**epeatable
* **S**elf verifying
* **T**imely

#HSLIDE

## Unit != Class

#HSLIDE

## Slice is a Unit

#HSLIDE

## Vertical Slice Testing or VEST

#VSLIDE

### **VEST** Principles
* Looking from the outside 
* Use in-memory implementation

#VSLIDE

## Show me the code

#VSLIDE

## Thank you
github.com/borismod/vest