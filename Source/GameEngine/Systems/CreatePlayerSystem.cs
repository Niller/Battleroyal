using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leopotam.Ecs;

namespace GameEngine.Systems
{
    public class CreatePlayerSystem : IEcsInitSystem
    {
        protected EcsEntity PlayerEntity;

        public virtual void Init()
        {
            PlayerEntity = GameManager.Instance.World.NewEntityWith(out PlayerComponent _, out PositionComponent _, out RotationComponent _);
        }
    }
}
