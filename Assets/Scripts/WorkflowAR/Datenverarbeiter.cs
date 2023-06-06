using System;
using UnityEngine;

namespace WorkflowAR
{
    public class Datenverarbeiter : MonoBehaviour
    {
        private string jsonString =
            "{\"total_count\":1,\"jobs\":[{\"id\":9337192097,\"run_id\":3411067822,\"workflow_name\":\"Java CI with Gradle\",\"head_branch\":\"main\",\"run_url\":\"https://api.github.com/repos/RothRobe/FST22_UEB01/actions/runs/3411067822\",\"run_attempt\":1,\"node_id\":\"CR_kwDOIXaCmM8AAAACLIo-oQ\",\"head_sha\":\"1d09292125ca8623257acf0a072c680de7cb12bf\",\"url\":\"https://api.github.com/repos/RothRobe/FST22_UEB01/actions/jobs/9337192097\",\"html_url\":\"https://github.com/RothRobe/FST22_UEB01/actions/runs/3411067822/jobs/5674805385\",\"status\":\"completed\",\"conclusion\":\"failure\",\"created_at\":\"2022-11-07T14:12:10Z\",\"started_at\":\"2022-11-07T14:12:18Z\",\"completed_at\":\"2022-11-07T14:12:26Z\",\"name\":\"build\",\"steps\":[{\"name\":\"Set up job\",\"status\":\"completed\",\"conclusion\":\"success\",\"number\":1,\"started_at\":\"2022-11-07T14:12:17.000Z\",\"completed_at\":\"2022-11-07T14:12:19.000Z\"},{\"name\":\"Run actions/checkout@v3\",\"status\":\"completed\",\"conclusion\":\"success\",\"number\":2,\"started_at\":\"2022-11-07T14:12:19.000Z\",\"completed_at\":\"2022-11-07T14:12:20.000Z\"},{\"name\":\"Set up JDK 11\",\"status\":\"completed\",\"conclusion\":\"success\",\"number\":3,\"started_at\":\"2022-11-07T14:12:21.000Z\",\"completed_at\":\"2022-11-07T14:12:21.000Z\"},{\"name\":\"Build with Gradle\",\"status\":\"completed\",\"conclusion\":\"failure\",\"number\":4,\"started_at\":\"2022-11-07T14:12:21.000Z\",\"completed_at\":\"2022-11-07T14:12:23.000Z\"},{\"name\":\"Test with Gradle\",\"status\":\"completed\",\"conclusion\":\"skipped\",\"number\":5,\"started_at\":\"2022-11-07T14:12:23.000Z\",\"completed_at\":\"2022-11-07T14:12:23.000Z\"},{\"name\":\"Post Build with Gradle\",\"status\":\"completed\",\"conclusion\":\"success\",\"number\":8,\"started_at\":\"2022-11-07T14:12:23.000Z\",\"completed_at\":\"2022-11-07T14:12:24.000Z\"},{\"name\":\"Post Set up JDK 11\",\"status\":\"completed\",\"conclusion\":\"success\",\"number\":9,\"started_at\":\"2022-11-07T14:12:26.000Z\",\"completed_at\":\"2022-11-07T14:12:26.000Z\"},{\"name\":\"Post Run actions/checkout@v3\",\"status\":\"completed\",\"conclusion\":\"success\",\"number\":10,\"started_at\":\"2022-11-07T14:12:26.000Z\",\"completed_at\":\"2022-11-07T14:12:26.000Z\"},{\"name\":\"Complete job\",\"status\":\"completed\",\"conclusion\":\"success\",\"number\":11,\"started_at\":\"2022-11-07T14:12:24.000Z\",\"completed_at\":\"2022-11-07T14:12:24.000Z\"}],\"check_run_url\":\"https://api.github.com/repos/RothRobe/FST22_UEB01/check-runs/9337192097\",\"labels\":[\"ubuntu-latest\"],\"runner_id\":1,\"runner_name\":\"Hosted Agent\",\"runner_group_id\":2,\"runner_group_name\":\"GitHub Actions\"}]}";

        public Data data;
        
        void Start()
        {
            data = JsonToData(jsonString);
            
        }

        private Data JsonToData(string json)
        {
            return JsonUtility.FromJson<Data>(json);
        }
        
    }
    
    [Serializable]
    public class Data
    {
        public int total_count;
        public Job[] jobs;
        public int runner_id;
        public string runner_name;
        public int runner_group_id;
        public string runner_group_name;

        public Data(int total_count, Job[] jobs, int runner_id, string runner_name, int runner_group_id, string runner_group_name)
        {
            this.total_count = total_count;
            this.jobs = jobs;
            this.runner_id = runner_id;
            this.runner_name = runner_name;
            this.runner_group_id = runner_group_id;
            this.runner_group_name = runner_group_name;
        }

        public int Total_count => total_count;

        public Job[] Jobs => jobs;

        public int Runner_id => runner_id;

        public string Runner_name => runner_name;

        public int Runner_group_id => runner_group_id;

        public string Runner_group_name => runner_group_name;
    }

    [Serializable]
    public class Job
    {
        public int id;
        public int run_id;
        public string workflow_name;
        public string head_branch;
        public string run_url;
        public int run_attempt;
        public string node_id;
        public string head_sha;
        public string url;
        public string html_url;
        public string status;
        public string conclusion;
        public string created_at;
        public string started_at;
        public string completed_at;
        public string name;
        public Step[] steps;
        public string check_run_url;

        public Job(int id, int run_id, string workflow_name, string head_branch, string run_url, int run_attempt, string node_id, string head_sha, string url,
            string html_url, string status, string conclusion, string created_at, string started_at, string completed_at, string name, Step[] steps, string check_run_url)
        {
            this.id = id;
            this.run_id = run_id;
            this.workflow_name = workflow_name;
            this.head_branch = head_branch;
            this.run_url = run_url;
            this.run_attempt = run_attempt;
            this.node_id = node_id;
            this.head_sha = head_sha;
            this.url = url;
            this.html_url = html_url;
            this.status = status;
            this.conclusion = conclusion;
            this.created_at = created_at;
            this.started_at = started_at;
            this.completed_at = completed_at;
            this.name = name;
            this.steps = steps;
            this.check_run_url = check_run_url;
        }
        
    }

    [Serializable]
    public class Step
    {
        public string name;
        public string status;
        public string conclusion;
        public int number;
        public string started_at;
        public string completed_at;

        public Step(string name, string status, string conclusion, int number, string started_at, string completed_at)
        {
            this.name = name;
            this.status = status;
            this.conclusion = conclusion;
            this.number = number;
            this.started_at = started_at;
            this.completed_at = completed_at;
        }
    }
}