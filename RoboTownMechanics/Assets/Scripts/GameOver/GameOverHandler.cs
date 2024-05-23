using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using InputNameSpace;
using LocalMultiplayer.Lobby;
using StateMachines.GlobalStateMachine;

namespace GameOver.Handler
{
    public class GameOverHandler : MonoBehaviour
    {
        //--------------------Private--------------------//
        private InputComponent _inputComponent;

        //--------------------Function--------------------//
        private void OnEnable()
        {
            _inputComponent = LobbyJoinManager.Instance.Host?.CurrentPlayerMaster.PlayerInputComponent;


            _inputComponent.OnInteractInputAction.performed += TryAgain;
            //inputComponent.OnSouthInputAction.performed += MainMenu;
        }

        private void OnDisable()
        {
            _inputComponent.OnInteractInputAction.performed -= TryAgain;
            //inputComponent.OnSouthInputAction.performed -= MainMenu;
        }

        private void MainMenu(){
            //StateMachine.Instance.SetState(StateMachine.Instance.MainMenuStateInstance);
        }

        private void TryAgain(InputAction.CallbackContext context)
        {
            StateMachine.Instance.SetState(StateMachine.Instance.ResetStateInstance);
        }
    }
}