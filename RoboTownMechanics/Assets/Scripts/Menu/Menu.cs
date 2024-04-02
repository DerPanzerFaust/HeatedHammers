using UnityEngine;

namespace MenuHandler
{
    public class Menu : MonoBehaviour
    {
        //-------------------Private-------------------//
        [SerializeField]
        private string _name;
        [SerializeField]
        private bool _open;
        
        //-------------------Public-------------------//
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public bool Open
        {
            get => _open;
            set => _open = value;
        }
        
        //-------------------Functions-------------------//
        /// <summary>
        /// Sets this Menu's gameObject to active and sets it Open
        /// </summary>
        public void OpenMenu()
        {
            Open = true;
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Deactivates this Menu's gameObject to inactive
        /// </summary>
        public void CloseMenu()
        {
            Open = false;
            gameObject.SetActive(false);
        }

    }
}
