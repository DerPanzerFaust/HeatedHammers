using TimerHandler;
using StateMachines.GlobalStateMachine;
using PauseHandler;
using Level.Data;
using LocalMultiplayer.Lobby;

namespace StateMachines.States
{
    public class ResetState : State
    {
        //--------------------Private--------------------//
        private GameTimer _gameTimer;
        private StateMachine _stateMachine;
        private PauseSystem _pauseSystem;
        private LevelPhysicalObjectsResetter _physicalObjectsData;
        private LobbyJoinManager _lobbyJoinManager;

        //--------------------Functions--------------------//
        protected override void OnEnter()
        {
            _gameTimer = GameTimer.Instance;
            _stateMachine = StateMachine.Instance;
            _pauseSystem = PauseSystem.Instance;
            _physicalObjectsData = LevelPhysicalObjectsResetter.Instance;
            _lobbyJoinManager = LobbyJoinManager.Instance;

            //reset timer/unpause
            _gameTimer.SetTimer();
            _pauseSystem.PausingGame();

            _stateMachine.SetState(_stateMachine.LobbyStateInstance);
        }

        protected override void OnUpdate()
        {
        }

        protected override void OnExit()
        {
            _lobbyJoinManager.DeSpawnPlayers();
            _lobbyJoinManager.LobbyHasStarted = false;
            _physicalObjectsData.ResetLevel();
        }
    }
}