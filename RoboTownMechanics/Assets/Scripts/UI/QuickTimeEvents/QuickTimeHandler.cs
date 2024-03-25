using UnityEngine;

namespace QuickTime.Handler
{
    public class QuickTimeHandler : MonoBehaviour
    {
        [SerializeField]
        private GameObject _rhythmQuickTime;

        public void FailedQuickTime()
        {
            Debug.Log("Failed!");
        }
    }

}