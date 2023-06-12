using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

namespace WorkflowAR
{
    public class ShowOnTouch : MonoBehaviour, IMixedRealityTouchHandler
    {
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void OnTouchStarted(HandTrackingInputEventData eventData)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }

        public void OnTouchCompleted(HandTrackingInputEventData eventData)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }

        public void OnTouchUpdated(HandTrackingInputEventData eventData)
        {
            //throw new System.NotImplementedException();
        }
    }
}