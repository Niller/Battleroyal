using Unity.Mathematics;

namespace GameEngine
{
    public class MovementComponent
    {
        public float2 OldDirection;
        public float2 Direction;
        public float MaxVelocity;
        public float CurrentAcceleration;
    }
}