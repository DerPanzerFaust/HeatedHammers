using ObjectPooler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class RobotSpawner2 : MonoBehaviour
{
    public void Generate()
    {
        int random = Random.Range(0, 2);
        StartCoroutine(GenerateRoutine(random == 0? PoolObjectType.Robot1 : PoolObjectType.Robot2));

    }

    private IEnumerator GenerateRoutine(PoolObjectType type)
    {
        GameObject ob = ObjectPooling.Instance.GetPoolObject(type);

        ob.transform.position = new Vector3 (Random.Range(-1f,1f), 0.40f, Random.Range(-1f, 1f)); 
        ob.gameObject.SetActive(true);

        yield return new WaitForSeconds(10f);

        ObjectPooling.Instance.CoolObject(ob, type);
    }




}
