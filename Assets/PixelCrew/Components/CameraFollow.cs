using UnityEngine;
using Cinemachine;

namespace PixelCrew.Components
{
    public class CameraFollow : MonoBehaviour
    {
        private CinemachineVirtualCamera _vcam;

        private void Awake()
        {
            _vcam = GetComponent<CinemachineVirtualCamera>();
        }

        private void Start()
        {
            // Находим Hero на сцене (того, который пришёл)
            var hero = FindObjectOfType<Hero>();

            if (hero != null)
            {
                // Привязываем камеру к нему
                _vcam.Follow = hero.transform;
                _vcam.LookAt = hero.transform;
            }
        }
    }
}