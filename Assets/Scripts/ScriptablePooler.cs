using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptablePooler", fileName = "ScriptablePooler")]
public class ScriptablePooler : ScriptableObject
{
    public GameObject prefab;
    [SerializeField] protected int _maxSize=10;
    [SerializeField]  protected Queue<GameObject> pooledObjectQueue = new Queue<GameObject>();

    public virtual GameObject TakeFromPool()
    {
        GameObject obj;
        if (pooledObjectQueue.Count > 0)
        {
            obj = pooledObjectQueue.Dequeue();
            obj.gameObject.SetActive(true);
        }
        else
        {
            obj = Instantiate(prefab);
        }

        return obj;
    }

    public void PutBackToPool(GameObject t)
    {
        if (pooledObjectQueue.Count > _maxSize)
        {
            Destroy(t.gameObject);
        }
        else
        {
            t.gameObject.SetActive(false);
            pooledObjectQueue.Enqueue(t);
            Debug.Log(pooledObjectQueue.Count);
        }
    }

}
