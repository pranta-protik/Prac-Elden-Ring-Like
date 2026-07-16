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
    }
}