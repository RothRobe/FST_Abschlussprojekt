using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using TMPro;
using UnityEngine;

namespace WorkflowAR
{
    /*
     * This class creates a timeline of a specific run.
     * First, it reads the data from the GitHub Actions API and parses the response.
     * Then it creates a timeline for any job.
     * Each job consists of one or more steps which are represented by the spheres.
     */
    public class TimeLine : MonoBehaviour
    {
        private GameObject _spherePrefab;
        private GameObject _tooltipPrefab;
        private GameObject[] _sphereParents;
        private Vector3 _spheresize;
        private List<GameObject>[] _sphereList;
        public String URL { get; set; }


        void Start()
        {
            _spherePrefab = Resources.Load<GameObject>("Prefabs/SpherePrefab");
            _tooltipPrefab = Resources.Load<GameObject>("Prefabs/ToolTipPrefab");
            _spheresize = new Vector3(0.1f, 0.1f, 0.1f);
        }


        public void StartVisualization(String url)
        {
            //Remove old Timeline
            DestroyTimeline();

            //Reading Data
            String jsonData = ReadData(url);

            //Parsing Data
            Run run = JsonToData(jsonData);

            //Creating Spheres
            _sphereList = new List<GameObject>[run.total_count];
            _sphereParents = new GameObject[run.total_count];
            for (int i = 0; i < run.total_count; i++)
            {
                _sphereList[i] = new List<GameObject>();
                _sphereParents[i] = new GameObject();
                //Each Job has an empty parent at the correct position, so the spheres can be set via the localPosition
                _sphereParents[i].transform.position = new Vector3(0f, _spheresize.y * 2 * i, 0.5f);
                float nextX = 0;

                for (int j = 0; j < run.jobs[i].steps.Length; j++)
                {
                    _sphereList[i].Add(CreateSphere(run.jobs[i].steps[j], i, nextX));
                    nextX = nextX + _spheresize.x * 1.1f + CalculateDuration(run.jobs[i].steps[j]) * 0.02f;
                }
            }

            //Drawing the lines between spheres
            DrawAllLines();

            AddMetaData(run);

        }

        private String ReadData(String url)
        {
            WebClient wb = new WebClient();
            wb.Headers.Set("User-Agent", "PostmanRuntime/7.29.2");
            wb.Headers.Set("Host", "api.github.com");
            String data = wb.DownloadString(url);
            return data;
        }

        private Run JsonToData(string json)
        {
            return JsonUtility.FromJson<Run>(json);
        }

        private GameObject CreateSphere(Step step, int jobIndex, float x)
        {
            GameObject temp = Instantiate(_spherePrefab, _sphereParents[jobIndex].transform, false);
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
                temp.GetComponent<Renderer>().material.color = Color.white;
            }
            
            temp.transform.localPosition = new Vector3(x, 0f, 0f);
            temp.transform.localScale = _spheresize;

            AddToolTip(step, temp);
            return temp;
        }

        private void AddToolTip(Step step, GameObject sphere)
        {
            GameObject toolTip = Instantiate(_tooltipPrefab, sphere.transform, false);
            toolTip.GetComponent<ToolTip>().ToolTipText = "Name: " + step.name +
                                                          "\nStatus: " + step.status +
                                                          "\nConclusion: " + step.conclusion +
                                                          "\nNumber: " + step.number +
                                                          "\nStarted at: " + step.started_at +
                                                          "\nCompleted at: " + step.completed_at;

            toolTip.SetActive(false);
            toolTip.name = "ToolTip_" + step.name;
            toolTip.transform.localPosition = new Vector3(0, 0, _spheresize.z * -8);
            toolTip.transform.localScale = new Vector3(4f, 4f, 4f);
            toolTip.GetComponent<ToolTip>().ShowBackground = false;
        }
        
        
        private long CalculateDuration(Step step)
        {
            String[] startArray = step.started_at.Substring(11, 8).Split(':');
            String[] endArray = step.completed_at.Substring(11, 8).Split(':');

            long start = Int64.Parse(startArray[0]) * 3600 + Int64.Parse(startArray[1]) * 60 +
                         Int64.Parse(startArray[2]);
            long end = Int64.Parse(endArray[0]) * 3600 + Int64.Parse(endArray[1]) * 60 + Int64.Parse(endArray[2]);

            return end - start;

        }

        private long CalculateDuration(String startedAt, String completedAt)
        {
            String[] startArray = startedAt.Substring(11, 8).Split(':');
            String[] endArray = completedAt.Substring(11, 8).Split(':');

            long start = Int64.Parse(startArray[0]) * 3600 + Int64.Parse(startArray[1]) * 60 +
                         Int64.Parse(startArray[2]);
            long end = Int64.Parse(endArray[0]) * 3600 + Int64.Parse(endArray[1]) * 60 + Int64.Parse(endArray[2]);

            return end - start;
        }

        //Drawing a fine white line between two GameObjects
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

        private void DrawAllLines()
        {
            for (int i = 0; i < _sphereList.Length; i++)
            {
                GameObject[] spheres = _sphereList[i].ToArray();
                for (int j = 0; j < spheres.Length; j++)
                {
                    if (j > 0)
                    {
                        DrawLine(spheres[j], spheres[j - 1]);
                    }
                }
            }
        }
        //Adding a dividing "line" between timelines and adding Metadata about the run
        //Also adding a button that opens the run's GitHub page in browser
        private void AddMetaData(Run run)
        {
            GameObject line = GameObject.CreatePrimitive(PrimitiveType.Cube);
            line.name = "Line";
            line.transform.position = new Vector3(- _spheresize.x, (run.total_count - 1) * _spheresize.y, 0.5f);
            line.transform.localScale = new Vector3(0.01f, (_spheresize.y * 2 * run.total_count) - _spheresize.y, _spheresize.z);

            GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/TextPrefab"));
            go.name = "MetaData";
            TextMeshPro tmp = go.GetComponent<TextMeshPro>();
            tmp.outlineColor = Color.black;
            tmp.outlineWidth = 0.15f;
            tmp.text = "";
            foreach (Job job in run.jobs)
            {
                tmp.text += "Job name: " + job.name + "\n" +
                            "Run ID: " + job.run_id + "\n" +
                            "Duration: " + CalculateDuration(job.started_at, job.completed_at) + "\n";
            }

            GameObject button = Instantiate(Resources.Load<GameObject>("Prefabs/BrowserButtonPrefab"));
            button.name = "BrowserButton";
            button.GetComponent<ButtonConfigHelper>().OnClick.AddListener(() => Application.OpenURL(URL));
            button.transform.position = new Vector3(-0.17f, -0.07f, 0.5f);
        }

        public void DestroyTimeline()
        {
            if (_sphereParents == null || _sphereParents.Length == 0) return;
            foreach (GameObject parent in _sphereParents)
            {
                Destroy(parent);
            }
            Destroy(GameObject.Find("Line"));
            Destroy(GameObject.Find("MetaData"));
            Destroy(GameObject.Find("BrowserButton"));
        }
    }
}