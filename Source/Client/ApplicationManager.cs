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
            Add(new DebugSystem()).
            Add(new CreateViewSystem()).
            Add(new RemoveViewSystem());

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
