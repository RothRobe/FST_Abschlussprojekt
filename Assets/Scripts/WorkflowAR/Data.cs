using System;

namespace WorkflowAR
{
    //These classes are used for parsing the JSON string into objects.
    
    [Serializable]
    public class Data
    {
        public int total_count;
        public WorkflowRun[] workflow_runs;
    }

    [Serializable]
    public class WorkflowRun
    {
        public long id;
        public String name;
        public String conclusion;
        public String trigger;
        public String status;
        public String url;
        public String html_url;
    }
    
    [Serializable]
    public class Run
    {
        public int total_count;
        public Job[] jobs;
    }

    [Serializable]
    public class Job
    {
        public long id;
        public long run_id;
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
    }
}