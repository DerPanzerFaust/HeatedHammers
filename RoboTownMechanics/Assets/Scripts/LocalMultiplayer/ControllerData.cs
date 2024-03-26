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
        private void Awake() => InvokeRepeating(nameof(CheckForConnectedController), 0f, _scanForControllerRate);

        private void CheckForConnectedController()
        {
            //if in lobby state
            foreach(var inputDevice in Gamepad.all)
            {
                if(!_currentGamepads.Contains(inputDevice) && _playerMasters.Count + 1 <= 4)
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
    }
}