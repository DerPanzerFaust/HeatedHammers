using MenuHandler;

namespace StateMachines.States
{
    public class LobbyState : State
    {
        //--------------------Private--------------------//
        private MenuManager _menuManager;

        //--------------------Functions--------------------//
        protected override void OnEnter()
        {
            _menuManager = MenuManager.Instance;
            _menuManager.OpenMenu("Lobby");
        }

        protected override void OnUpdate()
        {

        }

        protected override void OnExit()
        {
            _menuManager.CloseMenu("Lobby");
        }
    }
}