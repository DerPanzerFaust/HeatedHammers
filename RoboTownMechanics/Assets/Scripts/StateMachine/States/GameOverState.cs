using StateMachines.States;
using MenuHandler;
using PauseHandler;
using PostProcessing.Profiles;
using UnityEngine.Rendering;
using UnityEngine;

public class GameOverState : State
{
    //--------------------Private--------------------//
    private MenuManager _menuManager;
    private PauseSystem _pauseSystem;

    private VolumeProfileLibrary _profileLibrary;
    private Volume _currentSceneVolume;

    //--------------------Functions--------------------//
    protected override void OnEnter()
    {
        _menuManager = MenuManager.Instance;
        _menuManager.OpenMenu("GameOver");

        _profileLibrary = VolumeProfileLibrary.Instance;

        _currentSceneVolume = GameObject.FindObjectOfType<Volume>();
        _currentSceneVolume.profile = _profileLibrary.VolumeProfiles[1];

        _pauseSystem = PauseSystem.Instance;
        _pauseSystem.PausingGame();
    }

    protected override void OnUpdate()
    {
    }

    protected override void OnExit()
    {
        _menuManager.CloseMenu("GameOver");

        _currentSceneVolume.profile = _profileLibrary.VolumeProfiles[0];
    }
}
