using KBCore.Refs;
using UnityEngine;

namespace ERL
{
    public class CharacterAnimatorManager : ValidatedMonoBehaviour
    {
        private static readonly int _Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int _Vertical = Animator.StringToHash("Vertical");

        [SerializeField, Self] private CharacterManager _characterManager;

        private float _horizontal;
        private float _vertical;

        protected virtual void Awake()
        {
        }

        public void UpdateAnimatorMovementParameters(float horizontalValue, float verticalValue)
        {
            _characterManager.animator.SetFloat(_Horizontal, horizontalValue, 0.1f, Time.deltaTime);
            _characterManager.animator.SetFloat(_Vertical, verticalValue, 0.1f, Time.deltaTime);
        }
    }
}