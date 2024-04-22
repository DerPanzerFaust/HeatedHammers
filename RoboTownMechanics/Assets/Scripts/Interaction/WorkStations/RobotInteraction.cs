using Player.StateMachine;
using Utilities;
using Robot.List;
using UnityEngine;
using Player.PickUp;
using PickUps;

namespace Interaction.Workstations
{
    public class RobotInteraction : WorkstationInteraction
    {
        private Part _brokenPart;
        [SerializeField]
        private BotPartList _brokenPartList;

        private PlayerPickUp _playerPickUp;

        protected override void InteractionStart()
        {
            CurrentPickUpObjectType = PickUpObjectType.NONE;
            PlayerMaster.CurrentActivePlayerModel.GetComponent<PlayerStateMachine>().CurrentPlayerState = PlayerState.WALKING;


            if (_brokenPartList.BrokenPartList.Count < 0)
                return;

            _playerPickUp = PlayerMaster.CurrentActivePlayerModel.GetComponent<PlayerPickUp>();
            
            if (_playerPickUp.CurrentPickedUpObject != null)
                return;
            
            _brokenPart = _brokenPartList.BrokenPartList[Random.Range(0, _brokenPartList.Parts.Count)];
            GameObject spawnedPart = Instantiate(_brokenPart.BrokenPart, transform.position, Quaternion.identity);
            _playerPickUp.PickUpObject(spawnedPart.GetComponent<PickUpComponent>());
        }
    }
}