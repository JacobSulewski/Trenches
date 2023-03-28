namespace Trenches;
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
        => ((x, y), (width, height)) = (rect.Size, rect.Position);
    public static void Deconstruct(this RectangleF rect, out Vector2 position, out Vector2 size)
        => (position, size) = (rect.Position, rect.Size);
    public static void Deconstruct(this Point2 point, out float width, out float height)
        => (width, height) = (point.X, point.Y);
    public static void Deconstruct(this Size2 size, out float width, out float height)
        => (width, height) = (size.Width, size.Height);
    public static void Deconstruct(this Size size, out int width, out int height)
        => (width, height) = (size.Width, size.Height);
    public static Vector2 ScreenToWorld(this OrthographicCamera camera, Point screenPosition)
        => camera.ScreenToWorld(screenPosition.X, screenPosition.Y);
    public static Vector2 WorldToScreen(this OrthographicCamera camera, Point screenPosition)
        => camera.WorldToScreen(screenPosition.X, screenPosition.Y);

    public static ComponentMapper<T> Get<T>(this IComponentMapperService service) where T : class
        => service.GetMapper<T>();

    public static ValueTuple<ComponentMapper<T1>, ComponentMapper<T2>> Get<T1, T2>(this IComponentMapperService service) 
        where T1 : class
        where T2 : class
        => (service.GetMapper<T1>(), service.GetMapper<T2>());
    public static ValueTuple<ComponentMapper<T1>, ComponentMapper<T2>, ComponentMapper<T3>> Get<T1, T2, T3>(this IComponentMapperService service) 
        where T1 : class
        where T2 : class
        where T3 : class
        => (service.GetMapper<T1>(), service.GetMapper<T2>(), service.GetMapper<T3>());
    public static ValueTuple<ComponentMapper<T1>, ComponentMapper<T2>, ComponentMapper<T3>, ComponentMapper<T4>> Get<T1, T2, T3, T4>(this IComponentMapperService service) 
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        => (service.GetMapper<T1>(), service.GetMapper<T2>(), service.GetMapper<T3>(), service.GetMapper<T4>());
}