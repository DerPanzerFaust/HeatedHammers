using Bomb.ImageChanger;
using MenuHandler;
using TimerHandler;
using Robot.Spawner;

namespace StateMachines.States
{
    public class GameState : State
    {
        //--------------------Private--------------------//
        private GameTimer _timerManager;

        private BombImageChanger _bombImageChanger;

        private MenuManager _menuManager;

        private RobotSpawner _robotSpawner;

        //--------------------Functions--------------------//
        protected override void OnEnter()
        {
            _menuManager = MenuManager.Instance;
            _menuManager.OpenMenu("GUI");
            
            _timerManager = GameTimer.Instance;
            _timerManager.TimerStart();
            
            _bombImageChanger = BombImageChanger.Instance;
            _bombImageChanger.ChangeImage(_bombImageChanger.BombImageObjects[0]);

            _robotSpawner = RobotSpawner.Instance;
            _robotSpawner.SpawnRobot();
        }

        protected override void OnUpdate()
        {

        }

        protected override void OnExit()
        {
            _menuManager.CloseMenu("GUI");
        }
    }
}