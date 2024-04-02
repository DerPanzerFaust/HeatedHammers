using ObjectPooler;
using System.Collections;
using UnityEngine;

public class RobotSpawner : MonoBehaviour
{
    //-------------------Private------------------//
    private GameObject _platform;
    private float _time;


    private void Start()
    {
        _platform = GameObject.FindGameObjectWithTag("Platform");
        _time = ObjectPooling.robotTime;
    }
    /// <summary>
    /// simple click function to generate a robot
    /// </summary>
    public void Click()
    {
        Debug.Log(_time);
        int random = Random.Range(0,2);
        StartCoroutine(GenerateRoutine(random == 0? PoolObjectType.Robot1 : PoolObjectType.Robot2));
    }

    private IEnumerator GenerateRoutine(PoolObjectType type)
    {
        GameObject ob = ObjectPooling.Instance.GetPoolObject(type);
        ob.transform.position = new Vector3(_platform.transform.position.x, _platform.transform.position.y + 0.40f, _platform.transform.position.z);
        ob.gameObject.SetActive(true);

        yield return new WaitForSeconds(_time);

        ObjectPooling.Instance.CoolObject(ob, type);
    }
}
