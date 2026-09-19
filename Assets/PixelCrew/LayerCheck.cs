
using UnityEngine;

public class LayerCheck : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    private Collider2D _collider;

    public bool IsTouchingLayer;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        Debug.Log($"LayerCheck Awake: collider = {_collider}, layer = {_groundLayer.value}");
    }

    private void FixedUpdate()
    {
        IsTouchingLayer = _collider.IsTouchingLayers(_groundLayer);

        if (IsTouchingLayer)
            Debug.Log("LayerCheck: TOUCHING ground!");
    }
}