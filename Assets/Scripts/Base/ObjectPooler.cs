using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Base
{
    public class ObjectPooler : MonoBehaviour
    {
        [SerializeField] private List<PoolObject> _pooledObjects;

        public List<PoolObject> PooledObjects => _pooledObjects;

        public bool TryGetObject<T>(out T pooledObject)
            where T : PoolObject
        {
            pooledObject = (T)_pooledObjects.FirstOrDefault(p => p.gameObject.activeInHierarchy == false);
            return pooledObject != null;
        }
    }
}