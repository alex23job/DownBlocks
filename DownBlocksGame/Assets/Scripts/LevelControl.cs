using UnityEngine;

public class LevelControl : MonoBehaviour
{
    [SerializeField] private SpawnBallsControl spawnBallsControl;
    [SerializeField] private SpawnTails spawnTails;

    private bool isPause = true;
    private int _countBalls = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BeginGame(); 
        spawnTails.SetLevelControl(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPause(bool pause)
    {
        isPause = pause;
    }

    private void BeginGame()
    {
        SpawnBall(new Vector3(-1f, -1f, -1f));
        SpawnBall(new Vector3(-1f, -1f, -1f));
        SpawnBall(new Vector3(-1f, -1f, -1f));
        SpawnTail();
    }

    private void SpawnBall(Vector3 target)
    {
        _countBalls++;
        int numColor = Random.Range(0, 4);
        spawnBallsControl.SpawnBall(numColor, target);  
    }

    private void SpawnTail()
    {
        spawnTails.SpawnTail(4, 0, 1);
    }

    public void TranslateTarget(Vector3 pos)
    {
        SpawnBall(pos);
    }

    public void ErrorBall(GameObject errBall)
    {
        Destroy(errBall);
    }

    public void DestroyTail(GameObject tail)
    {
        Destroy(tail);
    }
}
