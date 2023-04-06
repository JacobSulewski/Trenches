using MonoGame.Extended.BitmapFonts;

namespace Trenches;
static class Utils 
{
    private static ContentManager Content;

    public static void Initialize(ContentManager content)
        => (Content) = (content);

    public static void DrawText(this SpriteBatch spriteBatch, string text, Vector2 position, Color color, float layerDepth, String font="arial"){
        //var font = Content.Load<BitmapFont>("Fonts/courier-new");
        var spriteFont = Content.Load<SpriteFont>($"Fonts/{font}");
        spriteBatch.DrawString(spriteFont, text, position, color, 0, Vector2.Zero, 1, SpriteEffects.None, layerDepth);
    } 
}