using UnityEngine;

public class FloorLogic : MonoBehaviour, IInteractable
{
    [SerializeField] ParticleSystem PS;  
    public void OnHoverEnter(Interaction a_Interaction)
    {
        
    }

    public void OnHoverExit(Interaction a_Interaction)
    {
        
    }

    public void OnHoverStay(Interaction a_Interaction)
    {
        
    }

    public void OnInteract(Interaction a_Interaction)
    {
        //do stuff
        if (a_Interaction.m_Interactor is PointerController pointer)
        {
            ParticleSystem ps = Instantiate(PS);

            ps.gameObject.transform.position = pointer.RayCastPosition;

            ps.Play();
        }
    }

    public void OnUninteract(Interaction a_Interaction)
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
