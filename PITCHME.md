# Mocking Frameworks are out. What's next?

#HSLIDE
## What Mocking Framework do you use?

#HSLIDE

##I am Boris Modylevsky

#HSLIDE

|       Class A       |
|---------------------|
|       Class B       | 
|---------------------|
|       Class C       |
|---------------------|
| External Dependency |

#HSLIDE

```C#
public class C
{
  public string GetContent(string file)
  { 
    return File.ReadAllText(file);
  }
}
```

#HSLIDE
```C#
[Test]
public void Test_With_Mocks()
{
    var fileSystem = A.Fake<IFileSystem>();
    A.CallTo(() => fileSystem.Directory.GetFiles(
	@"c:\", "*.txt", SearchOption.TopDirectoryOnly))
        .Returns(new [] { 
		@"c:\myfile.txt", @"c:\demo\jQuery.js", @"c:\demo\image.gif" });

    A.CallTo(() => fileSystem.File.ReadAllText(@"c:\myfile.txt"))
        .Returns("Testing is meh.");

    A.CallTo(() => fileSystem.File.ReadAllText(@"c:\demo\jQuery.js"))
        .Returns("some js");

    A.CallTo(() => fileSystem.File.ReadAllText(@"c:\demo\image.gif"))
        .Returns(Encoding.Default.GetString(new byte[] { 0x12, 0x34, 0x56, 0xd2 }));
}
```

#HSLIDE

## Takes a lot of time to write unit tests

#HSLIDE

## Problems

```C#
using (var stream = fileSystem.File.Open(
	file, FileMode.Open))
{
    byte[] buffer = new byte[1024];
    stream.Read(buffer, 0, 1024);
    var text = Encoding.Default.GetString(buffer);
    if (text != "Testing is awesome.")
        throw new NotSupportedException(" It's not me, it's you.");
}
```

#HSLIDE
```C#
[Test]
public void Test_With_MocksForFileOpen()
{
    var fileSystem = A.Fake<IFileSystem>();
    A.CallTo(() => fileSystem.Directory.GetFiles(@"c:\", "*.txt", SearchOption.TopDirectoryOnly))
        .Returns(new[] {@"c:\myfile.txt", @"c:\demo\jQuery.js", @"c:\demo\image.gif"});

    A.CallTo(() => fileSystem.File.Open(@"c:\myfile.txt", FileMode.Open))
        .Returns(new MemoryStream(Encoding.Default.GetBytes("Testing is meh.")));

    A.CallTo(() => fileSystem.File.Open(@"c:\demo\jQuery.js", FileMode.Open))
        .Returns(new MemoryStream(Encoding.Default.GetBytes("some js")));

    A.CallTo(() => fileSystem.File.Open(@"c:\demo\image.gif", FileMode.Open))
        .Returns(new MemoryStream(new byte[] {0x12, 0x34, 0x56, 0xd2}));
}
```

#HSLIDE

```C#
fileSystem.File.Open(file, FileMode.Open)

fileSystem.File.Open(file, FileMode.Open, fileAccess.Read)

fileSystem.File.Open(file, FileMode.Open, FileAccess.Read, FileShare.Read)
```

#HSLIDE

## A lot of test maintenance

#HSLIDE

```C#
public void CreateFile(string path, string content)
{
    fileSystem.File.WriteAllText(path, content);
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

## Mocking relies on assumptions on the functioning of the mocked part

#HSLIDE

## VEST Comes to Rescue

#HSLIDE

VEST Principles
* Looking from the outside 
* Use in-memory implementation

#HSLIDE

##Solve problems with VEST

```C#
using System.IO.Abstractions.TestingHelpers;
[Test]
public void Test_With_InMemory_FileSystem()
{
	var fileSystem = new MockFileSystem(
		new Dictionary<string, MockFileData>
    {
        {@"c:\myfile.txt", new MockFileData("Testing is meh.")},
        {@"c:\demo\jQuery.js", new MockFileData("some js")},
        {@"c:\demo\image.gif", new MockFileData(
		new byte[] {0x12, 0x34, 0x56, 0xd2})}
    });
}
```

#HSLIDE

##Unit Tests benefits

* Fast feedback
* Isolated
* Repeatable tests
* Self validating

#HSLIDE

* The End
github.com/borismod/vest


#HSLIDE
## Thank you
