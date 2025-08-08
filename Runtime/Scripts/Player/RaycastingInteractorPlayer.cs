using System.Collections.Generic;
using NikosAssets.Interactions.Interactables;
using UnityEngine;

namespace NikosAssets.Interactions.Player
{
    public class RaycastingInteractorPlayer : BaseRaycastingPlayer
    {
        public List<BaseInteractable> HoveredInteractables { get; protected set; } = new List<BaseInteractable>();

        protected override void Unhover()
        {
            if (_wasHover)
            {
                for (int i = 0; i < HoveredInteractables.Count; i=0)
                {
                    HoveredInteractables[i].Unhover();
                    HoveredInteractables.RemoveAt(i);
                }
            }
            
            base.Unhover();
        }

        protected override void OnSuccessfulCastCallback(RaycastHit hit)
        {
            GameObject rayCastGameObject = hit.collider.gameObject;
            
            foreach (BaseInteractable interactable in rayCastGameObject.GetComponents<BaseInteractable>())
            {
                if (interactable == null) continue;

                interactable.Interact();
            }
        }
        
        protected override bool OnSuccessfulHoverCallback(RaycastHit hit)
        {
            GameObject rayCastGameObject = hit.collider.gameObject;
            Dictionary<BaseInteractable, bool> hoveredStateInteractablesDict = new Dictionary<BaseInteractable, bool>();
            
            List<BaseInteractable> interactables = new List<BaseInteractable>(rayCastGameObject.GetComponents<BaseInteractable>());
            
            //set old interactables to not hovered
            HoveredInteractables.ForEach(i => hoveredStateInteractablesDict[i] = false);
            HoveredInteractables = interactables.FindAll(i =>
            {
                if (!i.canInteract)
                    return false;

                //set new intractables to hovered (can overlap with old ones, so "update" false to true here if not new)
                hoveredStateInteractablesDict[i] = true;
                return true;
            });
            
            //finally set the correct hovered state
            foreach (var (baseInteractable, hovered) in hoveredStateInteractablesDict)
            {
                if (hovered)
                    baseInteractable.Hover();
                else 
                    baseInteractable.Unhover();
            }
            
            return HoveredInteractables.Count > 0;
        }
    }
}
