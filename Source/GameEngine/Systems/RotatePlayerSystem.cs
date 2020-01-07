using System;
using Leopotam.Ecs;

namespace GameEngine.Systems
{
    public class RotatePlayerSystem : IEcsRunSystem
    {
        private EcsFilter<RotateEventComponent> _rotateEventFilter = null;
        private EcsFilter<PlayerComponent, RotationComponent> _playerFilter = null;

        public void Run()
        {
            foreach (var i in _rotateEventFilter)
            {
                var rotateEvent = _rotateEventFilter.Get1[i];

                foreach (var j in _playerFilter)
                {
                    var rotation = _playerFilter.Get2[i];
                    rotation.Value = rotateEvent.Value;
                }
            }
        }
    }
}