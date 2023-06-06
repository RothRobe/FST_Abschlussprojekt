using System;
using System.IO;
using System.Net;
using UnityEngine;

namespace WorkflowAR
{
    public class Datenleser : MonoBehaviour
    {
        readonly String url = "https://api.github.com/repos/RothRobe/FST22_UEB01/actions/runs/3411067822/jobs";
        void Start()
        {
            //LeseDaten();
        }
        void LeseDaten()
        {
            WebClient wb = new WebClient();
            wb.Headers.Set("User-Agent", "PostmanRuntime/7.29.2");
            wb.Headers.Set("Host", "api.github.com");
            String data2 = wb.DownloadString(url);
            Debug.Log(data2);
        }
    }
}