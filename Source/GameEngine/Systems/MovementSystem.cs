using Leopotam.Ecs;
using Unity.Mathematics;

namespace GameEngine.Systems
{
    public class MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PositionComponent, MovementComponent> _moveFilter = null;
        public void Run()
        {
            foreach (var i in _moveFilter)
            {
                var position = _moveFilter.Get1[i];
                var movement = _moveFilter.Get2[i];

                position.Value += movement.Direction * movement.CurrentAcceleration;

                var dot = math.dot(movement.OldDirection, movement.Direction);
                movement.CurrentAcceleration -= dot;
                movement.CurrentAcceleration += GameManager.Instance.DeltaTime;

                movement.OldDirection = movement.Direction;
            }
        }
    }
}