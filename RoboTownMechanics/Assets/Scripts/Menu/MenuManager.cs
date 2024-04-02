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
        /// Closes all other Menu's and opens given menu 
        /// </summary>
        /// <param>name="aMenu">The menu that will open</param>
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
        /// Closes all other Menu's and opens the menu with the given string as name
        /// </summary>
        /// <param>name="menuName">Name for the menu that will open</param>
        public void OpenMenu(string menuName)
        {
            for (int i = 0; i < _menus.Length; i++)
            {
                if (_menus[i].Name == menuName)
                {
                    _menus[i].OpenMenu();
                }
                else if (_menus[i].Open)
                {
                    CloseMenu(_menus[i]);
                }
            }
        }

        /// <summary>
        /// Opens the given menu and doesnt close any other menu's
        /// </summary>
        /// <param>name="aMenu">The menu to open</param>
        public void OpenMenuNoClose(Menu aMenu)
        {
            aMenu.OpenMenu();
        }

        /// <summary>
        /// Closes the given menu
        /// </summary>
        /// <param>name="aMenu">Menu to close</param>
        public void CloseMenu(Menu aMenu)
        {
            aMenu.CloseMenu();
        }

        /// <summary>
        /// Closes the menu with the name given
        /// </summary>
        /// <param>name="menuName">The name of the menu to close</param>
        public void CloseMenu(string menuName)
        {
            for (int i = 0; i < _menus.Length; i++)
                if (_menus[i].Name == menuName)
                    _menus[i].CloseMenu();
        }

        /// <summary>
        /// Close the application
        /// </summary>
        public void QuitApplication()
        {
            Application.Quit();
        }
    }
}

