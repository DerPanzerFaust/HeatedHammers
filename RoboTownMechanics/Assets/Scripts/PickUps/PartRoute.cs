using System.Collections;
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

        
        public void CompleteStation()
        {
            if (_currentToBeCompletedWorkstation.Index + 1 > _route.Count)
                return;
            
            _currentToBeCompletedWorkstation = _route[_currentToBeCompletedWorkstation.Index + 1];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stationTypeToCheck"></param>
        /// <returns></returns>
        public bool IsCorrectStation(RouteObject stationTypeToCheck)
        {
            if(stationTypeToCheck.Station == _currentToBeCompletedWorkstation.Station)
                return true;
            else
                return false;
        }
    }

    [System.Serializable]
    public class RouteObject
    {
        //--------------------Private--------------------//
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