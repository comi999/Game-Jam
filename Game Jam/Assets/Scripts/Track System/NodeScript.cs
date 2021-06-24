using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour, IInteractable
{
    public NodeScript next;
    public NodeScript previous;

    public NodeScript swapable;
    [Tooltip("Should the next node be swaped, or the previous node?")]
    public bool swapNext = true;
    

    public Material lineMat;

    private LineRenderer line;
    private LineRenderer swapableLine;



    void Start()
    {
        // Create temp line renderers
        if (next != null)
        {
            line = gameObject.AddComponent<LineRenderer>();
            line.SetPosition(0, transform.position - new Vector3(0,0,0.001f));
            line.SetPosition(1, next.transform.position - new Vector3(0,0,0.001f));
            line.material = lineMat;
        }

        if (swapable != null)
        {
            GameObject obj = new GameObject("swapableLine");
            obj.transform.parent = transform;
            obj.transform.localPosition = Vector3.zero;

            swapableLine = obj.AddComponent<LineRenderer>();
            swapableLine.SetPosition(0, transform.position - new Vector3(0, 0, 0.001f));
            swapableLine.SetPosition(1, swapable.transform.position - new Vector3(0, 0, 0.001f));
            swapableLine.material = lineMat;
            swapableLine.startColor = Color.red;
            swapableLine.endColor = Color.red;
        }
    }

    void OnMouseDown()
    {
        SwapTrack();
    }

    public void SwapTrack()
    {
        if (swapable == null)
            return;

        if (swapNext)
        {
            NodeScript temp = next;
            next = swapable;
            swapable = temp;
        }
        else
        {
            NodeScript temp = previous;
            previous = swapable;
            swapable = temp;
        }

        // Update line renderers
        line.SetPosition(1, next.transform.position - new Vector3(0, 0, 0.001f));
        swapableLine.SetPosition(1, swapable.transform.position - new Vector3(0, 0, 0.001f));
    }

    public void OnHoverEnter(Interaction a_Interaction)
    {
        //throw new System.NotImplementedException();
    }

    public void OnHoverStay(Interaction a_Interaction)
    {
        //throw new System.NotImplementedException();
    }

    public void OnHoverExit(Interaction a_Interaction)
    {
        //throw new System.NotImplementedException();
    }

    public void OnInteract(Interaction a_Interaction)
    {
        //SwapTrack();
    }

    public void OnUninteract(Interaction a_Interaction)
    {
        //throw new System.NotImplementedException();
    }
}
