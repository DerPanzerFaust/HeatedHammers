using Interaction.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualKeys : MonoBehaviour
{
    [SerializeField]
    private GameObject _buttonX;

    Interactor _interactor;

    private void Start()
    {
        _interactor =  GetComponent<Interactor>();
    }

    private void Update()
    {
        BaseInteraction interactable = _interactor.GetTopPriorityInteractionObject();

        if (interactable != null)
        {
            _buttonX.SetActive(true);
        }
        else _buttonX.SetActive(false);
    }
}
