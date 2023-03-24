namespace Trenches.Components;
class Physics {
    private Vector2 _direction = Vector2.Zero;
    public Vector2 Direction 
    {
        get =>_direction;
        set => _direction = value == Vector2.Zero ? Vector2.Zero : Vector2.Normalize(value); 
    }
    private float _magnitude = 0;
    public float Magnitude 
    {
        get => _magnitude;
        set => _magnitude = Math.Clamp(value, 0, Speed);
    }
    public Vector2 Velocity
    {
        get => Direction * Magnitude;
        set
        {
            Direction = value;
            Magnitude = value.Length();
        }
    }
    public float Speed { get; set; } = 1;
    public Vector2 Acceleration { get; set; } = Vector2.Zero;

    public override string ToString()
    {
        return $"Physics: [Direction: {Direction}, Magnitude: {Magnitude}, Velocity: {Velocity}, Speed: {Speed}, Acceleration: {Acceleration}]";
    }
}