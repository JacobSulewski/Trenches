namespace Trenches.Components;
class Physics {
    private Vector2 _direction = Vector2.UnitY;
    private float _magnitude;
    public Vector2 Direction 
    {
        get 
            => _direction;
        set 
            => _direction = value == Vector2.Zero ? _direction : Vector2.Normalize(value); 
    }
    public float Magnitude 
    {
        get 
            => _magnitude;
        set 
            => _magnitude = Math.Clamp(value, 0, Speed);
    }
    public Vector2 Velocity 
    {
        get 
            => Direction * Magnitude;
        set
            => (Direction, Magnitude) = (value, value.Length());
    }
    public readonly float Speed; 
    public Vector2 Acceleration = Vector2.Zero;
    public Physics(float speed) 
        => (Speed) = (speed);
    public override string ToString()
    {
        return $"{{Speed:{Speed} Velocity:{Velocity} Acceleration:{Acceleration}}}";
    }
}