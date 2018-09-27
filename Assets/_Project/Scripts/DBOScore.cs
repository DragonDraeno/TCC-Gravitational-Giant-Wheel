[System.Serializable]
public class DBOScore
{
    public float score;
    public string name;

    public DBOScore()
    {
    }

    public DBOScore(float score, string name)
    {
        this.score = score;
        this.name = name;
    }
}
