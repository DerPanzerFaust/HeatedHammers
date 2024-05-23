using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using LocalMultiplayer.Player;
using StateMachines.GlobalStateMachine;

namespace LocalMultiplayer.Lobby
{
    public class LobbyJoinManager : SingletonBehaviour<LobbyJoinManager>
    {
        //--------------------Private--------------------//
        [Header("Player1")]
        [SerializeField]
        private GameObject _player1Sprite;
        [SerializeField]
        private Material _player1Material;
        [SerializeField]
        private Transform _spawnPositionOne;
        
        [Header("Player2")]
        [SerializeField]
        private GameObject _player2Sprite;
        [SerializeField] 
        private Material _player2Material;
        [SerializeField]
        private Transform _spawnPositionTwo;

        [Header("Player3")]
        [SerializeField]
        private GameObject _player3Sprite;
        [SerializeField]
        private Material _player3Material;
        [SerializeField]
        private Transform _spawnPositionThree;

        [Header("Player4")]
        [SerializeField] 
        private GameObject _player4Sprite;
        [SerializeField]
        private Material _player4Material;
        [SerializeField]
        private Transform _spawnPositionFour;

        private StateMachine _stateMachine;

        [SerializeField]
        private List<LobbySpot> _lobbySpots = new();

        [SerializeField]
        private LobbySpot? _host;

        private PlayerSpawner _playerSpawner;

        private bool _lobbyHasStarted;

        private Material _lowestMaterial;

        private Transform _playerSpawnPosition;

        //--------------------Public--------------------//
        public LobbySpot? Host => _host;

        public bool LobbyHasStarted
        {
            get => _lobbyHasStarted;
            set => _lobbyHasStarted = value;
        }
        
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
            if (!CheckSpotFree())
                return;

            JoinLowestSpot(master);
        }

        private bool CheckSpotFree()
        {
            bool _spotIsFree;

            if(_lobbySpots.Count < 4) 
                _spotIsFree = true;
            else
                _spotIsFree = false;

            return _spotIsFree;
        }

        private void JoinLowestSpot(PlayerMaster playerMaster)
        {
            int lowestAvailableIndex = 5;

            if(_lobbySpots.Count == 0)
            {
                lowestAvailableIndex = 1;
            }
            else
            {
                int[] possibleIndexes = new int[4] {1, 2, 3, 4};

                foreach (LobbySpot spot in _lobbySpots)
                {
                    for (int i = 0; i < possibleIndexes.Length; i++)
                    {
                        if (possibleIndexes[i] == spot.SpotIndex)
                            possibleIndexes[i] = 0;
                    }
                }

                foreach (int i in possibleIndexes)
                {
                    if (i == 0)
                        continue;
                    else
                        lowestAvailableIndex = i;
                    break;
                }
            }
            
            switch (lowestAvailableIndex)
            {
                case 1:
                    _lowestMaterial = _player1Material;
                    _playerSpawnPosition = _spawnPositionOne;
                    _player1Sprite.SetActive(true);
                    break;
                case 2:
                    _lowestMaterial = _player2Material;
                    _playerSpawnPosition = _spawnPositionTwo;

                    _player2Sprite.SetActive(true);
                    break;
                case 3:
                    _lowestMaterial = _player3Material;
                    _playerSpawnPosition = _spawnPositionThree;

                    _player3Sprite.SetActive(true);
                    break;
                case 4:
                    _lowestMaterial = _player4Material;
                    _playerSpawnPosition = _spawnPositionFour;
                    _player4Sprite.SetActive(true);
                    break;
            }

            playerMaster.PlayerMaterial = _lowestMaterial;
            LobbySpot _playerLobbySpot = new LobbySpot(playerMaster, _lowestMaterial, lowestAvailableIndex, _playerSpawnPosition);

            if (lowestAvailableIndex == 1)
            {
                playerMaster.PlayerInputComponent.OnInteractInputAction.performed += StartGame;
                _host = _playerLobbySpot;
            }

            playerMaster.CurrentLobbySpot = _playerLobbySpot;
            _lobbySpots.Add(_playerLobbySpot);
        }

        /// <summary>
        /// A function to leave the lobby with the given master
        /// </summary>
        /// <param name="master">The master to leave the lobby with</param>
        public void LeaveLobby(PlayerMaster master)
        {
            LobbySpot masterSpot = new LobbySpot();
            int masterSpotIndex = 0;

            foreach (LobbySpot spot in _lobbySpots)
            {
                if (spot.CurrentPlayerMaster == master)
                {
                    masterSpot = spot;
                    masterSpotIndex = masterSpot.SpotIndex;
                }
            }

            master.HasJoinedLobby = false;

            switch (masterSpotIndex)
            {
                case 1:
                    _player1Sprite.SetActive(false);
                    break;
                case 2:
                    _player2Sprite.SetActive(false);
                    break;
                case 3:
                    _player3Sprite.SetActive(false);
                    break;
                case 4:
                    _player4Sprite.SetActive(false);
                    break;
            }

            if (masterSpotIndex == 1)
            {
                master.PlayerInputComponent.OnInteractInputAction.performed -= StartGame;
                _host = null;
            }

            _lobbySpots.Remove(masterSpot);
        }

        private void StartGame(InputAction.CallbackContext context)
        {
            if (_lobbyHasStarted)
                return;

            _playerSpawner.SpawnPlayers(_lobbySpots);

            _stateMachine.SetState(_stateMachine.GameStateInstance);
            _lobbyHasStarted = true;
        }

        /// <summary>
        /// Despawns all the player models in the lobby
        /// </summary>
        public void DeSpawnPlayers() => _playerSpawner.DeSpawnPlayers(_lobbySpots);
    }

    [Serializable]
    public struct LobbySpot
    {
        //--------------------Private--------------------//
        private PlayerMaster _currentPlayerMaster;
        [SerializeField]
        private Material _spotMaterial;
        [SerializeField]
        private int _spotIndex;
        [SerializeField]
        private Transform _spawnPosition;

        //--------------------Public--------------------//
        public PlayerMaster CurrentPlayerMaster => _currentPlayerMaster;
        
        public Material SpotMaterial => _spotMaterial;
        
        public int SpotIndex => _spotIndex;

        public Transform SpawnPosition => _spawnPosition;

        //--------------------Functions--------------------//
        public LobbySpot(PlayerMaster playerMaster, Material spotMaterial, int spotIndex, Transform spawnPosition)
        {
            _currentPlayerMaster = playerMaster;
            _spotMaterial = spotMaterial;
            _spotIndex = spotIndex;
            _spawnPosition = spawnPosition;
        }
    }
}