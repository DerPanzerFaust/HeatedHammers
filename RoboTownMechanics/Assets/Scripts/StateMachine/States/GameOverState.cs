using StateMachines.States;
using UnityEngine;

public class GameOverState : State
{
    //--------------------Functions--------------------//
    protected override void OnEnter()
    {
        Debug.Log("GameOver!");
    }

    protected override void OnUpdate()
    {

    }

    protected override void OnExit()
    {

    }
}
