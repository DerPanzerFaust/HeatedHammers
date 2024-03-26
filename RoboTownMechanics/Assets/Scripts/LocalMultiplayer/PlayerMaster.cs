using InputNameSpace;
using LocalMultiplayer.Lobby;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace LocalMultiplayer.Player
{
    public class PlayerMaster : MonoBehaviour
    {
        //--------------------Private--------------------//
        private Gamepad _currentGamepad;

        [SerializeField]
        private GameObject _playerPrefab;

        private InputComponent _playerInputComponent;

        private InputUser _playerInputUser;

        private bool _playerIsSpawned;

        private Color _playerColor;

        private LobbyJoinManager _lobbyJoinManager;

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

        public Color PlayerColor
        {
            get => _playerColor;
            set => _playerColor = value;
        }

        //--------------------Functions--------------------//
        private void Awake() => _playerInputComponent = GetComponent<InputComponent>();

        private void Start() => _lobbyJoinManager = LobbyJoinManager.Instance;

        private void Update()
        {
            //when starting game, spawn
            if (_currentGamepad.buttonSouth.IsPressed() && !_playerIsSpawned)
            {
                _playerIsSpawned = true;

                _lobbyJoinManager.JoinLobby(this);

                //GameObject instantiatedPrefab = Instantiate(_playerPrefab, new Vector3(0, _playerPrefab.transform.localScale.y, 0), Quaternion.identity);
                //instantiatedPrefab.GetComponent<PlayerData>().Master = this;
            }
        }
    }
}