using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew.Components
{
    public class MainMenu : MonoBehaviour
    {
        public void StartGame()
        {
            // Загружаем первую игровую сцену (SampleScene)
            SceneManager.LoadScene("SampleScene");
        }

        public void OpenOptions()
        {
            // Пока просто выведем сообщение в консоль
            Debug.Log("Настройки пока не готовы!");
        }

        public void ExitGame()
        {
            // Закрываем игру (работает только в собранном билде)
            Debug.Log("Выход из игры!");
            Application.Quit();
        }
    }
}