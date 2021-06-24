using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerController : ISingleton< PointerController >, IInteractor
{
    public enum MouseButton
    {
        LeftClick,
        RightClick
    }

    public MouseButton Interact;
    public LayerMask InteractableLayer;

    public Vector2 ScreenPosition { get; private set; }
    public Vector3 WorldPosition { get; private set; }
    public Vector3 RayCastPosition { get; private set; }
    public IInteractable HoveringOver { get; private set; }

    private void Update()
    {
        ScreenPosition = Input.mousePosition;
        WorldPosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        gameObject.transform.position = WorldPosition;

        Ray ray = Camera.main.ScreenPointToRay( ScreenPosition );
        IInteractable interactable = null;
        Interaction interaction = null;
        bool foundInteractable = Physics.Raycast( ray.origin, ray.direction, out RaycastHit hit, 10, InteractableLayer, QueryTriggerInteraction.Collide ) ? ( 
            hit.collider.gameObject.TryGetComponent( out interactable ) ? true : false ) : false;

        if ( foundInteractable )
        {
            if ( HoveringOver == null )
            {
                interaction = new Interaction( this, interactable );
                HoveringOver = interactable;
                HoveringOver.OnHoverEnter( interaction );
                OnHoverEnter( interaction );
            }

            if ( HoveringOver != interactable )
            {
                interaction = new Interaction( this, HoveringOver );
                HoveringOver.OnHoverExit( interaction );
                OnHoverExit( interaction );
                HoveringOver = null;
            }

            if ( HoveringOver == interaction )
            {
                interaction = new Interaction( this, interactable );
                interactable.OnHoverEnter( interaction );
                OnHoverStay( interaction );
            }

            RayCastPosition = hit.point;
        }
        else if ( HoveringOver != null )
        {
            interaction = new Interaction( this, HoveringOver );
            HoveringOver.OnHoverExit( interaction );
            OnHoverExit( interaction );
            HoveringOver = null;
        }

        interaction = new Interaction( this, interactable );

        if ( Input.GetMouseButtonDown( ( int )Interact ) )
        {
            if ( foundInteractable )
            {
                interactable.OnInteract( interaction );
            }

            OnInteract( interaction );
        }
        else if ( Input.GetMouseButtonUp( ( int )Interact ) )
        {
            if ( foundInteractable )
            {
                interactable.OnUninteract( interaction );
            }

            OnUninteract( interaction );
        }
    }

    public void OnInteract( Interaction a_Interaction )
    {
        
    }

    public void OnUninteract( Interaction a_Interaction )
    {
        
    }

    public void OnHoverEnter(Interaction a_Interaction)
    {
        
    }

    public void OnHoverExit(Interaction a_Interaction)
    {
        
    }

    public void OnHoverStay(Interaction a_Interaction)
    {
        
    }
}
