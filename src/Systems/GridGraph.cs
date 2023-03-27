using System.Collections.Generic;

using Trenches.Components;

namespace Trenches.Pathing;
class GridGraph
{
    private int[,] _nodes;
    private HashSet<Collider> entities;
    public readonly int CellSize;
    public int Width 
        { get; }
    public int Height
        { get; }
    public GridGraph(int width, int height, int cellSize) 
    {
         (Width, Height, CellSize) = (width, height, cellSize);
         _nodes = new int[Width / CellSize, Height / CellSize];
         for (int row = 0; row < Width / cellSize; ++row)
            for (int col = 0; col < Height / CellSize; ++col)
                _nodes[row, col] = 0;
    }

    public void Add(Collider collider) { }
    public void GetCoords() { }
    public void Draw(SpriteBatch spriteBatch)
    {
        // TODO thickness should scale with camera zoom
        for (int i = 0; i < Width; i += CellSize)
            for (int j = 0; j < Height; j += CellSize)
            {
                spriteBatch.DrawRectangle(new(i, j, CellSize + 1, CellSize + 1), Color.Black, 1f, LayerDepth.Debug);
//                Utils.Utils.DrawText(_nodes[i / CellSize, j / CellSize].ToString(), new(i, j), Color.White, LayerDepth.Debug);
            }
    }
}