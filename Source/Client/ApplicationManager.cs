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
            Add<InputSystem>().
            Add<CreateViewSystem>().
            Add<RemoveViewSystem>().
            Add<SyncViewSystem>();

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
