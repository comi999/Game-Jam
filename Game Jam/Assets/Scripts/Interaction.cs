using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction
{
    public Interaction( IInteractor a_Interactor, IInteractable a_Interactable )
    {
        m_Interactor = a_Interactor;
        m_Interactable = a_Interactable;
    }

    IInteractor m_Interactor;
    IInteractable m_Interactable;
}