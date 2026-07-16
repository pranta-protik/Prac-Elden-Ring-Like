using KBCore.Refs;
using UnityEngine;

namespace ERL
{
    public class PlayerCamera : DontDestroyOnLoadSingleton<PlayerCamera>
    {
        [Child] public Camera cameraObject;

        private void OnValidate()
        {
            this.ValidateRefs();
        }
    }
}