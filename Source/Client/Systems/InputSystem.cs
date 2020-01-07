using System.Linq;
using System.Runtime.InteropServices;
using Leopotam.Ecs;
using UnityEngine;

namespace GameEngine.Systems
{
    public class InputSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsEntity _rotateEventEntity;
        private EcsEntity _playerEntity;
        private EcsFilter<PlayerComponent> _filter = null;

        public void Init()
        {
           _rotateEventEntity = GameManager.Instance.World.NewEntityWith(out RotateEventComponent _);
           foreach (var i in _filter)
           {
               _playerEntity = _filter.Entities[i];
           }
        }

        public void Run()
        {
            var view = _playerEntity.Get<ViewComponent>();
            if (view == null)
            {
                return;
            }

            var currentPosition = view.Value.transform.position;
            var offset = new Vector2(Screen.width / 2f, Screen.height / 2f);
            //TODO [Alexander Borisov] Avoid Camera.main
            var screenPoint = (Vector2)Camera.main.WorldToScreenPoint(currentPosition) - offset;
            var mousePoint = ((Vector2)Input.mousePosition - offset).normalized;

            var angle = Vector2.SignedAngle(Vector2.up, mousePoint);
            //var angle = new Vector2(Mathf.Acos(mousePoint.x) * Mathf.Rad2Deg, Mathf.Asin(mousePoint.y) * Mathf.Rad2Deg);

            Debug.Log($"current: {screenPoint} mouse: {mousePoint} angle: {angle}");
            //Debug.Log(angle);

            _rotateEventEntity.Get<RotateEventComponent>().Value = -angle;
        }
    }
}