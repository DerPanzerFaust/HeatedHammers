using StateMachines.States;
using MenuHandler;
using PauseHandler;

public class GameOverState : State
{
    //--------------------Private--------------------//
    private MenuManager _menuManager;
    private PauseSystem _pauseSystem;

    //--------------------Functions--------------------//
    protected override void OnEnter()
    {
        _menuManager = MenuManager.Instance;
        _menuManager.OpenMenu("GameOver");

        _pauseSystem = PauseSystem.Instance;
        _pauseSystem.PausingGame();
        
    }

    protected override void OnUpdate()
    {
    }

    protected override void OnExit()
    {
        _menuManager.CloseMenu("GameOver");
    }
}
