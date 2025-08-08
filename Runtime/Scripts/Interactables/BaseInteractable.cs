using NaughtyAttributes;
using NikosAssets.Helpers;
using UnityEngine.Events;

namespace NikosAssets.Interactions.Interactables
{
    public abstract class BaseInteractable : BaseNotesMono
    {
        [BoxGroup(HelperConstants.ATTRIBUTE_FIELD_BOXGROUP_EVENTS)]
        public UnityEvent OnInteracted;
        [BoxGroup(HelperConstants.ATTRIBUTE_FIELD_BOXGROUP_EVENTS)]
        public UnityEvent OnHovered;
        [BoxGroup(HelperConstants.ATTRIBUTE_FIELD_BOXGROUP_EVENTS)]
        public UnityEvent OnUnhovered;

        [BoxGroup(HelperConstants.ATTRIBUTE_FIELD_BOXGROUP_SETTINGS)]
        public bool canInteract = true;

        protected bool _isHovered;

        protected virtual void OnDisable()
        {
            Unhover();
        }

        public virtual void Interact()
        {
            OnInteracted.Invoke();
        }

        public virtual void Hover()
        {
            if (_isHovered)
                return;

            _isHovered = true;
            OnHovered.Invoke();
        }

        public virtual void Unhover()
        {
            if (!_isHovered)
                return;

            _isHovered = false;
            OnUnhovered.Invoke();
        }
    }
}
