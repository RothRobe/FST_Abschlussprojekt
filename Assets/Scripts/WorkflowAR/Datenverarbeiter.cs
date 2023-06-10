using System;
using UnityEngine;

namespace WorkflowAR
{
    public class Datenverarbeiter
    {
        private string jsonString =
            "{\"total_count\":1,\"jobs\":[{\"id\":9337192097,\"run_id\":3411067822,\"workflow_name\":\"Java CI with Gradle\",\"head_branch\":\"main\",\"run_url\":\"https://api.github.com/repos/RothRobe/FST22_UEB01/actions/runs/3411067822\",\"run_attempt\":1,\"node_id\":\"CR_kwDOIXaCmM8AAAACLIo-oQ\",\"head_sha\":\"1d09292125ca8623257acf0a072c680de7cb12bf\",\"url\":\"https://api.github.com/repos/RothRobe/FST22_UEB01/actions/jobs/9337192097\",\"html_url\":\"https://github.com/RothRobe/FST22_UEB01/actions/runs/3411067822/jobs/5674805385\",\"status\":\"completed\",\"conclusion\":\"failure\",\"created_at\":\"2022-11-07T14:12:10Z\",\"started_at\":\"2022-11-07T14:12:18Z\",\"completed_at\":\"2022-11-07T14:12:26Z\",\"name\":\"build\",\"steps\":[{\"name\":\"Set up job\",\"status\":\"completed\",\"conclusion\":\"success\",\"number\":1,\"started_at\":\"2022-11-07T14:12:17.000Z\",\"completed_at\":\"2022-11-07T14:12:19.000Z\"},{\"name\":\"Run actions/checkout@v3\",\"status\":\"completed\",\"conclusion\":\"success\",\"number\":2,\"started_at\":\"2022-11-07T14:12:19.000Z\",\"completed_at\":\"2022-11-07T14:12:20.000Z\"},{\"name\":\"Set up JDK 11\",\"status\":\"completed\",\"conclusion\":\"success\",\"number\":3,\"started_at\":\"2022-11-07T14:12:21.000Z\",\"completed_at\":\"2022-11-07T14:12:21.000Z\"},{\"name\":\"Build with Gradle\",\"status\":\"completed\",\"conclusion\":\"failure\",\"number\":4,\"started_at\":\"2022-11-07T14:12:21.000Z\",\"completed_at\":\"2022-11-07T14:12:23.000Z\"},{\"name\":\"Test with Gradle\",\"status\":\"completed\",\"conclusion\":\"skipped\",\"number\":5,\"started_at\":\"2022-11-07T14:12:23.000Z\",\"completed_at\":\"2022-11-07T14:12:23.000Z\"},{\"name\":\"Post Build with Gradle\",\"status\":\"completed\",\"conclusion\":\"success\",\"number\":8,\"started_at\":\"2022-11-07T14:12:23.000Z\",\"completed_at\":\"2022-11-07T14:12:24.000Z\"},{\"name\":\"Post Set up JDK 11\",\"status\":\"completed\",\"conclusion\":\"success\",\"number\":9,\"started_at\":\"2022-11-07T14:12:26.000Z\",\"completed_at\":\"2022-11-07T14:12:26.000Z\"},{\"name\":\"Post Run actions/checkout@v3\",\"status\":\"completed\",\"conclusion\":\"success\",\"number\":10,\"started_at\":\"2022-11-07T14:12:26.000Z\",\"completed_at\":\"2022-11-07T14:12:26.000Z\"},{\"name\":\"Complete job\",\"status\":\"completed\",\"conclusion\":\"success\",\"number\":11,\"started_at\":\"2022-11-07T14:12:24.000Z\",\"completed_at\":\"2022-11-07T14:12:24.000Z\"}],\"check_run_url\":\"https://api.github.com/repos/RothRobe/FST22_UEB01/check-runs/9337192097\",\"labels\":[\"ubuntu-latest\"],\"runner_id\":1,\"runner_name\":\"Hosted Agent\",\"runner_group_id\":2,\"runner_group_name\":\"GitHub Actions\"}]}";

        public static Data JsonToData(string json)
        {
            return JsonUtility.FromJson<Data>(json);
        }
        
    }
    
    
}