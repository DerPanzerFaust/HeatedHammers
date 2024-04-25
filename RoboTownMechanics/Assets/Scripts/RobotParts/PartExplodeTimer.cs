using PickUps;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using Player.PickUp;
using Interaction.Pickup;
namespace PartsHandler
{
    public class PartExplodeTimer : MonoBehaviour
    {
        //--------------------Private--------------------//

        private PickUpComponent _currentPickedUpObject;

        private PlayerPickUp _playerPickUp;

        [SerializeField]
        private float _explodetimer;

        [SerializeField]
        private GameObject _destroyedPart;

        private Transform _position;

        private PickUpInteraction _pickUpInteraction;

        //--------------------Public--------------------//
        public PickUpComponent CurrentPickedUpObject
        {
            get => _currentPickedUpObject;
            set => _currentPickedUpObject = value;
        }

        public PlayerPickUp PlayerPickUp
        {
            get => _playerPickUp;
            set => _playerPickUp = value;
        }

        //--------------------Functions--------------------//
        private void Start()
        {
            _pickUpInteraction = GetComponent<PickUpInteraction>();

            _pickUpInteraction.OnInteract.AddListener(PickedUp);

        }

        private void OnDisable()
        {
            _pickUpInteraction.OnInteract.RemoveListener(PickedUp);
        }

        private void PickedUp()
        {
            PickUpInteraction pickUpInteraction = GetComponent<PickUpInteraction>();

            _playerPickUp = pickUpInteraction.PlayerMaster.CurrentActivePlayerModel.GetComponent<PlayerPickUp>();

            _position = pickUpInteraction.PlayerMaster.CurrentActivePlayerModel.GetComponent<Transform>();
        }

        private void Update()
        {
            _explodetimer -= Time.deltaTime;
            ExplosionTimer();
        }

        private void ExplosionTimer()
        {
            if (_explodetimer < 0)
            {
                ChangePart();
            }
        }

        private void ChangePart()
        {
            if (_playerPickUp != null && _playerPickUp.CurrentPickedUpObject != null)
            {
                GameObject spawnedPart = Instantiate(_destroyedPart, _position.transform.position, Quaternion.identity);

                _playerPickUp.DestroyObject();
                _playerPickUp.PickUpObject(spawnedPart.GetComponent<PickUpComponent>());
            }
            else if(_playerPickUp == null)
            {
                GameObject spawnedPart = Instantiate(_destroyedPart, gameObject.transform.position , Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}