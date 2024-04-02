using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This function stores the different types of objects as an enum
/// </summary>
public enum PoolObjectType
{
    Robot1,
    Robot2,
    Robot3
}
/// <summary>
/// This function contains the pool info, like the type, the amount, the prefabs and container
/// and the pool itself
/// </summary>
[Serializable]
public class PoolInfo
{
    public PoolObjectType type;
    public int amount = 0;
    public GameObject prefab;
    public GameObject container;

    [HideInInspector]
    public List<GameObject> pool = new List<GameObject> ();
}


namespace ObjectPooler 
{
    public class ObjectPooling : Singleton<ObjectPooling>
    {
        //--------------------Public--------------------//

        [SerializeField]
        List<PoolInfo> listOfPool;

        static public float robotTime;
        //--------------------Private--------------------//

        [SerializeField]
        private float _targetTime;

        private Vector3 _defaultPos = new Vector3 (-100, -100, -100);

        //--------------------Functions--------------------//

        private void Awake()
        {
            robotTime = _targetTime;
        }

        public void Start()
        {
 
            for (int i = 0; i < listOfPool.Count; i++)
            {
                FillPool(listOfPool[i]);
            }
        }

        void FillPool(PoolInfo info)
        {
            for (int i = 0;i< info.amount; i++)
            {
                GameObject obInstance = null;
                obInstance = Instantiate(info.prefab, info.container.transform);
                obInstance.gameObject.SetActive(false);
                obInstance.transform.position = _defaultPos;
                info.pool.Add(obInstance);
            }
        }

        /// <summary>
        /// Gets a pooled object when needed
        /// </summary>
        /// <returns>
        /// Returns "obInstance"
        /// </returns>
        public GameObject GetPoolObject(PoolObjectType type)
        {
            PoolInfo selected = GetPoolbyType(type);
            List<GameObject> pool = selected.pool;
            
            GameObject obInstance = null;
            if (pool.Count > 0)
            {
                obInstance = pool[pool.Count -1];
                pool.Remove(obInstance);
            } 
            else 
                obInstance = Instantiate(selected.prefab, selected.container.transform);

            return obInstance;
        }
        /// <summary>
        /// Sets the obj on the default position
        /// checks if the pool contains the object and if not than adds it
        /// </summary>
        public void CoolObject(GameObject ob, PoolObjectType type)
        {
            ob.SetActive(false);
            ob.transform.position = _defaultPos;

            PoolInfo selected = GetPoolbyType(type);
            List<GameObject> pool = selected.pool;

            if(!pool.Contains(ob))  
                pool.Add(ob);
        }

        private PoolInfo GetPoolbyType(PoolObjectType type)
            {
            for (int i = 0; i < listOfPool.Count; i++)
            {
                if (type == listOfPool[i].type)
                    return listOfPool[i];
            }

            return null;
        }
    }

}

