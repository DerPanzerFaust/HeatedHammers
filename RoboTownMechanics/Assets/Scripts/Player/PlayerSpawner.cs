using LocalMultiplayer.Lobby;
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
        public void SpawnPlayers(List<LobbySpot> lobbySpots)
        {
            foreach (var lobbySpot in lobbySpots)
                SpawnPlayer(lobbySpot.CurrentPlayerMaster, lobbySpot.SpawnPosition);
        }

        /// <summary>
        /// A function to spawn a player from the given master
        /// </summary>
        /// <param name="master">The master to spawn a player from</param>
        public void SpawnPlayer(PlayerMaster master, Transform spawnPos) 
        {
            GameObject instantiatedPrefab = Instantiate(_playerPrefab, 
                new Vector3(spawnPos.position.x, _playerPrefab.transform.localScale.y, spawnPos.position.z), Quaternion.identity);

            instantiatedPrefab.GetComponent<PlayerData>().Master = master;
            master.CurrentActivePlayerModel = instantiatedPrefab;

            instantiatedPrefab.GetComponentInChildren<SkinnedMeshRenderer>().material = master.PlayerMaterial;
        }
    }
}