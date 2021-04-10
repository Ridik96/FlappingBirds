using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    
    private Queue<GameObject> objectPool = new Queue<GameObject>();
    public GameObject objectSet;
    public Queue<GameObject> Pool
    {
        get { return objectPool; }
    }
    
    public void Init(GameObject prefab, int iCnt)
    {

        for (int i = 0; i < iCnt; ++i)
        {
            GameObject ojb = Instantiate(prefab);
            ojb.SetActive(false);
            objectPool.Enqueue(ojb);
        }
    }
    

    public void SetObject(Vector3 iPosition, Quaternion iRotation)
    {
       objectSet = objectPool.Dequeue();

        objectSet.transform.position = iPosition;
        objectSet.transform.rotation = iRotation;
        objectSet.SetActive(true);
        GameplayManager.Instance.terrein = objectSet;
        objectPool.Enqueue(objectSet);
    }
}