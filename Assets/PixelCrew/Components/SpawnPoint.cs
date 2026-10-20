using UnityEngine;

namespace PixelCrew.Components
{
    public class SpawnPoint : MonoBehaviour
    {
        private void Start()
        {
            var hero = FindObjectOfType<Hero>();
            if (hero != null)
            {
                hero.transform.position = transform.position;
            }
        }
    }
}