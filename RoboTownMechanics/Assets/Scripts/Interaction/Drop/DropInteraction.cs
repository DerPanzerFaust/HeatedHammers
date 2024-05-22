using PickUps;
using Player.Drop;
using Interaction.Base;


namespace Interaction.Drop
{
    public class DropInteraction : BaseInteraction
    {
        //--------------------Functions--------------------//
        private void Awake() => OnDrop.AddListener(Drop);

        private void OnDisable() => OnDrop.RemoveListener(Drop);

        private void Drop()
        {
            PlayerMaster.CurrentActivePlayerModel.GetComponent<PlayerDrop>()?.DropObject(GetComponent<PickUpComponent>());
        }
    }



}
