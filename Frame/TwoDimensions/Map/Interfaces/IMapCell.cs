namespace AoCTools.Frame.TwoDimensions.Map.Interfaces
{
    /// <summary>
    /// Interface of a map cell.
    /// </summary>
    public interface IMapCell
    {
        /// <summary>
        /// Raw cell content.
        /// </summary>
        object RawContent { get; }
        /// <summary>
        /// Cell coordinates.
        /// </summary>
        Coordinates Coordinates { get; }
    }
}