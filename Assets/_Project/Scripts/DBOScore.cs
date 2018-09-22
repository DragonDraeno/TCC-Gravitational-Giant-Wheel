[System.Serializable]
public class DBOScore
{
    public bool success;

    public Data data;

    [System.Serializable]
    public class Data
    {
        public Created_by created_by;

        [System.Serializable]
        public struct Created_by
        {
            public string id;
            public string email;
        }

        public Modified_by modified_by;

        [System.Serializable]
        public struct Modified_by
        {
            public string id;
            public string email;
        }

        public bool verified;
        public bool active;
        public string user_category;
        public string vid;
        public string _id;
        public string email;
        public string name;
        public string account;

        public Workspaces[] workspaces;

        [System.Serializable]
        public struct Workspaces
        {
            public int role;
            public string _id;
            public string wsid;
        }

        public string[] courses;
        public string avatar_hash;
        public string about_me;
        public string photo_url;
        public string last_seen;
        public bool is_development;
        public string phone;
        public string created_date;
        public string modified_date;
        public int __v;
        public string last_login;
    }
}