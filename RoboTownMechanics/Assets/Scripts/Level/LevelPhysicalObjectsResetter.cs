using Objects.resetter;

namespace Level.Data
{
    public class LevelPhysicalObjectsResetter : SingletonBehaviour<LevelPhysicalObjectsResetter>
    {
        //--------------------Functions--------------------//
        /// <summary>
        /// Resets all the ObjectResetters in the Scene
        /// </summary>
        public void ResetLevel()
        {
            ObjectResetter[] _children = FindObjectsOfType<ObjectResetter>();

            foreach (ObjectResetter child in _children)
                StartCoroutine(child.ResetObject());
        }
    }
}