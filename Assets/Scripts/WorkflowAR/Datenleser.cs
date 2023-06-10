using System;
using System.Net;

namespace WorkflowAR
{
    public class Datenleser
    {
        //readonly String url = "https://api.github.com/repos/RothRobe/FST22_UEB01/actions/runs/3411067822/jobs";
        public static String LeseDaten(String url)
        {
            WebClient wb = new WebClient();
            wb.Headers.Set("User-Agent", "PostmanRuntime/7.29.2");
            wb.Headers.Set("Host", "api.github.com");
            String data = wb.DownloadString(url);
            return data;
        }
    }
}