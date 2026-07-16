using KBCore.Refs;
using UnityEngine;

namespace ERL
{
    public class PlayerManager : CharacterManager
    {
        [SerializeField, Self] private PlayerLocomotionManager _playerLocomotionManager;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Update()
        {
            base.Update();

            if (!IsOwner) return;
            _playerLocomotionManager.HandleAllMovement();
        }

        protected override void LateUpdate()
        {
            if (!IsOwner) return;
            base.LateUpdate();

            PlayerCamera.Instance.HandleAllCameraActions();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (IsOwner) PlayerCamera.Instance.playerManager = this;
        }
    }
}