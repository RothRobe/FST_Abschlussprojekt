using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;
using UnityEngine.Serialization;

namespace WorkflowAR
{
    public class Spährenerzeuger : MonoBehaviour

    {
    private readonly GameObject _grid;
    private readonly GameObject _spherePrefab;

    //private Step success = new Step("Set up job", "completed", "success", 1, "2022", "2023");
    //private Step failure = new Step("Set up job", "completed", "failure", 1, "2022", "2023");
    //private Step skipped = new Step("Set up job", "completed", "skipped", 1, "2022", "2023");
    //private Step other = new Step("Set up job", "completed", "other", 1, "2022", "2023");

    public Spährenerzeuger(GameObject gridPrefab, GameObject spherePrefab)
    {
        _grid = Instantiate(gridPrefab);
        this._spherePrefab = spherePrefab;
    }

    //void Start()
    //{
        //grid = Instantiate(gridPrefab);
        //GameObject sphere1 = CreateSphere(success);
        //GameObject sphere2 = CreateSphere(failure);
        //GameObject sphere3 = CreateSphere(skipped);

        //sphere2.transform.position = new Vector3(2, 0, 0);
        //sphere3.transform.position = new Vector3(4, 0, 0);
        //AlignSpheres();

        //DrawLine(sphere1, sphere2);
        //DrawLine(sphere2, sphere3);
    //}

    public GameObject CreateSphere(Step step)
    {
        GameObject temp = Instantiate(_spherePrefab, _grid.transform, true);
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
        return temp;
    }

    public void DrawLine(GameObject sphere1, GameObject sphere2)
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

    public void AlignSpheres()
    {
        GridObjectCollection goc = _grid.GetComponent<GridObjectCollection>();
        goc.Rows = 1;
        goc.CellWidth = 2;
        goc.UpdateCollection();
    }
    }
}