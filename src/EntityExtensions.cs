namespace Trenches.Extensions;
static class ExtensionMethods {
    public static Entity Add<T>(this Entity entity, T component) where T : class 
    {
        entity.Attach(component);
        return entity;
    }

    public static void Deconstruct(this RectangleF rect, out float x, out float y, out float width, out float height)
    {
        x = rect.X;
        y = rect.Y;
        width = rect.Width;
        height = rect.Height;
    }

    public static void Deconstruct(this RectangleF rect, out Vector2 position, out Vector2 size)
    {
        position = rect.Position;
        size = rect.Size;
    }
}