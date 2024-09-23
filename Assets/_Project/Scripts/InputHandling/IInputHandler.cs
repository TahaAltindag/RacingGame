namespace _Project.Scripts
{
    // Interface defining input handling for throttle and brake
    public interface IInputHandler
    {
        // Represents the throttle input value (acceleration)
        float Throttle { get; }
        // Represents the brake input value (deceleration)
        float Brake { get; }
    }
}