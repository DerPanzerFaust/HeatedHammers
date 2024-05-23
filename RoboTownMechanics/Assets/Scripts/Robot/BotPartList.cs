using Interaction.Base;
using PickUps;
using Robot.Spawner;
using Robot.Timer;
using System.Collections.Generic;
using UnityEngine;

namespace Robot.List
{
    //--------------------Private--------------------//
    public class BotPartList : SingletonBehaviour<BotPartList>
    {
        //--------------------Private--------------------//
        [SerializeField]
        private List<Part> _parts = new();
        [SerializeField]
        private List<Part> _brokenParts = new();

        private RobotSpawner _robotSpawner;

        private BotPartBreakTimer _partBreakTimer;

        private bool _hasSpawned;

        //--------------------Public--------------------//
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

        //--------------------Functions--------------------//
        private void Start()
        {
            _robotSpawner = RobotSpawner.Instance;
            _partBreakTimer = GetComponent<BotPartBreakTimer>();

            _robotSpawner.OnEnteredShop += RobotSpawned;
        }

        private void OnDisable()
        {
            _robotSpawner.OnEnteredShop -= RobotSpawned;
        }

        public BaseInteraction GetPart(Transform requestingObject)
        {
            if(_brokenParts.Count == 0)
                return null;

            int randomPartInt = Random.Range(0, _brokenParts.Count);

            GameObject instantiatedObject = 
                Instantiate(_brokenParts[randomPartInt].BrokenPart, requestingObject.position + Vector3.down, requestingObject.rotation);

            instantiatedObject.GetComponent<PickUpComponent>().Part = _brokenParts[randomPartInt];

            _brokenParts[randomPartInt].IsPickedUp = true;

            return instantiatedObject.GetComponent<BaseInteraction>();
        }

        public void ReturnPartCompleted(Part returnedPart)
        {
            _parts.Add(returnedPart);

            foreach (Part part in _parts)
            {
                if (part.PartName == returnedPart.PartName)
                {
                    _brokenParts.Remove(part);
                    break;
                }
            }
        }

        public void ReturnPartDestroyed(Part returnedPart)
        {
            foreach(Part part in _parts)
            {
                if (part.PartName == returnedPart.PartName)
                {
                    part.IsPickedUp = false;
                    break;
                }
            }
        }

        private void RobotSpawned() => _hasSpawned = true;

        private void Update()
        {
            if(_hasSpawned && _brokenParts.Count <= 0)
            {
                //despawn succes
                _robotSpawner.DeSpawnRobotSucces();
                _hasSpawned = false;
                _partBreakTimer.IsRunning = false;
            }

            if( _hasSpawned && _parts.Count <= 0)
            {
                //despawn unsucces
                _robotSpawner.DeSpawnRobotUnSucces();
                _hasSpawned = false;
                _partBreakTimer.IsRunning = false;
            }
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
        private bool _isPickedUp = false;

        //--------------------Public--------------------//
        public string PartName => _partName;

        public bool IsPickedUp
        {
            get => _isPickedUp;
            set => _isPickedUp = value;
        }

        public GameObject BrokenPart => _brokenPart;

        public Part(string name, GameObject wholePart, GameObject brokenPart, bool isBroken)
        {
            _partName = name;
            _wholePart = wholePart;
            _brokenPart = brokenPart;
            _isPickedUp = isBroken;
        }
    }
}