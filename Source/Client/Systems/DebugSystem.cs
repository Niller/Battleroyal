using Leopotam.Ecs;
using UnityEngine;

namespace GameEngine.Systems
{
    internal class DebugSystem : IEcsRunSystem
    {
        public void Run()
        {
            Debug.Log(Time.deltaTime);
        }
    }
}
