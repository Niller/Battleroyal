using GameEngine.Systems;
using Leopotam.Ecs;
using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    EcsSystems _systems;
    EcsWorld _world;

    void OnEnable()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world).
            Add(new DebugSystem());
        _systems.Init();

    }

    void Update()
    {
        _systems.Run();
        _world.EndFrame();
    }

    void OnDisable()
    {
        _systems.Destroy();
        _systems = null;
        _world.Destroy();
        _world = null;
    }
}
