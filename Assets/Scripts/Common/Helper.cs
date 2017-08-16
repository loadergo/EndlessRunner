using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public static class Helper {

        public static List<T> GetComponentsInChildWithTag<T>(this Transform parent, string tag) where T : Component
        {
            var result = new List<T>();
            Transform t = parent.transform;
            foreach (Transform tr in t)
            {
                if (tr.tag == tag)
                {
                    var component = tr.GetComponent<T>();
                    if (component != null)
                    {
                        result.Add(component);
                    }
                }
            }
            return result;
        }

        public static List<GameObject> GetChildsWithTag(this Transform parent, string tag)
        {
            var result = new List<GameObject>();
            Transform t = parent.transform;
            foreach (Transform tr in t)
            {
                if (tr.tag == tag)
                {
                    result.Add(tr.gameObject);
                }
            }
            return result;
        }

        public static List<GameObject> GetAllChilds(this Transform parent)
        {
            var result = new List<GameObject>();
            Transform t = parent.transform;
            foreach (Transform tr in t)
            {
                result.Add(tr.gameObject);
            }
            return result;
        }
    }
}
