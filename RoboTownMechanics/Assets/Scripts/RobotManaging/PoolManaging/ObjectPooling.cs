using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PoolObjectType
{
    Robot1,
    Robot2
}

[Serializable]
public class PoolInfo
{
    public PoolObjectType type;
    public int amount = 0;
    public GameObject prefab;
    public GameObject container;

    [HideInInspector]
    public List<GameObject> pool = new List<GameObject>();
}

namespace ObjectPooler 
{
    public class ObjectPooling : Singleton<ObjectPooling> 
    {
        [SerializeField]
        List<PoolInfo> listOfPools;

        private Vector3 _defPos = new Vector3(-10,-10,-10);

        private void Start()
        {
            for (int i = 0; i < listOfPools.Count; i++)
                FillPool(listOfPools[i]);

            
        }

        void FillPool(PoolInfo info)
        {
            for(int i = 0; i < info.amount; i++)
            {
                GameObject obInstance = null;
                obInstance = Instantiate(info.prefab, info.container.transform);
                obInstance.gameObject.SetActive(false);
                obInstance.transform.position = _defPos;
                info.pool.Add(obInstance);
            }
        }


        public GameObject GetPooledObject(PoolObjectType type)
        {
            PoolInfo selected = GetPoolByType(type);
            List<GameObject> pool = selected.pool;

            GameObject obInst = null;
            if (pool.Count > 0)
            {
                obInst = pool[pool.Count - 1];
                pool.Remove(obInst);
            }
            else 
                obInst = Instantiate(selected.prefab, selected.container.transform);

            return obInst;
        }

        public void CoolObject(GameObject ob, PoolObjectType type)
        {
            ob.SetActive(false);
            ob.transform.position = _defPos;

            PoolInfo selected = GetPoolByType(type);
            List<GameObject> pool = selected.pool;

            if (!pool.Contains(ob))
                pool.Add(ob);
        }    

        private PoolInfo GetPoolByType (PoolObjectType type)
        {
            for (int i = 0 ; i < listOfPools.Count;i++)
                if (type == listOfPools[i].type)
                    return listOfPools[i];

            return null;
        }
    }

}

