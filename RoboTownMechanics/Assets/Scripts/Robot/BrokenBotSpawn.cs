using Robot.List;
using Robot.Timer;
using UnityEngine;

namespace Robot.Spawn
{
    public class BrokenBotSpawn : MonoBehaviour
    {
        //--------------------Private--------------------//
        [SerializeField]
        private BrokenBotList _brokenBotList;
        [SerializeField]
        private int _brokenStartParts;
        [SerializeField]
        private BrokenPartSpawner _brokenPartSpawner;
        [SerializeField]
        private bool _botSpawned;
        private Part _brokenPart;

        //--------------------Functions--------------------//
        private void Update()
        {
            if (_botSpawned)
                _brokenPartSpawner.DoTimer();

            if (_botSpawned)
            {
                for (int i = 0; i < _brokenStartParts; i++)
                {
                    BreakPart();
                }
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
