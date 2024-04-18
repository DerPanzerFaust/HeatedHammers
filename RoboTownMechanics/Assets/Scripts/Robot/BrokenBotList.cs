using System.Collections.Generic;
using UnityEngine;

namespace Robot.List
{
    //--------------------Private--------------------//
    public class BrokenBotList : MonoBehaviour
    {
        [SerializeField]
        private List<Part> _parts;
        [SerializeField]
        private List<Part> _brokenParts;

    //--------------------Public--------------------/
        public List<Part> Parts
        {
            get => _parts;
            set => _parts = value;
        }

        public List<Part> BrokenPartList
        {
            get => _brokenParts;
            set => _brokenParts = value;
        }
    }

    [System.Serializable]
    public class Part
    {
        //--------------------Private--------------------//
        [SerializeField]
        private string _partName;
        [SerializeField]
        private GameObject _wholePart;
        [SerializeField]
        private GameObject _brokenPart;
        [SerializeField]
        private bool _isBroken;

        //--------------------Public--------------------//
        public bool IsBroken
        {
            get => _isBroken;
            set => _isBroken = value;
        }

        public Part(string name, GameObject wholePart, GameObject brokenPart, bool isBroken)
        {
            _partName = name;
            _wholePart = wholePart;
            _brokenPart = brokenPart;
            _isBroken = isBroken;
        }
    }
}