using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class WorkstationInteraction : MonoBehaviour, IInteraction
{

    //--------------------Private--------------------//

    private bool _isOn;

    [SerializeField] 
    private UnityEvent _stopInteract;
    
    [SerializeField] 
    private UnityEvent _onInteract;

    //--------------------Functions--------------------//
    UnityEvent IInteraction.onInteract
    {
        get => _onInteract;
        set => _onInteract = value;
    }

    public void Interact()
    {
        if (_isOn)
        {
            _stopInteract.Invoke();
        }
        else
        {
            _onInteract.Invoke();
        }

        _isOn = !_isOn;
    }
}
