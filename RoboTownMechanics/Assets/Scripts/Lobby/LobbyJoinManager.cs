using LocalMultiplayer.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LocalMultiplayer.Lobby
{
    public class LobbyJoinManager : SingletonBehaviour<LobbyJoinManager>
    {
        //--------------------Private--------------------//
        [Header("UpperLeft")]
        [SerializeField]
        private GameObject _upperLeft;
        [SerializeField]
        private Color _upperLeftColor;
        
        [Header("UpperRight")]
        [SerializeField]
        private GameObject _upperRight;
        [SerializeField] 
        private Color _upperRightColor;
        
        [Header("LowerLeft")]
        [SerializeField]
        private GameObject _lowerLeft;
        [SerializeField]
        private Color _lowerLeftColor;
        
        [Header("LowerRight")]
        [SerializeField] 
        private GameObject _lowerRight;
        [SerializeField]
        private Color _lowerRightColor;

        private List<PlayerMaster> _joinedMasters = new();

        //--------------------Functions--------------------//

        public void JoinLobby(PlayerMaster master)
        {
            _joinedMasters.Add(master);

            int index = _joinedMasters.IndexOf(master);

            if(index == 0)
            {

            }

            switch (index)
            {
                case 0:
                    master.PlayerColor = _upperLeftColor;
                    break;
                case 1:
                    master.PlayerColor = _upperRightColor;
                    break;
                case 2:
                    master.PlayerColor = _lowerLeftColor;
                    break;
                case 3: 
                    master.PlayerColor = _lowerRightColor;
                    break;
            }
        }

        public void StartGame()
        {

        }
    }
}