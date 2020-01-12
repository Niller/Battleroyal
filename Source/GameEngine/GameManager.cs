using GameEngine.Systems;
using Leopotam.Ecs;

namespace GameEngine
{
    public class GameManager
    {
        public EcsSystems Systems;
        public EcsWorld World;
        public float DeltaTime;

        private static GameManager _instance;
        public static GameManager Instance => _instance ?? (_instance = new GameManager());

        private GameManager()
        {

        }

        public void Initialize()
        {
            World = new EcsWorld();
            Systems = new EcsSystems(World);
            Systems.Add<CreatePlayerSystem>().Add<MovementSystem>().Add<MovementPlayerSystem>();
                //Add<RotatePlayerSystem>();
;        }

        public void Start()
        {
            Systems.Init();
        }

        public void Update(float deltaTime)
        {
            DeltaTime = deltaTime;
            Systems.Run();
            World.EndFrame();
        }

        public void Dispose()
        {
            Systems.Destroy();
            Systems = null;
            World.Destroy();
            World = null;
        }

    }
}
