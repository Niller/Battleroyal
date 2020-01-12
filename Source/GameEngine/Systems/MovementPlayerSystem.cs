using Leopotam.Ecs;

namespace GameEngine.Systems
{
    public class MovementPlayerSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MoveEventComponent> _moveEventFilter = null;
        private readonly EcsFilter<PlayerComponent, MovementComponent> _playerFilter = null;

        public void Run()
        {
            foreach (var i in _moveEventFilter)
            {
                var moveEvent = _moveEventFilter.Get1[i];

                foreach (var j in _playerFilter)
                {
                    var movement = _playerFilter.Get2[i];
                    movement.Direction = moveEvent.Value;
                }
            }
        }
    }
}