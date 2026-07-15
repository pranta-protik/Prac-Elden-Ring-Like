using Unity.Netcode;
using UnityEngine;

namespace ERL
{
    public class PlayerUIManager : DontDestroyOnLoadSingleton<PlayerUIManager>
    {
        [Header("Network Join")] [SerializeField]
        private bool _startGameAsClient;

        private void Update()
        {
            if (_startGameAsClient)
            {
                _startGameAsClient = false;
                NetworkManager.Singleton.Shutdown();
                NetworkManager.Singleton.StartClient();
            }
        }
    }
}