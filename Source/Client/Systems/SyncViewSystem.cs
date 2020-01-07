﻿using Leopotam.Ecs;
using UnityEngine;

namespace GameEngine.Systems
{
    public class SyncViewSystem : IEcsRunSystem
    {
        private EcsFilter<SyncViewComponent, ViewComponent> _syncViewFilter = null;

        public void Run()
        {
            foreach (var i in _syncViewFilter)
            {
                var entity = _syncViewFilter.Entities[i];
                var view = _syncViewFilter.Get2[i];

                //TODO [Alexander Borisov] Use different systems?
                var position = entity.Get<PositionComponent>();
                if (position != null)
                {
                    view.Value.transform.position = new Vector3(position.Value.X, 0, position.Value.Y);
                } 

                var rotation = entity.Get<RotationComponent>();
                if (rotation != null) 
                {
                    view.Value.transform.rotation = Quaternion.Euler(0, rotation.Value, 0);
                }
            }
            
        }
    }
}