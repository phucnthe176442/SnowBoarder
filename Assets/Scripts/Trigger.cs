using System.Collections.Generic;
using UnityEngine;

namespace snow_boarder
{
    public class Trigger : MonoBehaviour
    {
        public event System.Action<GameObject> OnTriggerEnterEvent;
        public event System.Action<GameObject, Trigger> OnTriggerEnterEventT;
        public event System.Action<GameObject> OnTriggerStayEvent;
        public event System.Action<GameObject> OnTriggerExitEvent;
        public GameObject parent;

        private void OnTriggerEnter2D(UnityEngine.Collider2D other)
        {
            OnTriggerEnterEvent?.Invoke(other.gameObject);
            OnTriggerEnterEventT?.Invoke(other.gameObject, this);
        }

        private void OnTriggerStay2D(UnityEngine.Collider2D other) { OnTriggerStayEvent?.Invoke(other.gameObject); }

        private void OnTriggerExit2D(UnityEngine.Collider2D other) { OnTriggerExitEvent?.Invoke(other.gameObject); }

        public List<GameObject> GetObjectsAround(float radius)
        {
            var colliders = Physics.OverlapSphere(this.transform.position, radius);
            List<GameObject> listObject = new List<GameObject>();
            foreach (var col in colliders)
            {
                if (col.gameObject != this.gameObject && col.gameObject.activeInHierarchy)
                    listObject.Add(col.gameObject);
            }

            return listObject;
        }

        public List<GameObject> GetObjectNear()
        {
            if (this.GetComponent<Rigidbody>() != null)
            {
                var thisCollider = this.GetComponent<BoxCollider>();
                var colliders = Physics.OverlapBox(thisCollider.bounds.center, this.GetComponent<Collider>().bounds.extents / 2);
                List<GameObject> listObject = new List<GameObject>();
                foreach (var col in colliders)
                {
                    if (col.gameObject != parent && col.gameObject.activeInHierarchy)
                        listObject.Add(col.gameObject);
                }

                return listObject;
            }
            else return null;
        }

        public List<GameObject> GetObjectsAround(float radius, int layerIndex)
        {
            var colliders = Physics.OverlapSphere(this.transform.position, radius, layerIndex);
            List<GameObject> listObject = new List<GameObject>();
            foreach (var col in colliders) listObject.Add(col.gameObject);

            return listObject;
        }

        public void Encapsulate(Collider c)
        {
            var collider = GetComponent<Collider>();
            if (collider is BoxCollider a && c is BoxCollider b)
            {
                var bounds = a.bounds;
                bounds.Encapsulate(b.bounds);

                a.center = bounds.center - transform.position;
                a.size = bounds.size;
            }
        }
    }
}