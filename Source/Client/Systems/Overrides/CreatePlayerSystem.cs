using Client.Configs.View;
using GameEngine;
using Leopotam.Ecs;

namespace Client.Systems.Overrides
{
    [SystemOverride(typeof(GameEngine.Systems.CreatePlayerSystem))]
    public sealed class CreatePlayerSystem : GameEngine.Systems.CreatePlayerSystem
    {
        public override void Init()
        {
            base.Init();

            PlayerEntity.Set<ViewSourceComponent>().ViewType = ViewType.Human;
            PlayerEntity.Set<SyncViewComponent>();
        }
    }
}
