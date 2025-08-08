using System.Collections.Generic;
using NaughtyAttributes;
using NikosAssets.Helpers;
using UnityEngine;

namespace NikosAssets.Interactions.Interactables
{
    /// <summary>
    /// In case the desired <see cref="BaseInteractable"/>s don't contain any colliders, use this to link Mono to them
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class InteractableGroup : BaseInteractable
    {
        [BoxGroup(HelperConstants.ATTRIBUTE_FIELD_BOXGROUP_SETTINGS)]
        public List<BaseInteractable> directLinks = new();

        public override void Interact()
        {
            foreach (BaseInteractable interactable in directLinks)
            {
                if (interactable == null)
                    continue;
                
                interactable.Interact();
            }

            base.Interact();
        }
    }
}
