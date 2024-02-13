using AoCTools.Frame.TwoDimensions.Map.Interfaces;

namespace AoCTools.Frame.TwoDimensions.Map.Abstracts
{
    /// <summary>
    /// Interface of a map cell with a non-char content.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MapCell<T> : IMapCell
    {
        /// <summary>
        /// Creates a MapCell.
        /// </summary>
        /// <param name="content">Typed content of the cell.</param>
        /// <param name="row">Row position of the cell.</param>
        /// <param name="col">Column position of the cell.</param>
        protected MapCell(T content, int row, int col)
        {
            RawContent = content;
            Content = content;
            Coordinates = new Coordinates(row, col);
        }

        /// <inheritdoc/>
        public object RawContent { get; }
        /// <summary>
        /// Cell content.
        /// </summary>
        public T Content { get; protected set; }
        /// <inheritdoc/>
        public virtual Coordinates Coordinates { get; }
    }
}