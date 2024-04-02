using LocalMultiplayer.Player;
using StateMachines.GlobalStateMachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LocalMultiplayer.Lobby
{
    public class LobbyJoinManager : SingletonBehaviour<LobbyJoinManager>
    {
        //--------------------Private--------------------//
        [Header("UpperLeft")]
        [SerializeField]
        private GameObject _upperLeft;
        [SerializeField]
        private Color _upperLeftColor;
        
        [Header("UpperRight")]
        [SerializeField]
        private GameObject _upperRight;
        [SerializeField] 
        private Color _upperRightColor;
        
        [Header("LowerLeft")]
        [SerializeField]
        private GameObject _lowerLeft;
        [SerializeField]
        private Color _lowerLeftColor;
        
        [Header("LowerRight")]
        [SerializeField] 
        private GameObject _lowerRight;
        [SerializeField]
        private Color _lowerRightColor;

        private StateMachine _stateMachine;

        private List<PlayerMaster> _joinedMasters = new();

        private PlayerSpawner _playerSpawner;

        private bool _lobbyHasStarted;

        //--------------------Functions--------------------//
        private void Start()
        {
            _playerSpawner = PlayerSpawner.Instance;
            _stateMachine = StateMachine.Instance;
        }

        /// <summary>
        /// A function to join the localmultiplayer lobby with your playermaster
        /// </summary>
        /// <param name="master">The master to join into the lobby</param>
        public void JoinLobby(PlayerMaster master)
        {
            _joinedMasters.Add(master);

            int index = _joinedMasters.IndexOf(master);

            if(index == 0)
            {
                master.PlayerInputComponent.OnInteractInputAction.performed += StartGame;
            }

            switch (index)
            {
                case 0:
                    master.PlayerColor = _upperLeftColor;
                    _upperLeft.SetActive(true);
                    break;
                case 1:
                    master.PlayerColor = _upperRightColor;
                    _upperRight.SetActive(true);
                    break;
                case 2:
                    master.PlayerColor = _lowerLeftColor;
                    _lowerLeft.SetActive(true);
                    break;
                case 3: 
                    master.PlayerColor = _lowerRightColor;
                    _lowerRight.SetActive(true);
                    break;
            }
        }

        /// <summary>
        /// A function to leave the lobby with the given master
        /// </summary>
        /// <param name="master">The master to leave the lobby with</param>
        public void LeaveLobby(PlayerMaster master)
        {
            int index = _joinedMasters.IndexOf(master);

            master.HasJoinedLobby = false;

            if ((_joinedMasters.Count - 1) == 0)
            {
                master.PlayerInputComponent.OnInteractInputAction.performed -= StartGame;
            }

            switch (index)
            {
                case 0:
                    _upperLeft.SetActive(false);
                    break;
                case 1:
                    _upperRight.SetActive(false);
                    break;
                case 2:
                    _lowerLeft.SetActive(false);
                    break;
                case 3:
                    _lowerRight.SetActive(false);
                    break;
            }

            _joinedMasters.Remove(master);
        }

        private void StartGame(InputAction.CallbackContext context)
        {
            if (_lobbyHasStarted)
                return;

            _playerSpawner.SpawnPlayers(_joinedMasters);

            _stateMachine.SetState(_stateMachine.GameStateInstance);
            _lobbyHasStarted = true;
        }
    }
}