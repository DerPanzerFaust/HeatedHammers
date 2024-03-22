using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IInteraction 
{
    public UnityEvent onInteract { get; protected set; }
    public void Interact();
}
