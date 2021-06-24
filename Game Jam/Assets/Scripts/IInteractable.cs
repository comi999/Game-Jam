using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void OnInteract( IInteractor a_Interactor );
    void OnUninteract( IInteractor a_Interactor );
}
