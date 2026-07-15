using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ERL
{
    public class PlayerInputManager : DontDestroyOnLoadSingleton<PlayerInputManager>
    {
        [SerializeField] private Vector2 _movementInput;

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
                _playerControls.PlayerMovement.Movement.performed += ctx => _movementInput = ctx.ReadValue<Vector2>();
            }

            _playerControls.Enable();
        }

        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= OnSceneChanged;
        }
    }
}