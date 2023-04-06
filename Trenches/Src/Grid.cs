using System.Collections.Generic;
using System.Linq;

namespace Trenches;
class Grid<T>
{
    private Cell<T>[,] _cells;
    public readonly Vector2 Position;
    public readonly float CellSize;
    public readonly int Rows;
    public readonly int Cols;
    public RectangleF BoundingRectangle
        => new(Position, new Vector2(Rows, Cols) * CellSize);
    public Grid(Vector2 position, int rows, int cols, int cellSize) 
    {     
        (Position, Rows, Cols, CellSize) = (position, rows, cols, cellSize);
        _cells = new Cell<T>[Rows, Cols]; 
        for (int row = 0; row < Rows; ++row)
            for (int col = 0; col < Cols; ++col)
                _cells[row, col] = new Cell<T>
                    {
                        Position = Position + (new Vector2(col, row) * CellSize),
                        Size = Vector2.One * CellSize,
                        Row = row,
                        Col = col
                    };
    }
    public Point WorldToIndex(Vector2 position)
        => (Size)(Size2)((position - Position) / CellSize);
    public Vector2 IndexToWorld(int row, int col)
        => (new Vector2(row, col) * CellSize) + Position;
    public T this[int row, int col] 
    { 
        get 
            => _cells[row, col].Data;
        set 
            => _cells[row, col].Data = value;
    }
    public Cell<T> GetCell(int row, int col)
        => _cells[row, col];
    public IEnumerable<Cell<T>> GetOverlappingCells(RectangleF rect)
    {
        // find indices
        var (left, top) = WorldToIndex(rect.TopLeft); // Start indices
        var (right, bottom) = WorldToIndex(rect.BottomRight); // End indices

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
            spriteBatch.DrawRectangle(new(Position, Size + new Size2(1, 1) /* Offset so lines overlap nicely */), Color, 1f, LayerDepth.Debug);
            // spriteBatch.DrawText(Data.ToString(), Position + ((Vector2)Size * .4f), Color, LayerDepth.Debug);
        }
        public override string ToString()
            => $"{{Color:{Color} Position:{Position} Size:{Size} Row:{Row} Col:{Col} Data:{Data}}}";
    } 
}