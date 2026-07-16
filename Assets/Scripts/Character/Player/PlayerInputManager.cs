using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ERL
{
    public class PlayerInputManager : DontDestroyOnLoadSingleton<PlayerInputManager>
    {
        [SerializeField] private Vector2 _movementInput;
        [SerializeField] private bool _walkInput;
        public float horizontalInput;
        public float verticalInput;
        public float moveAmount;

        private PlayerControls _playerControls;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            SceneManager.activeSceneChanged += OnSceneChanged;
            Instance.enabled = false;
        }

        private void OnSceneChanged(Scene oldScene, Scene newScene)
        {
            if (newScene.buildIndex == WorldSaveGameManager.Instance.GetWorldSceneIndex())
            {
                Instance.enabled = true;
            }
            else
            {
                Instance.enabled = false;
            }
        }

        private void OnEnable()
        {
            if (_playerControls == null)
            {
                _playerControls = new PlayerControls();
                _playerControls.PlayerMovement.Movement.performed += i => _movementInput = i.ReadValue<Vector2>();
                _playerControls.PlayerActions.Walk.performed += i => _walkInput = true;
                _playerControls.PlayerActions.Walk.canceled += i => _walkInput = false;
            }

            _playerControls.Enable();
        }

        private void Update()
        {
            HandleMovementInput();
        }

        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= OnSceneChanged;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (enabled)
            {
                if (hasFocus)
                {
                    _playerControls.Enable();
                }
                else
                {
                    _playerControls.Disable();
                }
            }
        }

        private void HandleMovementInput()
        {
            horizontalInput = _movementInput.x;
            verticalInput = _movementInput.y;

            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

            if (moveAmount > 0 && _walkInput)
            {
                moveAmount = 0.5f;
            }
            else if (moveAmount > 0 && !_walkInput)
            {
                moveAmount = 1f;
            }
        }
    }
}