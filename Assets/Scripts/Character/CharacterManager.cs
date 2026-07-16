using KBCore.Refs;
using Unity.Netcode;
using UnityEngine;

namespace ERL
{
    public class CharacterManager : NetworkBehaviour
    {
        [Self] public CharacterController characterController;
        [SerializeField, Self] private CharacterNetworkManager _characterNetworkManager;

        private void OnValidate()
        {
            this.ValidateRefs();
        }

        protected virtual void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        protected virtual void Update()
        {
            if (IsOwner)
            {
                _characterNetworkManager.networkPosition.Value = transform.position;
                _characterNetworkManager.networkRotation.Value = transform.rotation;
            }
            else
            {
                transform.position = Vector3.SmoothDamp(transform.position,
                    _characterNetworkManager.networkPosition.Value,
                    ref _characterNetworkManager.networkPositionVelocity,
                    _characterNetworkManager.networkPositionSmoothTime);

                transform.rotation = Quaternion.Slerp(transform.rotation,
                    _characterNetworkManager.networkRotation.Value,
                    _characterNetworkManager.networkRotationSmoothTime);
            }
        }

        protected virtual void LateUpdate()
        {
        }
    }
}