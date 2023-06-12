using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using TMPro;
using UnityEngine;

namespace WorkflowAR
{
    public class Main : MonoBehaviour
    {
        public GameObject spherePrefab;
        public GameObject gridPrefab;
        public GameObject tooltipPrefab;

        private String _url = "https://api.github.com/repos/RothRobe/FST22_UEB01/actions/runs/3411067822/jobs";
        private List<GameObject> _sphereList = new List<GameObject>();
        private GameObject _grid;
        private float _spaceBetweenSpheres;
        private float _sphereSize;

        
        void Start()
        {
            _grid = Instantiate(gridPrefab);
            _grid.name = "Spheres";
            StartVisualization();
        }
        
        

        private void StartVisualization()
        {
            //Reading Data
            String jsonData = ReadData(_url);

            //Parsing Data
            Data data = JsonToData(jsonData);

            //Creating Spheres
            foreach (Job job in data.jobs)
            {
                for (int i = 0; i < job.steps.Length; i++)
                {
                    _sphereList.Add(CreateSphere(job.steps[i]));
                }
            }

            //Aligning Spheres
            _spaceBetweenSpheres = 1f / _sphereList.Count;
            AlignSpheres();

            //Drawing Lines and Resizing Spheres
            _sphereSize = _spaceBetweenSpheres / 2;
            DrawLines();
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
        
        

        private GameObject CreateSphere(Step step)
        {
            GameObject temp = Instantiate(spherePrefab, _grid.transform, true);
            if (step.conclusion.Equals("success"))
            {
                temp.GetComponent<Renderer>().material.color = Color.green;
            }
            else if (step.conclusion.Equals("failure"))
            {
                temp.GetComponent<Renderer>().material.color = Color.red;
            }
            else if (step.conclusion.Equals("skipped"))
            {
                temp.GetComponent<Renderer>().material.color = Color.gray;
            }
            else
            {
                temp.GetComponent<Renderer>().material.color = Color.black;
            }
            AddToolTip(step, temp);
            return temp;
        }

        private void AddToolTip(Step step, GameObject sphere)
        {
            GameObject toolTip = Instantiate(tooltipPrefab, sphere.transform, false);
            toolTip.GetComponent<ToolTip>().ToolTipText = "Name: " + step.name +
                                                                "\nStatus: " + step.status +
                                                                "\nConclusion: " + step.conclusion +
                                                                "\nNumber: " + step.number +
                                                                "\nStarted at: " + step.started_at +
                                                                "\nCompleted at: " + step.completed_at;
            toolTip.GetComponent<ToolTipConnector>().Target = sphere;
            
            toolTip.SetActive(false);
            toolTip.name = "ToolTip_" + step.name;
            toolTip.transform.localPosition = new Vector3(0, _sphereSize, 0);
            toolTip.transform.localScale = new Vector3(10f, 10f, 10f);
            toolTip.GetComponent<ToolTip>().ShowBackground = false;
        }
        
        
        
        private void AlignSpheres()
        {
            GridObjectCollection goc = _grid.GetComponent<GridObjectCollection>();
            goc.Rows = 1;
            goc.CellWidth = _spaceBetweenSpheres;
            goc.UpdateCollection();
        }
        
        

        private void DrawLine(GameObject sphere1, GameObject sphere2)
        {
            if (sphere1 == null || sphere2 == null)
            {
                return;
            }
            LineRenderer line = sphere1.GetComponent<LineRenderer>();

            line.enabled = true;
            line.positionCount = 2;
            line.startWidth = 0.005F;
            line.endWidth = 0.005F;
            line.material = new Material(Shader.Find("Sprites/Default"));
            line.startColor = Color.white;
            line.endColor = Color.white;

            line.SetPosition(0, sphere1.transform.position);
            line.SetPosition(1, sphere2.transform.position);
        }

        private void DrawLines()
        {
            GameObject[] spheres = _sphereList.ToArray();
            for (int i = 0; i < spheres.Length; i++)
            {
                spheres[i].transform.localScale = new Vector3(_sphereSize, _sphereSize, _sphereSize);
                if (i < spheres.Length - 1)
                {
                    DrawLine(spheres[i], spheres[i+1]);
                }
            }
        }
    }
}