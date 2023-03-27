namespace Trenches.Components;
class Physics {
    private Vector2 _direction = Vector2.Zero;
    private float _magnitude;
    public Vector2 Direction 
    {
        get 
            => _direction;
        set 
            => _direction = value == Vector2.Zero ? value : Vector2.Normalize(value); 
    }
    public float Magnitude 
    {
        get 
            => _magnitude;
        set 
            => _magnitude = Math.Clamp(value, 0, Speed);
    }
    public Vector2 Velocity
        => Direction * Magnitude;
#if false 
/* 
    When setting velocity using *= and direction is zero, then velocity will deconstruct 
    magnitude to zero and overwrite the value. To fix this, velocity is read only.
    Maybe magnitude and direction should depend on velocity instead of velocity on
    magnitude and direction. If its fixed you can uncomment acceleration in PhysicsSystem.
*/
    {
        get 
            => Direction * Magnitude;
        set
            => (Direction, Magnitude) = (value, value.Length());
    }
#endif
    public readonly float Speed; 
    //public Vector2 Acceleration = Vector2.Zero;
    public Physics(float speed) 
        => (Speed, _magnitude) = (speed, speed);
    public override string ToString()
    {
        return $"Physics: [Speed: {Speed}, Magnitude: {Magnitude}, Direction: {Direction}, Velocity: {Velocity}]";
    }
}