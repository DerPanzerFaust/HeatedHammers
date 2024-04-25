using Robot.List;
using Robot.Spawner;
using Robot.Timer;
using UnityEngine;

namespace Robot.Spawn
{
    public class BotPartHandler : MonoBehaviour
    {
        //--------------------Private--------------------//
        private BotPartList _brokenBotList;

        private BotPartBreakTimer _brokenPartSpawner;

        private RobotSpawner _robotSpawner;

        [SerializeField]
        private int _brokenStartParts;

        [SerializeField]
        private bool _botSpawned;

        private Part _brokenPart;

        //--------------------Functions--------------------//
        private void Start()
        {
            _brokenBotList = GetComponent<BotPartList>();
            _brokenPartSpawner = GetComponent<BotPartBreakTimer>();

            _robotSpawner = RobotSpawner.Instance;

            _robotSpawner.OnEnteredShop += BreakParts;
        }

        private void OnDisable() => _robotSpawner.OnEnteredShop -= BreakParts;

        private void BreakParts()
        {
            _brokenPartSpawner.DoTimer();

            for (int i = 0; i < _brokenStartParts; i++)
            {
                BreakPart();
            }
        }

        /// <summary>
        /// This function gets a random part from the whole part list, Checks if the part isBroken and stows sat part in the broken parts list.
        /// </summary>
        public void BreakPart()
        {
            if (_brokenBotList.Parts.Count <= 0)
                return;

            _brokenPart = _brokenBotList.Parts[Random.Range(0, _brokenBotList.Parts.Count)];

            _brokenPart.IsBroken = true;
            _brokenBotList.BrokenPartList.Add(_brokenPart);
            _brokenBotList.Parts.Remove(_brokenPart);
        }
    }
}
