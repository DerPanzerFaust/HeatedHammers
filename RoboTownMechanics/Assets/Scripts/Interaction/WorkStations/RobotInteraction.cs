using Player.StateMachine;
using Utilities;

namespace Interaction.Workstations
{
    public class RobotInteraction : WorkstationInteraction
    {
        protected override void InteractionStart()
        {
            CurrentPickUpObjectType = PickUpObjectType.NONE;
            PlayerMaster.CurrentActivePlayerModel.GetComponent<PlayerStateMachine>().CurrentPlayerState = PlayerState.WALKING;
        }
    }
}