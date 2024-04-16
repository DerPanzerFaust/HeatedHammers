namespace Interaction.Workstations
{
    public class ForgeInteraction : WorkstationInteraction
    {
        //--------------------Functions--------------------//
        protected override void SpecialAction()
        {
            _quickTimeCanvas.SetActive(true);
            _quickHandler.SetInputEvents(PlayerMaster.PlayerInputComponent);
            _quickHandler.QuickTimeActive = true;
        }
    }
}