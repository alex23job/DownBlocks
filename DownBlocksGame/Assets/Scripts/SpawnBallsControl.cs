using UnityEngine;

public class SpawnBallsControl : MonoBehaviour
{
    [SerializeField] private Material[] arrMaterials;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject snowPrefab;

    private GameObject ball1 = null, ball2 = null, ball3 = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetParams()
    {

    }

    public void SpawnBall(int numColor, Vector3 target, int typeBall = 1)
    {
        if (ball1 != null && target != new Vector3(-1f, -1f, -1f)) 
        { 
            ball1.GetComponent<BallMovement>().SetTarget(target);
        }
        ball1 = ball2; ball2 = ball3;
        Vector3 pos = transform.position;
        pos.z = -0.2f;

        if (typeBall == 1)
        {
            ball3 = Instantiate(ballPrefab, pos, Quaternion.identity);
            ball3.GetComponent<BallColor>().SetParams(numColor + 1, arrMaterials[numColor]);
        }
        else if (typeBall == 2)
        {
            ball3 = Instantiate(bombPrefab, pos, Quaternion.Euler(new Vector3(160f, 0, 0)));
            print("Spawn bomb");
        }
        else if (typeBall == 3)
        {
            pos.z = -1f;
            ball3 = Instantiate(snowPrefab, pos, Quaternion.identity);
            ball3.transform.localScale = new Vector3(50f, 50f, 50f);
            print("Spawn snow");
        }
        ball3.GetComponent<BallMovement>().SetParams(typeBall);
        if (ball2 != null)
        {
            int tpBall = ball2.GetComponent<BallMovement>().BallType;
            pos.x -= 0.9f; pos.z = -0.3f; 
            if (tpBall == 3)
            {
                ball2.transform.localScale = new Vector3(70f, 70f, 70f);
                pos.z = -1f;
            }
            ball2.transform.position = pos; 
        }
        if (ball1 != null) 
        {
            int tpBall = ball1.GetComponent<BallMovement>().BallType;
            if (tpBall == 3)
            {
                ball1.transform.localScale = new Vector3(100f, 100f, 100f);
            }
            pos.x -= 1f; pos.z = -1f; ball1.transform.position = pos; 
        }
    }
}
