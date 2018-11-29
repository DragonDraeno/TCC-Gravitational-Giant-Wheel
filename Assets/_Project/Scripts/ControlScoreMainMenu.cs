using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class ControlScoreMainMenu : MonoBehaviour {

    [SerializeField] private List<DBOScore> list;
    [SerializeField] private GameObject ScoreScrollView;
    [SerializeField] private TextMeshProUGUI textModelPlayer;

    private void Start()
    {
        LoadScoreInMenu();
    }
    
    public Task LoadScoreInMenu()
    {
        Firebase.Database.FirebaseDatabase dbInstance = Firebase.Database.FirebaseDatabase.DefaultInstance;
        return dbInstance.GetReference("DBOScore").OrderByChild("score").LimitToLast(100).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                int doPlayerPosition = 1;
                foreach (DataSnapshot score in snapshot.Children.Reverse())
                {
                    IDictionary dictScore = (IDictionary)score.Value;
                    PlayersInView(doPlayerPosition, dictScore["name"], dictScore["score"]);
                    doPlayerPosition++;
                }
                
            }
        });
    }

    public void PlayersInView(int position, object name, object score)
    {
        TextMeshProUGUI textAux;
        textModelPlayer.text = position + "° - " + name + ": " + score;
        textAux = textModelPlayer;
        textAux.enabled = true;
        Instantiate(textAux, ScoreScrollView.transform);
    }
}
