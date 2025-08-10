using UnityEngine;

public class SpawnTails : MonoBehaviour
{
    [SerializeField] private GameObject[] arrTails;
    [SerializeField] private Vector3 pointSpawn1 = Vector3.zero;
    [SerializeField] private Vector3 pointSpawn2 = Vector3.zero;
    [SerializeField] private LevelControl levelControl;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject SpawnMovingTail(int numTail, int offsetX, int numPoint, float speed)
    {
        GameObject tail = null;
        if (numTail >= 0 && numTail < arrTails.Length)
        {
            Vector3 pos = (numPoint == 1) ? pointSpawn1 : pointSpawn2;
            pos.x += offsetX;
            pos.z -= 1f;
            tail = Instantiate(arrTails[numTail], pos, Quaternion.Euler(180f, 0, 0));
            tail.GetComponent<TailControl>().SetParams(numTail + 1, levelControl, true, speed);
        }
        return tail;
    }

    public GameObject SpawnTail(int numTail, int offsetX, int numPoint)
    {
        GameObject tail = null;
        if (numTail >= 0 && numTail < arrTails.Length)
        {
            Vector3 pos = (numPoint == 1) ? pointSpawn1 : pointSpawn2;
            pos.x += offsetX;
            tail = Instantiate(arrTails[numTail], pos, Quaternion.Euler(180f, 0, 0));
            tail.GetComponent<TailControl>().SetParams(numTail + 1, levelControl);
        }
        return tail;
    }

    public void SetLevelControl(LevelControl lc)
    {
        levelControl = lc;
    }
}
