using System.Collections.Generic;
using UnityEngine;
using Utilities;


namespace PartUtilities.Route
{
    public class PartRoute : MonoBehaviour
    {
        //--------------------Private--------------------//
        [SerializeField]
        private List<RouteObject> _route = new();

        [SerializeField]
        private RouteObject _currentToBeCompletedWorkstation;
        //--------------------Public--------------------//
        public RouteObject CurrentToBeCompletedWorkstation => _currentToBeCompletedWorkstation;

        //--------------------Functions--------------------//

        private void Awake()
        {
            _currentToBeCompletedWorkstation = _route[0];

            for (int i = 0; i < _route.Count; i++)
            {
                _route[i].Index = i + 1;
            }
        }

        /// <summary>
        /// checks if you can complete current station and moves on to the next
        /// </summary>
        /// <returns>bool if route can move on to the next station or not</returns>
        public bool CanCompleteStation()
        {
            if (_currentToBeCompletedWorkstation.Index + 1 > _route.Count)
                return false;
            
            _currentToBeCompletedWorkstation = _route[_currentToBeCompletedWorkstation.Index];
            return true;
        }

        /// <summary>
        /// Checks if the given station is the correct station needed to be completed
        /// </summary>
        /// <param name="stationTypeToCheck">station to check</param>
        /// <returns>bool</returns>
        public bool IsCorrectStation(WorkStation stationTypeToCheck)
        {
            if(stationTypeToCheck == _currentToBeCompletedWorkstation.Station)
                return true;
            else
                return false;
        }
    }

    [System.Serializable]
    public class RouteObject
    {
        //--------------------Private--------------------//
        [SerializeField]
        private int _index;

        [SerializeField]
        private WorkStation _station;

        //--------------------Public--------------------//
        public int Index
        {
            get => _index;
            set => _index = value;
        }

        public WorkStation Station => _station;

        //--------------------Functions--------------------//
        RouteObject(int index, WorkStation station)
        {
            _index = index;
            _station = station;
        }
    }
}