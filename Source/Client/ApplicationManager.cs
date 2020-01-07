using GameEngine;
using GameEngine.Systems;
using Leopotam.Ecs;
using UnityEngine;

public class ApplicationManager : MonoBehaviour
{

    void OnEnable()
    {
        GameManager.Instance.Initialize();

        GameManager.Instance.Systems.
            Add<DebugSystem>().
            Add<CreateViewSystem>().
            Add<RemoveViewSystem>();

        GameManager.Instance.Start();
    }

    void Update()
    {
        GameManager.Instance.Update();
    }

    void OnDisable()
    {
        GameManager.Instance.Dispose();
    }
}
