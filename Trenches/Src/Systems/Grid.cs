using System.Collections.Generic;
using System.Linq;

namespace Trenches.Pathing;
class Grid<T>
{
    private Cell<T>[,] _cells;
    public readonly Vector2 Position;
    public readonly Size2 CellSize;
    public readonly Size2 Size;
    public int Rows
        => (int)(Size.Height / CellSize.Height);
    public int Cols
        => (int)(Size.Width / CellSize.Width);
    public Grid(RectangleF rect, Point2 cellSize) 
    {     
        ((Position, Size), CellSize) = (rect, cellSize);
        _cells = new Cell<T>[Rows, Cols]; 
        for (int row = 0; row < Rows; ++row)
            for (int col = 0; col < Cols; ++col)
                _cells[row, col] = new Cell<T>
                    {
                        Position = Position + (new Vector2(row, col) * CellSize),
                        Size = CellSize,
                        Row = row,
                        Col = col
                    };
    }
    public Point WorldToIndex(Vector2 position)
        => (Size)(Size2)((position - Position) / CellSize);
    public Vector2 IndexToWorld(int row, int col)
        => (new Vector2(row, col) * CellSize) + Position;
    /* This is used to index from a world position */
    public T this[Vector2 position] 
    {
        get
        {
            var (row, col) = WorldToIndex(position);
            if (row >= Rows || col >= Cols || row < 0 || col < 0)
                return default(T);
            return _cells[row, col].Data;
        }
        set
        {
            var (row, col) = WorldToIndex(position);
            if (row >= Rows || col >= Cols || row < 0 || col < 0)
                return;
            _cells[row, col].Data = value;
        }
    }
    /* This is used to index from a Grid index */
    public T this[int row, int col] 
    { 
        get 
            => _cells[row, col].Data;
        set 
            => _cells[row, col].Data = value;
    }
    public IEnumerable<Cell<T>> GetOverlappingCells(RectangleF rect)
    {
        // find indices
        var (left, top) = WorldToIndex(rect.TopLeft); // Start indices
        var (right, bottom) = WorldToIndex(rect.BottomRight); // End indices
        Log.Information($"left:{left} right:{right} top:{top} bot:{bottom}");

        // create subarray
        var sizeX = right - left + 1;
        var sizeY = bottom - top + 1;
        var subArray = Enumerable.Range(left, sizeX)
                                 .SelectMany(i => Enumerable.Range(top, sizeY)
                                 .Select(j => _cells[i, j]));
        return subArray;
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var cell in _cells)
            cell.Draw(spriteBatch);
    }

    public class Cell<U>
    {
        protected Color Color 
            { get; set; } = Color.Black;
        required public Vector2 Position
            { get; init; }
        required public Size2 Size
            { get; init; }
        required public int Row
            { get; init; }
        required public int Col
            { get; init; }
        public U Data;
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle(new(Position, Size + new Size2(1, 1)), Color, 1f, LayerDepth.Debug);
            spriteBatch.DrawText(Data.ToString(), Position + ((Vector2)Size * .4f), Color, LayerDepth.Debug);
        }
        public override string ToString()
            => $"{{Color:{Color} Position:{Position} Size:{Size} Row:{Row} Col:{Col} Data:{Data}}}";
    } 
}