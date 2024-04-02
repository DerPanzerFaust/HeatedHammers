using LocalMultiplayer.Player;
using UnityEngine.Events;
   public interface IInteraction
    {
        //--------------------Public--------------------//
        public UnityEvent onInteract { get; protected set; }

        //--------------------Functions--------------------//

        /// <summary>
        /// The interact function from the interface
        /// </summary>
        public void Interact(PlayerMaster playerMaster);
    }