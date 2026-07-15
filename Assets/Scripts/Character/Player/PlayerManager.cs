using KBCore.Refs;
using UnityEngine;

namespace ERL
{
    public class PlayerManager : CharacterManager
    {
        [SerializeField, Self] private PlayerLocomotionManager _playerLocomotionManager;

        private void OnValidate()
        {
            this.ValidateRefs();
        }

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Update()
        {
            base.Update();
            _playerLocomotionManager.HandleAllMovement();
        }
    }
}