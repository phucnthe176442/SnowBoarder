﻿using UnityEngine;

namespace snow_boarder
{
    public class CameraFollow : MonoBehaviour
    {
        private const float SMOOTH_TIME = 0.3f;
        protected Vector3 velocity = Vector3.zero;
        private Vector3 _targetPosition = Vector3.zero;

        private void Update()
        {
            if (GameManager.Instance.CameraTarget == null) return;
            _targetPosition = GameManager.Instance.CameraTarget.position;
            _targetPosition.z = -1f;
        }

        private void LateUpdate()
        {
            transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref velocity, SMOOTH_TIME);
        }
    }
}
