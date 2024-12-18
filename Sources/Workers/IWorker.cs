using AoCTools.Loggers;

namespace AoCTools.Workers
{
    public interface IWorker
    {
        long WorkOneStar(string dataPath, SeverityLevel logAbove);
        long WorkTwoStars(string dataPath, SeverityLevel logAbove);
        
        string WorkOneStar_String(string dataPath, SeverityLevel logAbove);
        string WorkTwoStars_String(string dataPath, SeverityLevel logAbove);
    }
}