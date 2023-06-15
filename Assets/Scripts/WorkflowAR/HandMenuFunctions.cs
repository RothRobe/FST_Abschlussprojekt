using System;
using UnityEngine;

namespace WorkflowAR
{
    public class HandMenuFunctions : MonoBehaviour
    {
        public void VisualizeFST()
        {
            GameObject.Find("Main").GetComponent<RunMenu>().ShowMenu("https://api.github.com/repos/RothRobe/FST22_UEB01/actions/runs");
        }
        public void VisualizeAI()
        {
            GameObject.Find("Main").GetComponent<RunMenu>().ShowMenu("https://api.github.com/repos/microsoft/AI-For-Beginners/actions/runs");
        }
        public void VisualizeOtter()
        {
            GameObject.Find("Main").GetComponent<RunMenu>().ShowMenu("https://api.github.com/repos/Luodian/Otter/actions/runs");
        }
    }
}