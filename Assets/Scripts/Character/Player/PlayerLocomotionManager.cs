using KBCore.Refs;
using UnityEngine;

namespace ERL
{
    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        [SerializeField, Self] private PlayerManager _playerManager;
        [SerializeField] private float _walkingSpeed = 2f;
        [SerializeField] private float _runningSpeed = 5f;
        [SerializeField] private float _rotationSpeed = 15f;

        public float horizontalMovement;
        public float verticalMovement;
        public float moveAmount;

        private Vector3 _moveDirection;
        private Vector3 _rotationDirection;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Update()
        {
            base.Update();

            if (_playerManager.IsOwner)
            {
                _playerManager.characterNetworkManager.networkHorizontalMovement.Value = horizontalMovement;
                _playerManager.characterNetworkManager.networkVerticalMovement.Value = verticalMovement;
                _playerManager.characterNetworkManager.networkMoveAmount.Value = moveAmount;
            }
            else
            {
                horizontalMovement = _playerManager.characterNetworkManager.networkHorizontalMovement.Value;
                verticalMovement = _playerManager.characterNetworkManager.networkVerticalMovement.Value;
                moveAmount = _playerManager.characterNetworkManager.networkMoveAmount.Value;

                _playerManager.playerAnimatorManager.UpdateAnimatorMovementParameters(0f, moveAmount);
            }
        }

        public void HandleAllMovement()
        {
            HandleGroundedMovement();
            HandleRotation();
        }

        private void GetMovementValues()
        {
            horizontalMovement = PlayerInputManager.Instance.horizontalInput;
            verticalMovement = PlayerInputManager.Instance.verticalInput;
            moveAmount = PlayerInputManager.Instance.moveAmount;
        }

        private void HandleGroundedMovement()
        {
            GetMovementValues();

            _moveDirection = PlayerCamera.Instance.transform.forward * verticalMovement + PlayerCamera.Instance.transform.right * horizontalMovement;
            _moveDirection.Normalize();
            _moveDirection.y = 0f;

            if (PlayerInputManager.Instance.moveAmount > 0.5f)
            {
                _playerManager.characterController.Move(_moveDirection * (_runningSpeed * Time.deltaTime));
            }
            else if (PlayerInputManager.Instance.moveAmount <= 0.5f)
            {
                _playerManager.characterController.Move(_moveDirection * (_walkingSpeed * Time.deltaTime));
            }
        }

        private void HandleRotation()
        {
            _rotationDirection = Vector3.zero;
            _rotationDirection = PlayerCamera.Instance.cameraObject.transform.forward * verticalMovement + PlayerCamera.Instance.cameraObject.transform.right * horizontalMovement;
            _rotationDirection.Normalize();
            _rotationDirection.y = 0f;

            if (_rotationDirection == Vector3.zero)
            {
                _rotationDirection = transform.forward;
            }

            var newRotation = Quaternion.LookRotation(_rotationDirection);
            var targetRotation = Quaternion.Slerp(transform.rotation, newRotation, _rotationSpeed * Time.deltaTime);
            transform.rotation = targetRotation;
        }
    }
}