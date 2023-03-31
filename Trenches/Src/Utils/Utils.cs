using MonoGame.Extended.BitmapFonts;

namespace Trenches;
public static class Utils 
{
    private static ContentManager Content;

    public static void Initialize(ContentManager content)
        => (Content) = (content);

    public static void DrawText(this SpriteBatch spriteBatch, string text, Vector2 position, Color color, float layerDepth, String font="arial"){
        //var font = Content.Load<BitmapFont>("Fonts/courier-new");
        var spriteFont = Content.Load<SpriteFont>($"Fonts/{font}");
        spriteBatch.DrawString(spriteFont, text, position, color, 0, Vector2.Zero, 1, SpriteEffects.None, layerDepth);
    } 

    public static Point2 Clamp(Point2 vector, RectangleF bounds)
    {
            float x = Math.Clamp(vector.X, 0, bounds.Width);
            float y = Math.Clamp(vector.Y, 0, bounds.Height);
            return new(x,y);
    }
}