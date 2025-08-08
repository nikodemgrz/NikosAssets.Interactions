using System.Collections.Generic;
using NaughtyAttributes;
using NikosAssets.Helpers;
using NikosAssets.PauseHandler;
using UnityEngine;
using UnityEngine.Events;

namespace NikosAssets.Interactions.Player
{
    public abstract class BaseRaycastingPlayer : BaseNotesMono
    {
        public UnityEvent OnHover;
        public UnityEvent OnUnhover;
        public UnityEvent OnInteractAttempt;

        private const string RAYCAST = "Raycast ";
        
        [SerializeField]
        [BoxGroup(RAYCAST + HelperConstants.ATTRIBUTE_FIELD_BOXGROUP_SETTINGS)]
        protected Transform _posTarget;

        [SerializeField]
        [BoxGroup(RAYCAST + HelperConstants.ATTRIBUTE_FIELD_BOXGROUP_SETTINGS)]
        protected Transform _casterTarget;
        
        [SerializeField]
        [BoxGroup(RAYCAST + HelperConstants.ATTRIBUTE_FIELD_BOXGROUP_SETTINGS)]
        protected float _maxInteractDistance = 4;

        [SerializeField]
        [BoxGroup(RAYCAST + HelperConstants.ATTRIBUTE_FIELD_BOXGROUP_SETTINGS)]
        protected int _maxColliders = 1;

        [SerializeField]
        [BoxGroup(RAYCAST + HelperConstants.ATTRIBUTE_FIELD_BOXGROUP_SETTINGS)]
        protected LayerMask _layerMask;
        
        [BoxGroup(RAYCAST + HelperConstants.ATTRIBUTE_FIELD_BOXGROUP_SETTINGS)]
        public bool blockInteraction = false;

        [SerializeField]
        [BoxGroup(RAYCAST + HelperConstants.ATTRIBUTE_FIELD_BOXGROUP_SETTINGS)]
        [InfoBox("You dont need to assign this, if you use the PauseManagerProvider singleton (auto fetch)")]
        protected BasePauseManager _pauseManager;

        public BasePauseManager PauseManager
        {
            get
            {
                if (_pauseManager != null) 
                    return _pauseManager;
                
                PauseManagerProvider provider = PauseManagerProvider.Instance;
                if (provider != null)
                    _pauseManager = provider.GetPauseManager;

                return _pauseManager;
            }
            set => _pauseManager = value;
        }

        protected bool _wasHover;
        
        public virtual List<RaycastHit> GetHits()
        {
            RaycastHit[] raycastHits = new RaycastHit[5];

            if (Physics.RaycastNonAlloc(_posTarget.position,
                _casterTarget.forward,
                raycastHits,
                _maxInteractDistance,
                _layerMask) > 0)
            {
                List<RaycastHit> hits = new List<RaycastHit>(raycastHits);
                hits.Sort((x, y) =>
                {
                    if (x.collider == null) return -1;
                    if (y.collider == null) return 1;
                    Vector3 position = _casterTarget.position;

                    float distSqCompA = NumericHelper.DistanceSquared(x.point, position);
                    float distSqCompB = NumericHelper.DistanceSquared(y.point, position);

                    if (distSqCompA > distSqCompB)
                        return 1;

                    if (distSqCompA < distSqCompB)
                        return -1;

                    return 0;
                });

                return hits;
            }

            return new List<RaycastHit>();
        }

        public virtual void HandleHover()
        {
            bool shouldHover = false;
            
            int hit = 0;
            foreach (RaycastHit raycastHit in GetHits())
            {
                if (raycastHit.collider == null) 
                    continue;
                
                if (++hit > _maxColliders) 
                    break;
                
                if ((shouldHover = OnSuccessfulHoverCallback(raycastHit)) == true) 
                    break;
            }

            ToggleHover(shouldHover);
        }

        public virtual void ToggleHover(bool shouldHover)
        {
            if (shouldHover)
                Hover();
            else
                Unhover();
        }

        public virtual void Cast()
        {
            int hit = 0;
            foreach (RaycastHit raycastHit in GetHits())
            {
                if (raycastHit.collider == null) 
                    continue;
                
                if (++hit > _maxColliders) 
                    break;
                    
                OnSuccessfulCastCallback(raycastHit);
            }
        }
        
        public virtual void Interact()
        {
            if (blockInteraction)
                return;

            if (PauseManager.Paused)
                return;

            Cast();
            
            OnInteractAttempt.Invoke();
        }

        protected virtual void Hover()
        {
            if (_wasHover) 
                return;
                
            _wasHover = true;
            
            OnHover.Invoke();
        }

        protected virtual void Unhover()
        {
            if (!_wasHover) 
                return;
                
            _wasHover = false;
            
            OnUnhover.Invoke();
        }
        
        protected abstract void OnSuccessfulCastCallback(RaycastHit hit);
        protected abstract bool OnSuccessfulHoverCallback(RaycastHit hit);
    }
}