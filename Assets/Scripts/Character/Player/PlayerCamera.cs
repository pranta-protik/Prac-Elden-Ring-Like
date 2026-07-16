using System;
using KBCore.Refs;
using UnityEngine;

namespace ERL
{
    public class PlayerCamera : DontDestroyOnLoadSingleton<PlayerCamera>
    {
        [Child] public Camera cameraObject;
        [SerializeField, Anywhere] private Transform _cameraPivotTransform;
        [HideInInspector] public PlayerManager playerManager;

        [Header("Camera Settings")] [SerializeField]
        private float _cameraSmoothTime = 0.1f;

        [SerializeField] private float _leftAndRightRotationSpeed = 220f;
        [SerializeField] private float _upAndDownRotationSpeed = 220f;
        [SerializeField] private float _minimumPivot = -30f;
        [SerializeField] private float _maximumPivot = 60f;
        [SerializeField] private float _cameraCollisionRadius = 0.2f;
        [SerializeField] private LayerMask _collideWithLayers;

        [Header("Camera Values")] private Vector3 _cameraVelocity;
        private Vector3 _cameraObjectPosition;
        [SerializeField] private float _leftAndRightLookAngle;
        [SerializeField] private float _upAndDownLookAngle;
        private float _cameraZPosition;
        private float _targetCameraZPosition;

        private void OnValidate()
        {
            this.ValidateRefs();
        }

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            _cameraZPosition = cameraObject.transform.localPosition.z;
        }

        public void HandleAllCameraActions()
        {
            if (playerManager == null) return;

            HandleFollowTarget();
            HandleRotations();
            HandleCollisions();
        }

        private void HandleFollowTarget()
        {
            var targetCameraPosition = Vector3.SmoothDamp(transform.position, playerManager.transform.position, ref _cameraVelocity, _cameraSmoothTime);
            transform.position = targetCameraPosition;
        }

        private void HandleRotations()
        {
            _leftAndRightLookAngle += PlayerInputManager.Instance.cameraHorizontalInput * _leftAndRightRotationSpeed * Time.deltaTime;
            _upAndDownLookAngle -= PlayerInputManager.Instance.cameraVerticalInput * _upAndDownRotationSpeed * Time.deltaTime;
            _upAndDownLookAngle = Mathf.Clamp(_upAndDownLookAngle, _minimumPivot, _maximumPivot);

            var cameraRotation = Vector3.zero;
            Quaternion targetRotation;

            cameraRotation.y = _leftAndRightLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            transform.rotation = targetRotation;

            cameraRotation = Vector3.zero;
            cameraRotation.x = _upAndDownLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            _cameraPivotTransform.localRotation = targetRotation;
        }

        private void HandleCollisions()
        {
            _targetCameraZPosition = _cameraZPosition;

            var direction = cameraObject.transform.position - _cameraPivotTransform.position;
            direction.Normalize();

            if (Physics.SphereCast(_cameraPivotTransform.position, _cameraCollisionRadius, direction, out var hit, Mathf.Abs(_targetCameraZPosition), _collideWithLayers))
            {
                var distanceFromHitObject = Vector3.Distance(_cameraPivotTransform.position, hit.point);
                _targetCameraZPosition = -(distanceFromHitObject - _cameraCollisionRadius);
            }

            if (Mathf.Abs(_targetCameraZPosition) < _cameraCollisionRadius)
            {
                _targetCameraZPosition = -_cameraCollisionRadius;
            }

            _cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, _targetCameraZPosition, 0.2f);
            cameraObject.transform.localPosition = _cameraObjectPosition;
        }
    }
}