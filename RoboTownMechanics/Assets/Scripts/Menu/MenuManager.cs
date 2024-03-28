using UnityEngine;

namespace MenuHandler
{
    public class MenuManager : SingletonBehaviour<MenuManager>
    {
        //-------------------Private-------------------//
        [SerializeField]
        private Menu[] _menus;

        //-------------------Functions-------------------//

        /// <summary>
        /// Sluit alle menus die open staan in de lijst.
        /// </summary>
        /// <param>name="aMenu"> Menu wat je opent .</param>
        public void OpenMenu(Menu aMenu)
        {
            for (int i = 0; i < _menus.Length; i++)
            {
                if (_menus[i].Open)
                {
                    CloseMenu(_menus[i]);
                }
            }
            aMenu.OpenMenu();
        }

        /// <summary>
        /// Opent menu wat niet had mogen sluiten.
        /// </summary>
        /// <param>name="aMenu"> Menu wat je opent, wat niet mocht sluiten .</param>
        public void OpenMenuNoClose(Menu aMenu)
        {
            aMenu.OpenMenu();
        }

        /// <summary>
        /// Sluit het huidige menu.
        /// </summary>
        /// <param>name="aMenu"> Menu wat je sluit .</param>
        public void CloseMenu(Menu aMenu)
        {
            aMenu.CloseMenu();
        }

        /// <summary>
        /// Sluit de applicatie
        /// </summary>
        public void QuitApplication()
        {
            Application.Quit();
        }
    }
}

