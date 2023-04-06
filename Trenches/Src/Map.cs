using System.Collections.Generic;

namespace Trenches;
enum Tile
{
    Ground,
    Trench,
}
class Map
{
    Grid<Tile> _grid;
    Dictionary<Tile, string> _assetLookupTable = new()
    {
        {Tile.Ground, "ground"},
        {Tile.Trench, "trench"},
    };
    public readonly RectangleF Bounds;
    required public ContentManager Content
        { private get; init; }
    public Map(int width, int height)
    {
        Bounds = new(0, 0, width, height);
        var rows = height / GameMain.PIXELS;
        var cols = width / GameMain.PIXELS;
        _grid = new(Vector2.Zero, rows, cols, GameMain.PIXELS);
        for (int row = 0; row < rows; ++row)
            for (int col = 0; col < cols; ++col)
            {
                _grid[row, col] = Tile.Ground;
            }
    }
    public void Draw(SpriteBatch spriteBatch, bool debug=false)
    {
        Texture2D texture;
        for (var row = 0; row < _grid.Rows; ++row)
            for (var col = 0; col < _grid.Cols; ++col)
            { 
                var tile = _grid[row, col];
                var asset = _assetLookupTable[tile];
                var position = _grid.IndexToWorld(row, col);
                texture = Content.Load<Texture2D>("Sprites/" + asset);
                spriteBatch.Draw(
                    texture,
                    position,
                    texture.Bounds,
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