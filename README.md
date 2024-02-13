# Advent of Code - Tools
This is a toolbox for my Advent of Code project.

# Content
## File Reader
Reads a text file.

## Two Dimensions
### Coordinates
Two values representing a position on a 2D plane.

### Cardinal Direction
Useful tools to manage north/south/west/east on a 2D plane.

### Map
A representation of a 2D plane.
Content may vary.

## Three Dimensions
### Coordinates
Three values representing a position on a 2D plane.

## Logger
Spamming managed via severity level.

## Numbers
### Range
Useful tools to manage a range of values.

### Maths
GCF, LCM, sum of first integers.

## Strings
### Formatter
Human-friendly time and distance.

### Helpers
Count differences between two strings.

## Workers
Interface and base class for a Daily Worker for Advent of Code.

# How to use
Get the sources, add a reference to AoCTools in your project dependencies, and you're good to go!

## Implement a Worker
For each day its own class inheriting WorkerBase.
```cs
class MyDay : WorkerBase
{
    private MyDataType _processedData;

    protected override void ProcessDataLines()
    {
        // Input has already been read and each line is in DataLines.
        // You may want to parse those lines as something else easier to handle.
    }

    // this is optional, but if you process DataLines, you might want to override this for better logging.
    public override object Data => _processedData;

    protected override long WorkOneStar_Implementation()
    {
        // Implement what you have to in order to pass the first star of the day.
    }

    protected override long WorkTwoStars_Implementation()
    {
        // Implement what you have to in order to pass the second star of the day.
    }
}
```
You can use your Worker this way:
```cs
var _dataPath = GetDataPath(); // gets your input file path
var _logLevel = SeverityLevel.Never; // everything equal or higher than _logLevel will be logged

var worker = new MyDay();
var star1 = worker.WorkOneStar(_dataPath, _logLevel);
var star2 = worker.WorkTwoStars(_dataPath, _logLevel);
```
