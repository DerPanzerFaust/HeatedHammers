using InputNameSpace;
using LocalMultiplayer.Lobby;
using StateMachines.GlobalStateMachine;
using StateMachines.States;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace LocalMultiplayer.Player
{
    public class PlayerMaster : MonoBehaviour
    {
        //--------------------Private--------------------//
        private Gamepad _currentGamepad;

        private InputComponent _playerInputComponent;

        private InputUser _playerInputUser;

        private bool _hasJoinedLobby;

        private Material _playerMaterial;

        private LobbyJoinManager _lobbyJoinManager;

        private PlayerSpawner _playerSpawner;

        private GameObject _currentActivePlayerModel;

        private StateMachine _stateMachine;

        private LobbySpot _currentlobbySpot;

        //--------------------Public--------------------//
        public Gamepad CurrentGamepad
        {
            get => _currentGamepad;
            set => _currentGamepad = value;
        }

        public InputComponent PlayerInputComponent => _playerInputComponent;

        public InputUser PlayerInputUser
        {
            get => _playerInputUser;
            set => _playerInputUser = value;
        }

        public Material PlayerMaterial
        {
            get => _playerMaterial;
            set => _playerMaterial = value;
        }

        public GameObject CurrentActivePlayerModel
        {
            get => _currentActivePlayerModel;
            set => _currentActivePlayerModel = value;
        }

        public bool HasJoinedLobby
        {
            get => _hasJoinedLobby;
            set => _hasJoinedLobby = value;
        }

        public LobbySpot CurrentLobbySpot
        {
            get => _currentlobbySpot;
            set => _currentlobbySpot = value;
        }

        //--------------------Functions--------------------//
        private void Awake() => _playerInputComponent = GetComponent<InputComponent>();

        private void Start()
        {
            _lobbyJoinManager = LobbyJoinManager.Instance;
            _playerSpawner = PlayerSpawner.Instance;
            _stateMachine = StateMachine.Instance;

            _playerInputComponent.OnPickUpInputAction.performed += JoinLobby;
        }

        private void OnDisable() => _playerInputComponent.OnPickUpInputAction.performed -= JoinLobby;

        private void JoinLobby(InputAction.CallbackContext context)
        {
            if (_hasJoinedLobby)
                return;
            
            _hasJoinedLobby = true;

            if (_stateMachine.CurrentState.GetType() == typeof(GameState))
            {
                _lobbyJoinManager.JoinLobby(this);
                _playerSpawner.SpawnPlayer(this, _currentlobbySpot.SpawnPosition);
            }
            else
            {
                _lobbyJoinManager.JoinLobby(this);
            }
        }
        
        /// <summary>
        /// Destroys this master and its child player
        /// </summary>
        public void Destroy()
        {
            if(_currentActivePlayerModel != null)
                Destroy(_currentActivePlayerModel);
        
            Destroy(gameObject);
        }
    }
}