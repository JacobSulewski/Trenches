using MonoGame.Extended.BitmapFonts;

namespace Trenches.Utils;
public static class Utils 
{
    private static ContentManager Content;
    private static SpriteBatch SpriteBatch;

    public static void Initialize(ContentManager content, SpriteBatch spriteBatch)
        => (Content, SpriteBatch) = (content, spriteBatch);

    public static void DrawText(string text, Vector2 position, Color color, float layerDepth){
        var font = Content.Load<BitmapFont>("Fonts/courier-new");
        SpriteBatch.DrawString(font, text, position, color, 0, Vector2.Zero, 1, SpriteEffects.None, layerDepth);
    } 
}