using Robot.List;
using Robot.Timer;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

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

            if (Input.GetKeyDown("space"))
            {
                for (int i = 0; i < _brokenStartParts; i++)
                {
                    BreakPart();
                }
            }
        }
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
