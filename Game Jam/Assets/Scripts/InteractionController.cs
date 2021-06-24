using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : ISingleton< InteractionController >
{
    private void Awake()
    {
        m_MainCamera = Camera.main;
    }

    private void Update()
    {
        Ray ray = m_MainCamera.ScreenPointToRay( Input.mousePosition );
        Physics.Raycast( ray, out RaycastHit hit );

        if ( hit.collider.gameObject.TryGetComponent( out IInteractable interactable ) )
        {
            interactable.OnInteract( new Interaction( default, interactable ) );
        }
    }

    private Camera m_MainCamera;
}
