using System.Collections.Generic;
using UnityEngine;

namespace LocalMultiplayer.Player
{
    public class PlayerSpawner : SingletonBehaviour<PlayerSpawner>
    {
        //--------------------Private--------------------//
        [SerializeField]
        private GameObject _playerPrefab;

        //--------------------Functions--------------------//
        /// <summary>
        /// A function to spawn a player for every master given in the list
        /// </summary>
        /// <param name="masters">The master list to spawn the players from</param>
        public void SpawnPlayers(List<PlayerMaster> masters)
        {
            foreach (var master in masters)
                SpawnPlayer(master);
        }

        /// <summary>
        /// A function to spawn a player from the given master
        /// </summary>
        /// <param name="master">The master to spawn a player from</param>
        public void SpawnPlayer(PlayerMaster master) 
        {
            GameObject instantiatedPrefab = Instantiate(_playerPrefab, new Vector3(0, _playerPrefab.transform.localScale.y, 0), Quaternion.identity);
            instantiatedPrefab.GetComponent<PlayerData>().Master = master;
            master.CurrentActivePlayerModel = instantiatedPrefab;

            instantiatedPrefab.GetComponent<Renderer>().material.color = master.PlayerColor;
        }
    }
}