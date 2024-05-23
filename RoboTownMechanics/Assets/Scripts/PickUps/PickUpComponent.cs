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
        
        //--------------------Public--------------------//
        public PickUpState CurrentPickUpState => _currentPickUpState;

        public PickUpObjectType PickUpObjectType => _pickUpObjectType;
    }
}