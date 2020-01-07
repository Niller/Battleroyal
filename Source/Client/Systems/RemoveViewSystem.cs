using Leopotam.Ecs;
using Leopotam.Ecs.Reactive;
using UnityEngine;

namespace GameEngine.Systems
{
    public class RemoveViewSystem : EcsReactiveSystem<EcsFilter<ViewSourceComponent>>
    {
        protected override EcsReactiveType GetReactiveType()
        {
            return EcsReactiveType.OnRemoved;
        }

        protected override void RunReactive()
        {
            foreach (ref var entity in this)
            {
                var view = entity.Get<ViewComponent>();

                Debug.Log($"Try remove from entity {entity}");

                if (view.Value == null)
                {
                    Debug.LogWarning($"Cannot remove view due to view == null");
                    continue;
                }
                
                Object.Destroy(view.Value);
            }
        }
    }
}