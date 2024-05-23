using PickUps;
using Player.PickUp;
using Interaction.Base;

namespace Interaction.Pickup
{
    public class PickUpInteraction : BaseInteraction
    {
        //--------------------Functions--------------------//
        private void OnEnable(){
			OnDrop.AddListener(PickUp);
			OnInteract.AddListener(PickUp);
		}

        private void OnDisable(){
			OnDrop.RemoveListener(PickUp);
			OnInteract.RemoveListener(PickUp);
		}

        private void PickUp()
        {
            PlayerMaster.CurrentActivePlayerModel.GetComponent<PlayerPickUp>()?.PickUpObject(GetComponent<PickUpComponent>());
        }
    }
}

