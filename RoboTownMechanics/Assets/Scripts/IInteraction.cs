using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IInteraction 
{
    //--------------------Public--------------------//
    public UnityEvent onInteract { get; protected set; }

    //--------------------Functions--------------------//
    public void Interact();
}
