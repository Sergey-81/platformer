using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Transform _camera;
    private Vector3 _startPosition;

    [SerializeField] private float _parallaxEffect; // Сила эффекта (0 - стоит на месте, 1 - движется с камерой)

    private void Start()
    {
        _camera = Camera.main.transform;
        _startPosition = transform.position;
    }

    private void LateUpdate()
    {
        // Вычисляем, насколько камера сдвинулась от начальной точки
        Vector3 cameraDelta = _camera.position - _startPosition;

        // Двигаем объект с учетом коэффициента (чем меньше коэффициент, тем дальше объект кажется)
        transform.position = new Vector3(
            _startPosition.x + cameraDelta.x * _parallaxEffect,
            _startPosition.y + cameraDelta.y * _parallaxEffect,
            _startPosition.z
        );
    }
}