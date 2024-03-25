using InputNameSpace;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class Rhythm : MonoBehaviour
{
    //--------------------Private--------------------//
    [SerializeField]
    private Image _rhythmCirkel;
    [SerializeField]
    private float _tempo;
    [SerializeField]
    private int _targetWidth;
    [SerializeField]
    private int _targetHeight;
    [SerializeField]
    private int _originalWidth;
    [SerializeField]
    private int _originalHeight;
    [SerializeField]
    private int _bufferWidth;
    [SerializeField]
    private int _bufferHeight;
    private InputComponent _inputComponent;

    //--------------------Public--------------------//
    public Action OnFailQuickTime;


    //--------------------Functions--------------------//
    private void Start()
    {
        _inputComponent = InputComponent.Instance;
        _inputComponent.Interact.performed += InteractPressed;

        OnFailQuickTime += FailedQuickTime;
    }

    private void Update()
    {
        MakeRhythm();
    }

    private void MakeRhythm()
    {
        _rhythmCirkel.rectTransform.sizeDelta -= new Vector2(_targetWidth, _targetHeight) * _tempo * Time.deltaTime;

        if (_rhythmCirkel.rectTransform.sizeDelta.magnitude <= new Vector2(_targetWidth, _targetHeight).magnitude)
        {
            Debug.Log("Too late!");
            OnFailQuickTime.Invoke();
        }
    }

    private void InteractPressed(InputAction.CallbackContext context)
    {
        if (_rhythmCirkel.rectTransform.sizeDelta.magnitude >= new Vector2(_targetHeight, _targetWidth).magnitude &&
            _rhythmCirkel.rectTransform.sizeDelta.magnitude <= new Vector2(_bufferHeight, _bufferWidth).magnitude)
        {
            _rhythmCirkel.rectTransform.sizeDelta = new Vector2(_originalWidth, _originalHeight);
            Debug.Log("On point!");
        }
        else if (_rhythmCirkel.rectTransform.sizeDelta.magnitude >= new Vector2(_bufferHeight, _bufferWidth).magnitude)
        {
            Debug.Log("Too early!");
        }
    }

    private void FailedQuickTime()
    { 
        //Hier komt de code voor wat er gebeurd als de speler de QTE faalt. Exit en wegschieten in ragdoll.
    }
}
