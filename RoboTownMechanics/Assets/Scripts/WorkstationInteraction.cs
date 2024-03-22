using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorkstationInteraction : MonoBehaviour, IInteraction
{

    //--------------------Private--------------------//

    [SerializeField]private UnityEvent _onInteract;

    //--------------------Functions--------------------//
    UnityEvent IInteraction.onInteract
    {
        get => _onInteract;
        set => _onInteract = value;
    }

    public void Interact() => _onInteract.Invoke();
}
