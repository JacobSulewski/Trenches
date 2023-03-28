namespace Trenches.Pathing;
class Grid<T>
{
    private T[,] _cells;
    public readonly int CellSize;
    public Vector2 Position
        { get; }
    public int Width 
        { get; }
    public int Height
        { get; }
    public Grid(int x, int y, int width, int height, int cellSize) 
    {
         (Position, Width, Height, CellSize) = 
            (new(x, y), width, height, cellSize);

         _cells = new T[Width / CellSize, Height / CellSize];
         Log.Information($"()");
         for (int row = 0; row < Width / CellSize; ++row)
            for (int col = 0; col < Height / CellSize; ++col)
                _cells[row, col] = default(T);
    }



    public ValueTuple<int, int> ScreenToIndex(Vector2 position)
    {
        int row = (int)(position.X - Position.X) / CellSize;
        int col = (int)(position.Y - Position.Y) / CellSize;
        return (row, col);
    }

    public T this[Vector2 position] 
    {
        get
        {
            var (row, col) = ScreenToIndex(position);
            return _cells[row, col];
        }
        set
        {
            var (row, col) = ScreenToIndex(position);
            _cells[row, col] = value;
        }
    }
    public T this[float x, float y] 
    { 
        get => this[new(x, y)];
        set => this[new(x,y)] = value;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // TODO thickness should scale with camera zoom
        for (int i = 0; i < Width; i += CellSize)
            for (int j = 0; j < Height; j += CellSize)
            {
                var offset = Position + new Vector2(i, j);
                var size = Vector2.One * (CellSize + 1);
                var (row, col) = ScreenToIndex(offset);
                spriteBatch.DrawRectangle(new(offset, size), Color.Black, 1f, LayerDepth.Debug);
                Log.Debug($"[{row}, {col}], {_cells.GetUpperBound(0)} {_cells.GetUpperBound(1)}");
               // Utils.DrawText(_cells[row, col].ToString(), offset, Color.White, LayerDepth.Debug);
            } 
    }
}