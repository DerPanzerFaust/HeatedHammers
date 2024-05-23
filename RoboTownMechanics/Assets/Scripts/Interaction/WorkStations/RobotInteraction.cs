using PickUps;
using Player.StateMachine;
using Robot.List;
using Utilities;
using UnityEngine;

namespace Interaction.Workstations
{
    public class RobotInteraction : WorkstationInteraction
    {
        //--------------------Private--------------------//
        private BotPartList _botPartList;

        //--------------------Functions--------------------//
        private void Start() => _botPartList = GetComponent<BotPartList>();

        protected override void InteractionStart()
        {
            _botPartList.ReturnPartCompleted(PickUpGameObjectReference.GetComponent<PickUpComponent>().Part);

            CurrentPickUpObjectType = PickUpObjectType.NONE;
            PlayerMaster.CurrentActivePlayerModel.GetComponent<PlayerStateMachine>().CurrentPlayerState = PlayerState.WALKING;
        }
    }
}