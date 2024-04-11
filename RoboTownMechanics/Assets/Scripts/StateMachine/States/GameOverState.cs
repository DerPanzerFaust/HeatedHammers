using StateMachines.States;
using UnityEngine;
using MenuHandler;

public class GameOverState : State
{
    //--------------------Private--------------------//
    private MenuManager _menuManager;

    //--------------------Functions--------------------//
    protected override void OnEnter()
    {
        _menuManager = MenuManager.Instance;
        _menuManager.OpenMenu("GameOver");
        Debug.Log("GameOver!");
    }

    protected override void OnUpdate()
    {

    }

    protected override void OnExit()
    {

    }
}
