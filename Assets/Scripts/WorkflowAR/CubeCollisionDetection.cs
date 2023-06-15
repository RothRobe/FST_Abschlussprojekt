using UnityEngine;

namespace WorkflowAR
{
    public class CubeCollisionDetection : MonoBehaviour
    {
        private Color _defaultColor;
        private Color _collisionColor;
        private void OnTriggerEnter(Collider other)
        {
            _defaultColor = other.gameObject.GetComponent<Renderer>().material.color;
            other.gameObject.GetComponent<Renderer>().material.color = _collisionColor;
            other.gameObject.GetComponent<SphereManipulation>().isColliding = true;
            foreach (var renderer in GetComponentsInChildren<Renderer>())
            {
                renderer.material.color = _collisionColor;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            other.gameObject.GetComponent<Renderer>().material.color = _defaultColor;
            other.gameObject.GetComponent<SphereManipulation>().isColliding = false;
            foreach (var renderer in GetComponentsInChildren<Renderer>())
            {
                renderer.material.color = Color.white;
            }
        }

        private void Start()
        {
            _collisionColor = new Color(0.074f, 0.145f, 0.494f);
        }
    }
}
