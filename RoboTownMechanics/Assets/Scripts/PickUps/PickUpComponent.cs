using Robot.List;
using UnityEngine;
using Utilities;

namespace PickUps
{
    public class PickUpComponent : MonoBehaviour
    {
        //--------------------Private--------------------//
        [SerializeField]
        private PickUpState _currentPickUpState;

        [SerializeField]
        private PickUpObjectType _pickUpObjectType;

        [SerializeField]
        private Part _partData;
        
        //--------------------Public--------------------//
        public PickUpState CurrentPickUpState => _currentPickUpState;

        public PickUpObjectType PickUpObjectType => _pickUpObjectType;

        public Part Part
        {
            get => _partData;
            set => _partData = value;
        }
    }
}