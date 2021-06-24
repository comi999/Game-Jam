using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void OnHoverEnter( Interaction a_Interaction );
    void OnHoverStay( Interaction a_Interaction );
    void OnHoverExit( Interaction a_Interaction );
    void OnInteract( Interaction a_Interaction );
    void OnUninteract( Interaction a_Interaction );
}
