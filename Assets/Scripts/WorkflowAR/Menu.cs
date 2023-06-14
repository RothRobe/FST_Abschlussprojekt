using System;
using System.Net;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

namespace WorkflowAR
{
    public class Menu : MonoBehaviour
    {
        public GridObjectCollection goc;
        public PressableButton buttonPrefab;

        private String _url;
        private void Start()
        {
            //Data data = JsonToData(ReadData("https://api.github.com/repos/RothRobe/FST22_UEB01/actions/runs"));
            //Data data = JsonToData(ReadData("https://api.github.com/repos/microsoft/AI-For-Beginners/actions/runs"));
            //CreateAllButtons(data.workflow_runs);
            //gameObject.GetComponent<TimeLine>().StartVisualization("https://api.github.com/repos/microsoft/AI-For-Beginners/actions/runs/5177278694/jobs");
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
        }
    }
}