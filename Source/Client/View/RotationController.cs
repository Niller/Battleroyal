using UnityEngine;

namespace Client.View
{
    public class RotationController : MonoBehaviour
    {
        private const float ChestRotationToleranceAngle = 75f;
        private const float BodyRotationTime = 0.3f;
        private const float ChestBodyDifferenceAngleSafeCoef = 1.2f;

        private readonly int _rotationSpeedPropertyId = Animator.StringToHash("RotationSpeed");

        public Transform Chest;
        public Animator Animator;

        private float _bodyRotation;
        private float _chestRotation;

        private float _currentTime;
        private Quaternion _startRotation = Quaternion.identity;

        public void SetRotation(float value)
        {
            var diff = value - _bodyRotation;
            var absDiff = Mathf.Abs(diff);

            //Debug.Log($"bodyRotation: {_bodyRotation} _chestRotation: {_chestRotation} diff: {diff}");

            if (absDiff < ChestRotationToleranceAngle)
            {
                _chestRotation = value;
            }
            else
            {
                _chestRotation = value;
                _bodyRotation = value;

                _currentTime = 0;
                _startRotation = transform.rotation;
            }
        }

        private void LateUpdate()
        {
            var ratio = _currentTime / BodyRotationTime;
            transform.rotation = Quaternion.Lerp(_startRotation, Quaternion.Euler(0, _bodyRotation, 0), ratio);

            var angle = Quaternion.Angle(transform.rotation, Chest.rotation);

            if (angle > ChestRotationToleranceAngle * ChestBodyDifferenceAngleSafeCoef)
            {
                _startRotation = Quaternion.Lerp(transform.rotation, Chest.rotation, 1f - ChestRotationToleranceAngle/angle);
            }

            var rotationSpeed = 0.5f;
            if (ratio < 1)
            {
                rotationSpeed = _startRotation.eulerAngles.y < _bodyRotation ?
                    CalculateAnimationWeight(0.5f, 1f, ratio) : CalculateAnimationWeight(0.5f, 0, ratio);
            }

            Animator.SetFloat(_rotationSpeedPropertyId, rotationSpeed);


            Chest.rotation = Quaternion.Euler(0, _chestRotation, 0);
            _currentTime += Time.deltaTime;
        }

        private float CalculateAnimationWeight(float from, float to, float ratio)
        {
            if (ratio <= 0.5f)
            {
                return Mathf.Lerp(from, to, ratio * 2);
            }

            ratio -= 0.5f;
            return Mathf.Lerp(to, @from, ratio * 2);

        }
    }
}
