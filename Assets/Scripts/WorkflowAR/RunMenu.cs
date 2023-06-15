using System;
using System.Net;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

namespace WorkflowAR
{
    /*
     * This class creates the menu which let's you visualize a specific run.
     * Each run is visualized by a sphere.
     * When you grab a sphere and release it in the transparent box to the right, this run will be visualized.
     * When you release it outside the box, it will be warped back to it's original position.
     */
    public class RunMenu : MonoBehaviour
    {
        private GameObject _spherePrefab;
        private GameObject _toolTipPrefab;
        private GameObject _cubePrefab;
        private GameObject _parent;
        private GameObject _cube;
        private readonly Vector3 _spheresize = new Vector3(0.1f, 0.1f, 0.1f);
        private void Start()
        {
            _spherePrefab = Resources.Load<GameObject>("Prefabs/SpherePrefab");
            _toolTipPrefab = Resources.Load<GameObject>("Prefabs/ToolTipPrefab");
            _cubePrefab = Resources.Load<GameObject>("Prefabs/TransparentCubePrefab");
        }

        public void ShowMenu(String url)
        {
            DestroyMenu();
            Data data = JsonToData(ReadData(url));
            CreateMenu(data);
        }

        private String ReadData(String url)
        {
            WebClient wb = new WebClient();
            wb.Headers.Set("User-Agent", "PostmanRuntime/7.29.2");
            wb.Headers.Set("Host", "api.github.com");
            String data = wb.DownloadString(url);
            return data;
        }
        
        private Data JsonToData(string json)
        {
            return JsonUtility.FromJson<Data>(json);
        }

        private void CreateMenu(Data data)
        {
            //Creating a parent object and adding a GridObjectCollection
            _parent = new GameObject();
            _parent.transform.localPosition = new Vector3(0, 0, 0.5f);
            _parent.name = "EmptyParent";
            GridObjectCollection goc = _parent.AddComponent<GridObjectCollection>();
            goc.Layout = LayoutOrder.ColumnThenRow;
            goc.Columns = 6;
            goc.CellHeight = _spheresize.y * 2;
            goc.CellWidth = _spheresize.x * 2;

            //Then create a sphere for each run and update the GridObjectCollection
            foreach (WorkflowRun wr in data.workflow_runs)
            {
                CreateSphere(wr);
            }

            goc.UpdateCollection();

            //Create the transparent cube which let's you visualize the specified run
            _cube = Instantiate(_cubePrefab);
            _cube.name = "Cube";
            Rigidbody rb = _cube.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
            _cube.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            _cube.transform.localPosition = new Vector3(1, 0, 0.5f);
            _cube.AddComponent<CubeCollisionDetection>();
        }

        private void CreateSphere(WorkflowRun wr)
        {
            GameObject temp = Instantiate(_spherePrefab, _parent.transform, false);
            if (wr.conclusion.Equals("success"))
            {
                temp.GetComponent<Renderer>().material.color = Color.green;
            }
            else if (wr.conclusion.Equals("failure"))
            {
                temp.GetComponent<Renderer>().material.color = Color.red;
            }
            else 
            {
                temp.GetComponent<Renderer>().material.color = Color.white;
            }

            //Adding the necessary components to make the spheres interactable. Also adding tooltips
            temp.GetComponent<SphereCollider>().isTrigger = true;
            temp.AddComponent<NearInteractionGrabbable>();
            temp.AddComponent<ObjectManipulator>();
            SphereManipulation manipulation = temp.AddComponent<SphereManipulation>();
            manipulation.url = wr.url;
            manipulation.html_url = wr.html_url;
            Rigidbody rb = temp.AddComponent<Rigidbody>();
            rb.useGravity = false;
            temp.transform.localPosition = new Vector3(0, 0, 0);
            temp.transform.localScale = _spheresize;
            AddToolTip(wr, temp);
        }
        
        private void AddToolTip(WorkflowRun wr, GameObject sphere)
        {
            GameObject toolTip = Instantiate(_toolTipPrefab, sphere.transform, false);
            toolTip.GetComponent<ToolTip>().ToolTipText = "Name: " + wr.name +
                                                          "\nID: " + wr.id +
                                                          "\nStatus: " + wr.status +
                                                          "\nConclusion: " + wr.conclusion +
                                                          "\nEvent: " + wr.trigger +
                                                          "\nUrl: " + wr.html_url;

            toolTip.SetActive(false);
            toolTip.name = "ToolTip_" + wr.name;
            toolTip.transform.localPosition = new Vector3(0, 0, _spheresize.z * -8);
            toolTip.transform.localScale = new Vector3(4f, 4f, 4f);
            toolTip.GetComponent<ToolTip>().ShowBackground = false;
        }

        public void DestroyMenu()
        {
            Destroy(_parent);
            Destroy(GameObject.Find("Cube"));
            GameObject.Find("Main").GetComponent<TimeLine>().DestroyTimeline();
        }
    }
}