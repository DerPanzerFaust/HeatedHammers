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
        private InputComponent inputComponent;

        //--------------------Function--------------------//
        private void OnEnable()
        {
            inputComponent = LobbyJoinManager.Instance.Host?.CurrentPlayerMaster.PlayerInputComponent;


            inputComponent.OnInteractInputAction.performed += TryAgain;
            //inputComponent.OnSouthInputAction.performed += MainMenu;
        }

        private void OnDisable()
        {
            inputComponent.OnInteractInputAction.performed -= TryAgain;
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