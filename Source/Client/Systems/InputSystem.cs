using Leopotam.Ecs;
using UnityEngine;
using Unity.Mathematics;
using Vector2 = UnityEngine.Vector2;

namespace GameEngine.Systems
{
    public class InputSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsEntity _rotateEventEntity;
        private EcsEntity _moveEventEntity;
        private EcsEntity _playerEntity;
        private readonly EcsFilter<PlayerComponent> _filter = null;

        public void Init()
        {
           _rotateEventEntity = GameManager.Instance.World.NewEntityWith(out RotateEventComponent _);
           _moveEventEntity = GameManager.Instance.World.NewEntityWith(out MoveEventComponent _);
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

            UpdateMouseInput();
            //Debug.Log($"current: {screenPoint} mouse: {mousePoint} angle: {angle}");

            UpdateKeyboardInput();
        }

        private void UpdateMouseInput()
        {
            var offset = new Vector2(Screen.width / 2f, Screen.height / 2f);
            var mousePoint = ((Vector2)Input.mousePosition - offset).normalized;
            var angle = -Vector2.SignedAngle(Vector2.up, mousePoint);
            angle = angle < 0 ? 360 + angle : angle;
            _rotateEventEntity.Get<RotateEventComponent>().Value = angle;
        }

        private void UpdateKeyboardInput()
        {
            var result = new float2(0, 0);
            var inputExist = false;
            if (Input.GetKey(KeyCode.A))
            {
                result.x -= 1;
                inputExist = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                result.x += 1;
                inputExist = true;
            }
            if (Input.GetKey(KeyCode.W))
            {
                result.y += 1;
                inputExist = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                result.y -= 1;
                inputExist = true;
            }

            if (inputExist)
            {
                result = math.normalize(result);
            }

            _moveEventEntity.Get<MoveEventComponent>().Value = result;
        }
    }
}