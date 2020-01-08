using UnityEngine;

namespace Client.View
{
    public class RotationController : MonoBehaviour
    {
        private const float ChestRotationToleranceAngle = 75f;
        private const float BodyRotationTime = 0.2f;
        private const float ChestBodyDifferenceAngleSafeCoef = 1.2f;

        public Transform Chest;

        private float _bodyRotation;
        private float _chestRotation;

        private float _currentTime;
        private Quaternion _startRotation;

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
            transform.rotation = Quaternion.Lerp(_startRotation, Quaternion.Euler(0, _bodyRotation, 0), _currentTime / BodyRotationTime);
            Chest.rotation = Quaternion.Euler(0, _chestRotation, 0);

            var angle = Quaternion.Angle(transform.rotation, Chest.rotation);

            if (angle > ChestRotationToleranceAngle * ChestBodyDifferenceAngleSafeCoef)
            {
                _startRotation = Quaternion.Lerp(transform.rotation, Chest.rotation, 1f - ChestRotationToleranceAngle/angle);
            }

            _currentTime += Time.deltaTime;
        }
    }
}
