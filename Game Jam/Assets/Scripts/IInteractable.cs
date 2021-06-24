using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void OnInteract( Interaction a_Interaction );
    void OnUninteract( Interaction a_Interaction );
}
