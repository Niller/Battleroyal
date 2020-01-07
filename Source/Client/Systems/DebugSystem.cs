using Leopotam.Ecs;
using UnityEngine;

namespace GameEngine.Systems
{
    class DebugSystem : IEcsRunSystem
    {
        public void Run()
        {
            Debug.Log(Time.deltaTime);
        }
    }
}
