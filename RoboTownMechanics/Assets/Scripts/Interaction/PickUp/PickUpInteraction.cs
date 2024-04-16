using PickUps;
using Player.PickUp;
using Interaction.Base;

namespace Interaction.Pickup
{
    public class PickUpInteraction : BaseInteraction
    {
        //--------------------Functions--------------------//
        private void Awake() => OnInteract.AddListener(PickUp);

        private void OnDisable() => OnInteract.RemoveListener(PickUp);

        private void PickUp()
        {
            PlayerMaster.CurrentActivePlayerModel.GetComponent<PlayerPickUp>()?.PickUpObject(GetComponent<PickUpComponent>());
        }
    }
}

