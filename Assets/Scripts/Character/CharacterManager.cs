using KBCore.Refs;
using Unity.Netcode;
using UnityEngine;

namespace ERL
{
    public class CharacterManager : NetworkBehaviour
    {
        [Self] public CharacterController characterController;
        [Self] public Animator animator;
        [Self] public CharacterNetworkManager characterNetworkManager;

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
                characterNetworkManager.networkPosition.Value = transform.position;
                characterNetworkManager.networkRotation.Value = transform.rotation;
            }
            else
            {
                transform.position = Vector3.SmoothDamp(transform.position,
                    characterNetworkManager.networkPosition.Value,
                    ref characterNetworkManager.networkPositionVelocity,
                    characterNetworkManager.networkPositionSmoothTime);

                transform.rotation = Quaternion.Slerp(transform.rotation,
                    characterNetworkManager.networkRotation.Value,
                    characterNetworkManager.networkRotationSmoothTime);
            }
        }

        protected virtual void LateUpdate()
        {
        }
    }
}