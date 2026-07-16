using UnityEngine;
using UnityEngine.SceneManagement;

namespace ERL
{
    public class PlayerInputManager : DontDestroyOnLoadSingleton<PlayerInputManager>
    {
        [Header("Player Movement Input")] [SerializeField]
        private Vector2 _movementInput;

        [SerializeField] private bool _walkInput;
        public float horizontalInput;
        public float verticalInput;
        public float moveAmount;

        [Header("Camera Movement Input")] [SerializeField]
        private Vector2 _cameraInput;

        public float cameraHorizontalInput;
        public float cameraVerticalInput;

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
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
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
                _playerControls.PlayerCamera.Movement.performed += i => _cameraInput = i.ReadValue<Vector2>();
            }

            _playerControls.Enable();
        }

        private void Update()
        {
            HandleMovementInput();
            HandleCameraInput();
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

        private void HandleCameraInput()
        {
            cameraHorizontalInput = _cameraInput.x;
            cameraVerticalInput = _cameraInput.y;
        }
    }
}