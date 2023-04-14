using System.Collections.Generic;

namespace Trenches;
enum Tile: int
{
    Ground,
    Trench,
}
class Map
{
    readonly Grid<int> _grid; // This is an int so that debug grid looks nicer
    readonly Dictionary<Tile, Texture2D> _textureLookup;
    readonly ContentManager Content;
    public readonly RectangleF Bounds;
    public Map(ContentManager content, int width, int height)
    {
        (Content, Bounds) = (content, new(0, 0, width, height));
        _textureLookup = new()
        {
            {Tile.Ground, Content.Load<Texture2D>("Sprites/ground")},
            {Tile.Trench, Content.Load<Texture2D>("Sprites/trench")},
        };
        var rows = height / GameMain.PIXELS; // TODO: Hard coded
        var cols = width / GameMain.PIXELS; // TODO: Hard coded
        _grid = new(Vector2.Zero, rows, cols, GameMain.PIXELS);
        for (int row = 0; row < rows; ++row)
            for (int col = 0; col < cols; ++col)
            {
                _grid[row, col] = (int)Tile.Ground;
            }
    }
    
    public void Draw(SpriteBatch spriteBatch, bool debug=false)
    {
        for (var row = 0; row < _grid.Rows; ++row)
            for (var col = 0; col < _grid.Cols; ++col)
            {
                var cell = _grid.GetCell(row, col);
                var texture = _textureLookup[(Tile)cell.Data]; 
                spriteBatch.Draw(
                    texture,
                    cell.Position,
                    texture.Bounds,
                    Color.White,
                    0,
                    Vector2.Zero,
                    Vector2.One * 4, // TODO: Hard coded
                    SpriteEffects.None,
                    LayerDepth.Background
                );
            }
        if (debug)
            _grid.Draw(spriteBatch);
    }
}