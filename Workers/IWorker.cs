using AoCTools.Loggers;

namespace AoCTools.Workers
{
    public interface IWorker
    {
        long WorkOneStar(string dataPath, SeverityLevel logAbove);
        long WorkTwoStars(string dataPath, SeverityLevel logAbove);
    }
}