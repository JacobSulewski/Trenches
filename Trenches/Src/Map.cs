using System.Collections.Generic;

namespace Trenches;
enum Tile: int
{
    Ground,
    Trench,
}
class Map
{
    Grid<Texture2D> _grid; // This is an int so that debug grid looks nicer
    Dictionary<Tile, Texture2D> _textureLookupTable;
    public readonly RectangleF Bounds;
    required public ContentManager Content
        { private get; init; }
    public Map(int width, int height)
    {
        Bounds = new(0, 0, width, height);
        _textureLookupTable = new()
        {
            {Tile.Ground, Content.Load<Texture2D>("Sprites/ground")},
            {Tile.Trench, Content.Load<Texture2D>("Sprites/trench")},
        };
        var rows = height / GameMain.PIXELS;
        var cols = width / GameMain.PIXELS;
        _grid = new(Vector2.Zero, rows, cols, GameMain.PIXELS);
        for (int row = 0; row < rows; ++row)
            for (int col = 0; col < cols; ++col)
            {
                _grid[row, col] = _textureLookupTable[Tile.Ground];
            }
    }
    
    public void Draw(SpriteBatch spriteBatch, bool debug=false)
    {
        for (var row = 0; row < _grid.Rows; ++row)
            for (var col = 0; col < _grid.Cols; ++col)
            { 
                var cell = _grid.GetCell(row, col);
                spriteBatch.Draw(
                    cell.Data,
                    cell.Position,
                    cell.Data.Bounds,
                    Color.White,
                    0,
                    Vector2.Zero,
                    Vector2.One * 4,
                    SpriteEffects.None,
                    LayerDepth.Background
                );
            }
        if (debug)
            _grid.Draw(spriteBatch);
    }
}