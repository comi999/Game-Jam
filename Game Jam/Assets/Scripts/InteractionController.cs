using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : ISingleton< InteractionController >
{
    public enum MouseButton
    {
        LeftClick,
        RightClick
    }

    public PointerController PointerController;
    public MouseButton Interact;
}