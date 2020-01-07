using Client.Configs;
using Leopotam.Ecs;
using Leopotam.Ecs.Reactive;
using UnityEngine;

namespace GameEngine.Systems
{
    public class CreateViewSystem : EcsReactiveSystem<EcsFilter<ViewSourceComponent>>
    {
        protected override EcsReactiveType GetReactiveType()
        {
            return EcsReactiveType.OnAdded;
        }

        protected override void RunReactive()
        {
            foreach (ref var entity in this)
            {
                var viewSource = entity.Get<ViewSourceComponent>();
                var viewType = viewSource.ViewType;
                Debug.Log($"Try create view {viewType} for entity {entity}");

                if (!ConfigsManager.Instance.ViewConfig.GetValue(viewType, out var prefab))
                {
                    Debug.LogError($"Unable to find view for type {viewType}");
                    continue;
                }

                var instance = Object.Instantiate(prefab);

                var view = entity.Set<ViewComponent>();
                if (view != null)
                {
                    view.Value = instance;
                }
            }
        }
    }
}