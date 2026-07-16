using KBCore.Refs;

namespace ERL
{
    public class PlayerManager : CharacterManager
    {
        [Self] public PlayerLocomotionManager playerLocomotionManager;
        [Self] public PlayerAnimatorManager playerAnimatorManager;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Update()
        {
            base.Update();

            if (!IsOwner) return;
            playerLocomotionManager.HandleAllMovement();
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
            if (IsOwner)
            {
                PlayerCamera.Instance.playerManager = this;
                PlayerInputManager.Instance.playerManager = this;
            }
        }
    }
}