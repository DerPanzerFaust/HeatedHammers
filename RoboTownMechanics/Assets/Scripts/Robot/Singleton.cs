using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    //--------------------Private--------------------//
    [SerializeField]
    private bool _dontDestory = false;

    static T m_instance;
    //--------------------Functions--------------------//

    /// <summary>
    /// a function which finds GameObject T if said GameObject cannot be found it will create it instead
    /// </summary>
    /// <returns>Returns the instance </returns>
    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = GameObject.FindAnyObjectByType<T>();

                if (m_instance != null)
                {
                    GameObject singleton = new GameObject(typeof(T).Name);
                    m_instance = singleton.AddComponent<T>();
                }
            }
            return m_instance;
        }
    }

    /// <summary>
    /// a function which checks if "_dontDestroy is true than then parent will be removed and prevents from being destroyed
    /// </summary>
    public virtual void Awake ()
    {
        if(m_instance == null) 
        {
            m_instance = this as T;
            if(_dontDestory)
            {
                transform.parent = null;
                DontDestroyOnLoad(this.gameObject);
            }    
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
