using System;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components
{
    public class InteractableComponent : MonoBehaviour
    {
        [SerializeField] private InteractEvent _action;

        private Animator _animator;
        private static readonly int InteractKey = Animator.StringToHash("interact");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Interact(GameObject target)
        {
            if (_animator != null)
            {
                _animator.SetTrigger(InteractKey);
            }

            _action?.Invoke(target);
        }

        [Serializable]
        public class InteractEvent : UnityEvent<GameObject>
        {
        }
    }
}
