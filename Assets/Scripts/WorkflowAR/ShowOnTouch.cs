using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

namespace WorkflowAR
{
    public class ShowOnTouch : MonoBehaviour, IMixedRealityTouchHandler
    {
        private Color _originalColor;
        // Start is called before the first frame update
        void Start()
        {
            _originalColor = GetComponent<Renderer>().material.color;
        }

        public void OnTouchStarted(HandTrackingInputEventData eventData)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<Renderer>().material.color = new Color(0.074f, 0.145f, 0.494f);
        }

        public void OnTouchCompleted(HandTrackingInputEventData eventData)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            GetComponent<Renderer>().material.color = _originalColor;
        }

        public void OnTouchUpdated(HandTrackingInputEventData eventData)
        {
            //ignored
        }
    }
}