using Robot.List;
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

        private bool _botSpawned;
        private Part _brokenPart;

        //--------------------Functions--------------------//
        private void Update()
        {
            if (Input.GetKeyDown("space"))
            {
                for (int i = 0; i < _brokenStartParts; i++)
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
    }
}
