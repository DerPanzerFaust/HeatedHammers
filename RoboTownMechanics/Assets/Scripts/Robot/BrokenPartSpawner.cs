using Robot.List;
using Robot.Spawn;
using UnityEditorInternal;
using UnityEngine;

namespace Robot.Timer
{
    public class BrokenPartSpawner : MonoBehaviour
    {
        [SerializeField]
        private float _initialTime;
        [SerializeField]
        private float _currentTime;
        [SerializeField]
        private BrokenBotList _brokenBotList;
        [SerializeField]
        private BrokenBotSpawn _brokenBotSpawn;
        private Part _brokenPart;

        private void Start()
        {
            _currentTime = _initialTime;
        }
        public void DoTimer()
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime <= 0f)
            {
                _brokenBotSpawn.BreakPart();
                ResetTime();
            }
        }
        private void ResetTime() => _currentTime = _initialTime;
    }
}
