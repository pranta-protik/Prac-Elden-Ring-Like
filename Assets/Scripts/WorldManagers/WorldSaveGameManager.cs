using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ERL
{
    public class WorldSaveGameManager : DontDestroyOnLoadSingleton<WorldSaveGameManager>
    {
        [SerializeField] private int _worldSceneIndex = 1;

        public IEnumerator LoadNewGame()
        {
            var loadOperation = SceneManager.LoadSceneAsync(_worldSceneIndex);
            yield return null;
        }

        public int GetWorldSceneIndex() => _worldSceneIndex;
    }
}