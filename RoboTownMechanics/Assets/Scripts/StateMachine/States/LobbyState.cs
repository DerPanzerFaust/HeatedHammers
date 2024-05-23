using MenuHandler;
using PostProcessing.Profiles;
using UnityEngine;
using UnityEngine.Rendering;

namespace StateMachines.States
{
    public class LobbyState : State
    {
        //--------------------Private--------------------//
        private MenuManager _menuManager;
        private VolumeProfileLibrary _profileLibrary;
        private Volume _currentSceneVolume;
        //--------------------Functions--------------------//

        protected override void OnEnter()
        {
            _menuManager = MenuManager.Instance;
            _profileLibrary = VolumeProfileLibrary.Instance;

            _menuManager.OpenMenu("Lobby");

            _currentSceneVolume = GameObject.FindObjectOfType<Volume>();
            _currentSceneVolume.profile = _profileLibrary.VolumeProfiles[1];
        }

        protected override void OnUpdate()
        {

        }

        protected override void OnExit()
        {
            _menuManager.CloseMenu("Lobby");
            _currentSceneVolume.profile = _profileLibrary.VolumeProfiles[0];
        }
    }
}