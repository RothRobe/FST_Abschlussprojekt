using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

namespace WorkflowAR
{
    public class TimeLine : MonoBehaviour
    {
    private GameObject spherePrefab;
    private GameObject gridPrefab;
    private GameObject tooltipPrefab;

    //private String _url = "https://api.github.com/repos/RothRobe/FST22_UEB01/actions/runs/3411067822/jobs";
    private String _url = "https://api.github.com/repos/microsoft/AI-For-Beginners/actions/runs/5177278694/jobs";
    private List<GameObject>[] _sphereList;
    private GameObject[] sphereParents;
    private Vector3 spheresize;


    void Start()
    {
        spherePrefab = Resources.Load<GameObject>("Prefabs/SpherePrefab");
        gridPrefab = Resources.Load<GameObject>("Prefabs/GridPrefab");
        tooltipPrefab = Resources.Load<GameObject>("Prefabs/ToolTipPrefab");

        spheresize = new Vector3(0.1f, 0.1f, 0.1f);
        
        //StartVisualization(_url);
    }


    public void StartVisualization(String url)
    {
        DestroyTimeline();
        //Reading Data
        String jsonData = ReadData(url);

        //Parsing Data
        Run run = JsonToData(jsonData);

        //Creating Spheres
        _sphereList = new List<GameObject>[run.total_count];
        sphereParents = new GameObject[run.total_count];

        for (int i = 0; i < run.total_count; i++)
        {
            _sphereList[i] = new List<GameObject>();
            sphereParents[i] = new GameObject();
            sphereParents[i].transform.position = new Vector3(0f, spheresize.y * 2 * i, 0.5f);

            for (int j = 0; j < run.jobs[i].steps.Length; j++)
            {
                _sphereList[i].Add(CreateSphere(run.jobs[i].steps[j], i, j));
            }
            //Aligning Spheres
            //AlignSpheres(i);
        }
        //Drawing Lines and Resizing Spheres
        DrawAllLines();

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



    private GameObject CreateSphere(Step step, int jobIndex, int stepIndex)
    {
        GameObject temp = Instantiate(spherePrefab, sphereParents[jobIndex].transform, false);
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
        
        //TODO implement getting the distance between steps
        temp.transform.localPosition = new Vector3(stepIndex * 2 * spheresize.x, 0f, 0f);
        temp.transform.localScale = spheresize;
        
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
        //toolTip.GetComponent<ToolTipConnector>().Target = sphere;

        toolTip.SetActive(false);
        toolTip.name = "ToolTip_" + step.name;
        toolTip.transform.localPosition = new Vector3(0, 0, spheresize.z * -8);
        toolTip.transform.localScale = new Vector3(4f, 4f, 4f);
        toolTip.GetComponent<ToolTip>().ShowBackground = false;
    }


/*
    private void AlignSpheres(int job)
    {
        GridObjectCollection goc = _grids[job].GetComponent<GridObjectCollection>();
        goc.Rows = 1;
        goc.CellWidth = _spaceBetweenSpheres[job];
        goc.UpdateCollection();
    }
*/


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

    public void DestroyTimeline()
    {
        if (sphereParents == null || sphereParents.Length == 0) return;
        foreach (GameObject parent in sphereParents)
        {
            Destroy(parent);
        }
    }
    }
}