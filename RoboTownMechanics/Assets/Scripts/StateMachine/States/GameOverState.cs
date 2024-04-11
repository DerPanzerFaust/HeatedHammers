using StateMachines.States;
using UnityEngine;
using MenuHandler;
using TimerHandler;

public class GameOverState : State
{
    //--------------------Private--------------------//
    private MenuManager _menuManager;
    private InternalTimer _timer;

    //--------------------Functions--------------------//
    protected override void OnEnter()
    {
        _menuManager = MenuManager.Instance;
        _menuManager.OpenMenu("GameOver");

        _timer = InternalTimer.Instance;
        _timer.PausingGame();
        
    }

    protected override void OnUpdate()
    {

    }

    protected override void OnExit()
    {
        
    }
}
