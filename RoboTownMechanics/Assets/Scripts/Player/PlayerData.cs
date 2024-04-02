using UnityEngine;

namespace LocalMultiplayer.Player
{
    public class PlayerData : MonoBehaviour
    {
        //--------------------Private--------------------//
        private PlayerMaster _master;

        //--------------------Public--------------------//
        public PlayerMaster Master
        {
            get => _master;
            set => _master = value;
        }
    }
}