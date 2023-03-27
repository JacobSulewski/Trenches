namespace Trenches.Utils;
static class Extensions {
    public static Entity Add<T>(this Entity entity) where T : class, new()
    {
        entity.Attach<T>(new());
        return entity;
    }
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
    public static void Deconstruct(this Size2 size, out float width, out float height)
    {
        width = size.Width;
        height = size.Height;
    }
    public static void Deconstruct(this Size size, out int width, out int height)
    {
        width = size.Width;
        height = size.Height;
    }
    public static Vector2 ScreenToWorld(this OrthographicCamera camera, Point screenPosition)
    {
        return camera.ScreenToWorld(screenPosition.X, screenPosition.Y);
    }
    public static Vector2 WorldToScreen(this OrthographicCamera camera, Point screenPosition)
    {
        return camera.WorldToScreen(screenPosition.X, screenPosition.Y);
    }
}