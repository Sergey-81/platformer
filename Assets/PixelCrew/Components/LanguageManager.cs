using UnityEngine;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // При запуске переводим всё на русский
        TranslateAllToRussian();
    }

    public void TranslateAllToRussian()
    {
        // Находим ВСЕ текстовые компоненты на сцене (включая выключенные)
        Text[] allTexts = Resources.FindObjectsOfTypeAll<Text>();

        foreach (Text textObj in allTexts)
        {
            // Проверяем, что объект реально на сцене (а не в префабе)
            if (textObj.gameObject.scene.name != null)
            {
                // Переводим по английскому тексту, который там написан
                if (textObj.text == "Start") textObj.text = "Начать";
                else if (textObj.text == "Options") textObj.text = "Настройки";
                else if (textObj.text == "Exit") textObj.text = "Выход";
                else if (textObj.text == "HP: 5") textObj.text = "Здоровье: 5"; // Для твоего HUD
            }
        }
    }
}