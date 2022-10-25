using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GameObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject iPoolablePrefab;
    [SerializeField]
    private Transform parentTransform;
    [SerializeField]
    private bool collectionCheck;
    [SerializeField]
    private int defaultCapacity = 20;

    private ObjectPool<GameObject> pool;

    // Start is called before the first frame update
    void Start()
    {
        // Validate poolablePrefab implements iPoolable
        if(iPoolablePrefab.GetComponent<iPoolable>() == null)
        {
            Debug.LogWarning("GameObject pool for " + iPoolablePrefab.name + " does not implement iPoolable");
            return;
        }

        // create object pool
        pool = new ObjectPool<GameObject>(
            () =>
            {
                // instantiate prefab
                GameObject prefab = Instantiate(iPoolablePrefab);
                prefab.GetComponent<iPoolable>().SetReturnToPoolAction(ReturnToPool);
                if (parentTransform != null) prefab.transform.parent = parentTransform;
                return prefab;

            },
            (gameObject) =>
            {
                gameObject.SetActive(true);
            },
            (gameObject) =>
            {
                gameObject.transform.position = Vector3.zero;
                gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
                gameObject.SetActive(false);
            },
            (gameObject) =>
            {
                Destroy(gameObject.gameObject);

            }, collectionCheck, defaultCapacity);
    }    

    private void ReturnToPool(GameObject poolableGameObject)
    {
        pool.Release(poolableGameObject);
    }

    public GameObject GetItem()
    {
        return pool.Get();
    }
}
