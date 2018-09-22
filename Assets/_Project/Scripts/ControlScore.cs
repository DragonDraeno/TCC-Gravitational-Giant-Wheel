using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class ControlScore : MonoBehaviour {

    private bool useDatabase;
    private void Awake()
    {
        useDatabase = false;
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                useDatabase = true;
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });
    }
    
    void Start () {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tcc-gw.firebaseio.com/");
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        User user = new User("matheus", "matheus.r.n@hotmail.com");
        string json = JsonUtility.ToJson(user);

        reference.Child("users").Child("001").SetRawJsonValueAsync(json);
    }
    
}

public class User
{
    public string username;
    public string email;

    public User()
    {
    }

    public User(string username, string email)
    {
        this.username = username;
        this.email = email;
    }
}

/*
 {
  "rules": {
    ".read": "auth != null",
    ".write": "auth != null"
  }
}
     */
