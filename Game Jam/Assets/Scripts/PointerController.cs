using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour, IInteractor
{

    public Vector2 ScreenPosition { get; private set; }
    public Vector3 WorldPosition { get; private set; }
    public IInteractable HoveringOver { get; private set; }

    private void Update()
    {
        ScreenPosition = Input.mousePosition;
        WorldPosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );

        Ray ray = Camera.main.ScreenPointToRay( ScreenPosition );
        Physics.Raycast( ray, out RaycastHit hit );
        bool foundInteractable = hit.collider.gameObject.TryGetComponent( out IInteractable interactable );

        Interaction interaction = null;

        if ( foundInteractable )
        {
            if ( HoveringOver != interactable )
            {
                interaction = new Interaction( this, HoveringOver );
                HoveringOver.OnHoverExit( interaction );
                OnHoverExit( interaction );
                HoveringOver = null;
            }

            if ( HoveringOver == null )
            {
                interaction = new Interaction( this, interactable );
                HoveringOver.OnHoverEnter( interaction );
                OnHoverEnter( interaction );
                HoveringOver = interactable;
            }

            if ( HoveringOver == interaction )
            {
                interaction = new Interaction( this, interactable );
                interactable.OnHoverEnter( interaction );
                OnHoverStay( interaction );
            }
        }
        else if ( HoveringOver != null )
        {
            interaction = new Interaction( this, HoveringOver );
            HoveringOver.OnHoverExit( interaction );
            OnHoverExit( interaction );
        }

        interaction = new Interaction( this, interactable );

        if ( Input.GetMouseButtonDown( ( int )InteractionController.Instance.Interact ) )
        {
            if ( foundInteractable )
            {
                interactable.OnInteract( interaction );
            }

            OnInteract( interaction );
        }
        else if ( Input.GetMouseButtonUp( ( int )InteractionController.Instance.Interact ) )
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
