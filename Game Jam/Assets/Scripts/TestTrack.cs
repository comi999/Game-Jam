using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrack : MonoBehaviour, IInteractable
{
    public Renderer Renderer;
    public Material OnMaterial;
    public Material OffMaterial;
    public Material InteractMaterial;

    private void Awake()
    {
        Renderer = GetComponent< Renderer >();
    }

    public void OnHoverEnter(Interaction a_Interaction)
    {
        Renderer.material = OnMaterial;
    }

    public void OnHoverExit(Interaction a_Interaction)
    {
        Renderer.material = OffMaterial;
    }

    public void OnHoverStay(Interaction a_Interaction)
    {
        
    }

    public void OnInteract(Interaction a_Interaction)
    {
        
    }

    public void OnUninteract(Interaction a_Interaction)
    {
        
    }
}
