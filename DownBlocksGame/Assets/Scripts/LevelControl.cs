using UnityEngine;

public class LevelControl : MonoBehaviour
{
    [SerializeField] private SpawnBallsControl spawnBallsControl;

    private bool isPause = true;
    private int _countBalls = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BeginGame();         
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
        SpawnBall();
        SpawnBall();
        SpawnBall();
    }

    private void SpawnBall()
    {
        _countBalls++;
        int numColor = Random.Range(0, 4);
        spawnBallsControl.SpawnBall(numColor);  
    }
}
