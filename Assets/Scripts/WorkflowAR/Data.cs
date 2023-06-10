using System;

namespace WorkflowAR
{
    //These classes are used for parsing the JSON string into objects.
    
    
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