using System;
using System.Net;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

namespace WorkflowAR
{
    public class RunMenu : MonoBehaviour
    {
        private GameObject _spherePrefab;
        private GameObject _toolTipPrefab;
        private GameObject _cubePrefab;
        private GameObject _parent;
        private GameObject _cube;
        private readonly Vector3 _spheresize = new Vector3(0.1f, 0.1f, 0.1f);

        private String _url;
        private void Start()
        {
            //Data data = JsonToData(ReadData("https://api.github.com/repos/RothRobe/FST22_UEB01/actions/runs"));
            //Data data = JsonToData(ReadData("https://api.github.com/repos/microsoft/AI-For-Beginners/actions/runs"));
            //CreateAllButtons(data.workflow_runs);
            //gameObject.GetComponent<TimeLine>().StartVisualization("https://api.github.com/repos/microsoft/AI-For-Beginners/actions/runs/5177278694/jobs");
            _spherePrefab = Resources.Load<GameObject>("Prefabs/SpherePrefab");
            _toolTipPrefab = Resources.Load<GameObject>("Prefabs/ToolTipPrefab");
            _cubePrefab = Resources.Load<GameObject>("Prefabs/TransparentCubePrefab");
            ShowMenu("https://api.github.com/repos/microsoft/AI-For-Beginners/actions/runs");
        }

        public void ShowMenu(String url)
        {
            DestroyMenu();
            Data data = JsonToData(ReadData(url));
            CreateMenu(data);
        }

        private String ReadData(String url)
        {
            _url = url;
            
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

            foreach (WorkflowRun wr in data.workflow_runs)
            {
                CreateSphere(wr);
            }

            goc.UpdateCollection();

            _cube = Instantiate(_cubePrefab);
            _cube.name = "Cube";
            Rigidbody rb = _cube.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
            _cube.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            _cube.transform.localPosition = new Vector3(1, 0, 0.5f);
            _cube.AddComponent<CollisionDetection>();
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


            temp.GetComponent<SphereCollider>().isTrigger = true;
            temp.AddComponent<NearInteractionGrabbable>();
            temp.AddComponent<ObjectManipulator>();
            SphereScript script = temp.AddComponent<SphereScript>();
            script.url = wr.url;
            Rigidbody rb = temp.AddComponent<Rigidbody>();
            rb.useGravity = false;
            temp.transform.localPosition = new Vector3(0, 0, 0);
            temp.transform.localScale = _spheresize;
            AddToolTip(wr, temp);
            //return temp;
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
            //toolTip.GetComponent<ToolTipConnector>().Target = sphere;

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
        }

        /*
        private void CreateButton(WorkflowRun wr)
        {
            //Skip if action is required because it usually means they are waiting for a maintainer's approval
            //Which means you will not be able to see anything because it did not run yet.
            if (wr.conclusion.Equals("action_required")) return;
            
            PressableButton button = Instantiate(buttonPrefab, goc.gameObject.transform, false);
            button.name = "Button";
            Destroy(button.transform.GetChild(4).GetChild(2).gameObject);
            Destroy(button.transform.GetChild(4).GetChild(3).gameObject);
            ButtonConfigHelper bch = button.GetComponent<ButtonConfigHelper>();
            bch.MainLabelText = wr.id + " " + wr.name;
            bch.IconStyle = ButtonIconStyle.Quad;
            bch.OnClick.AddListener(
                delegate
                {
                    gameObject.GetComponent<TimeLine>().StartVisualization(_url + "/" + wr.id + "/jobs");
                });
        }

        private void CreateAllButtons(WorkflowRun[] wrs)
        {
            foreach (WorkflowRun wr in wrs)
            {
                CreateButton(wr);
            }
            goc.UpdateCollection();
        }*/
    }
}