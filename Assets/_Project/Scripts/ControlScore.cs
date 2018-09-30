using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using TouchControlsKit;
using System.Linq;
using System.Threading.Tasks;

public class ControlScore : MonoBehaviour
{
    [SerializeField] private ControlGame controlGame;
    [SerializeField] private GameObject ScoreScrollView;
    [SerializeField] private TextMeshProUGUI textModel;
    [SerializeField] private TextMeshProUGUI textModelPlayer;

    [SerializeField] private TMP_InputField namePlayer;
    [SerializeField] private Button savePlayerScore;

    [SerializeField] private GameObject panelControlScore;
    [SerializeField] private GameObject panelInsertName;
    [SerializeField] private GameObject panelchoseControl;

    [SerializeField] private List<DBOScore> list;
    
    private bool useDatabase;
    private bool iscreate;

    private void Awake()
    {
        savePlayerScore.onClick.AddListener(CallScore);
        list = new List<DBOScore>();
        iscreate = false;
        useDatabase = false;
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
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

    public void Update()
    {
        if (TCKInput.GetAction("GameReloadBtn", EActionEvent.Down))
        {
            GameReloadLevelBtn();
        }
        if (TCKInput.GetAction("BackToMenuBtn", EActionEvent.Down))
        {
            BackToMenuBtn();
        }
    }

    public void FinishTime()
    {
        panelControlScore.SetActive(true);
    }

    public void CallScore() {
        if (iscreate == false && useDatabase == true)
        {
            iscreate = true;
            Time.timeScale = 0;
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tcc-gw.firebaseio.com/");
            DatabaseReference databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            //NewScore(databaseReference, controlGame.TimerTotal, "matheus8");
            string nameAux = " ";
            if (namePlayer.text != null && namePlayer.text.Length > 2)
            {
                nameAux = namePlayer.text;
            }
            else {
                string[] Alphabet = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                nameAux = Alphabet[Random.Range(0, Alphabet.Length)]+ Alphabet[Random.Range(0, Alphabet.Length)]+ Alphabet[Random.Range(0, Alphabet.Length)]+ Alphabet[Random.Range(0, Alphabet.Length)]+ Alphabet[Random.Range(0, Alphabet.Length)];
            }
            panelInsertName.SetActive(false);
            PutScore(databaseReference, controlGame.TimerTotal, nameAux);
            GetScore(nameAux);
        }
    }
    public void PutScore(DatabaseReference mDatabaseRef, float score, string name)
    {
        DBOScore dboscore = new DBOScore(score, name);
        string json = JsonUtility.ToJson(dboscore);
        Firebase.Database.DatabaseReference dbRef = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
        dbRef.Child("DBOScore").Push().SetRawJsonValueAsync(json);
    }

    public Task GetScore(string namekey)
    {
        Firebase.Database.FirebaseDatabase dbInstance = Firebase.Database.FirebaseDatabase.DefaultInstance;
        return dbInstance.GetReference("DBOScore").OrderByChild("score").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                int doPlayerPosition = 1;
                foreach (DataSnapshot score in snapshot.Children.Reverse())
                {
                    if (doPlayerPosition < 101)
                    {
                        IDictionary dictScore = (IDictionary)score.Value;
                        PlayersInView(doPlayerPosition, dictScore["name"], dictScore["score"], namekey);
                        doPlayerPosition++;
                    }
                    else {
                        break;
                    }
                }
            }
        });
    }

    public void PlayersInView(int position, object name, object score, string keyName) {
        TextMeshProUGUI textAux;
        if (keyName != name.ToString())
        {
            textModel.text = position + "° - " + name + ": " + score;
            textAux = textModel;
        }
        else {
            textModelPlayer.text = position + "° - " + name + ": " + score;
            textAux = textModelPlayer;
        }

        textAux.enabled = true;
        Instantiate(textAux, ScoreScrollView.transform);
    }

    public void GameReloadLevelBtn()
    {
        SceneManager.LoadScene("fase00", LoadSceneMode.Single);
    }

    public void BackToMenuBtn()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}

