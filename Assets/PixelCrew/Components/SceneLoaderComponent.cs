using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew.Components
{
    public class SceneLoaderComponent : MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        public void LoadScene()
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}