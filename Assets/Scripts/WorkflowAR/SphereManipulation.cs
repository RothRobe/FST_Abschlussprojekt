using System;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

namespace WorkflowAR
{
    public class SphereManipulation : MonoBehaviour
    {
        public String url;
        public String html_url;
        public bool isColliding;
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<ObjectManipulator>().OnManipulationEnded.AddListener(Rebuild);
        }

        public void Rebuild(ManipulationEventData data)
        {
            if (isColliding)
            {
                GameObject main = GameObject.Find("Main");
                main.GetComponent<RunMenu>().DestroyMenu();
                Debug.Log(url);
                TimeLine tl = main.GetComponent<TimeLine>();
                tl.URL = html_url;
                tl.StartVisualization(url + "/jobs");
            }
            else
            {
                GameObject parent = GameObject.Find("EmptyParent");

                foreach (Transform child in parent.transform)
                {
                    child.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                }
                
                parent.GetComponent<GridObjectCollection>().UpdateCollection();
            }
        }
    }
}
