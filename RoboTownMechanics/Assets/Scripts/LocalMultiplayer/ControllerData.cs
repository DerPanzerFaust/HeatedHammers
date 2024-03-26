using LocalMultiplayer.Lobby;
using LocalMultiplayer.Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace LocalMultiplayer.Controller
{
    public class ControllerData : MonoBehaviour
    {
        //--------------------Private--------------------//
        private List<Gamepad> _currentGamepads = new();

        private List<GameObject> _playerMasters = new();
        [SerializeField]
        private GameObject _playerMasterPrefab;

        [SerializeField]
        private float _scanForControllerRate;

        //--------------------Functions--------------------//
        private void Awake()
        {
            InvokeRepeating(nameof(CheckForConnectedController), 0f, _scanForControllerRate);
            InputSystem.onDeviceChange += OnDisconnectedController;
        }

        private void OnDisable() => InputSystem.onDeviceChange -= OnDisconnectedController;

        private void CheckForConnectedController()
        {
            foreach(var inputDevice in Gamepad.all)
            {
                //if in lobby state
                if (!_currentGamepads.Contains(inputDevice) && _playerMasters.Count + 1 <= 4)
                {
                    _currentGamepads.Add(inputDevice);
                    
                    GameObject spawnedPrefab = Instantiate(_playerMasterPrefab, Vector3.zero, Quaternion.identity);

                    InputUser user = InputUser.CreateUserWithoutPairedDevices();
                    InputUser.PerformPairingWithDevice(inputDevice, user);
                    user.AssociateActionsWithUser(spawnedPrefab.GetComponent<PlayerMaster>().PlayerInputComponent.GameInput);

                    spawnedPrefab.GetComponent<PlayerMaster>().CurrentGamepad = inputDevice;
                    spawnedPrefab.GetComponent<PlayerMaster>().PlayerInputUser = user;

                    _playerMasters.Add(spawnedPrefab);
                }
            }
        }

        private void OnDisconnectedController(InputDevice device, InputDeviceChange change)
        {
            if (change != InputDeviceChange.Disconnected)
                return;

            for (int i = 0; i < _playerMasters.Count; i++)
            {
                PlayerMaster masterComponent = _playerMasters[i].GetComponent<PlayerMaster>();

                if (device == masterComponent.CurrentGamepad)
                {
                    //check if in lobby state, if in lobby state:
                    LobbyJoinManager.Instance.LeaveLobby(masterComponent);

                    masterComponent.PlayerInputUser.UnpairDevices();
                    masterComponent.PlayerInputUser.UnpairDevicesAndRemoveUser();

                    _playerMasters.Remove(_playerMasters[i]);
                    _currentGamepads.Remove((Gamepad)device);

                    masterComponent.Destroy();
                }
            }
        }
    }
}