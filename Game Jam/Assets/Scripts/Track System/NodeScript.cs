using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
{
    public NodeScript next;
    public NodeScript previous;

    [HideInInspector]
    public NodeScript swapable;
    [HideInInspector]
    public bool swapNext = true;
    


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
    }
}
