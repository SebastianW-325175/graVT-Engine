using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class World2D : MonoBehaviour
    {
        private List<TinyObject> objects = new List<TinyObject>();

        public void RegisterObject(TinyObject tinyObject)
        {
            Debug.Log("Registering object: " + tinyObject.name);
        }
        //TODO: Add an option to unregister an object

        private void OnEnable()
        {
            objects = GetComponentsInChildren<TinyObject>(false).ToList();
            foreach (TinyObject tinyObject in objects)
            {
                tinyObject.world2D = this;
            }
        }
    }
}