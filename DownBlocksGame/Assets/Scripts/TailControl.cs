using UnityEngine;

public class TailControl : MonoBehaviour
{
    [SerializeField] private int typeTail;
    [SerializeField] private LevelControl levelControl;

    public int TypeTail {  get { return typeTail; } }

    private int[] znCol = new int[5] { 0, 0, 0, 0, 0};
    private bool isMoving = false;
    private float speed = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            Vector3 pos = transform.position;
            if (pos.y < -9f)
            {
                Destroy(gameObject);
                levelControl.ClearMovingTail();
            }
            else
            {
                pos.y -= Time.deltaTime * speed;
                transform.position = pos;
            }
        }
    }

    public void SetParams(int tailType, LevelControl lc, bool mode = false, float sp = 2f)
    {
        isMoving = mode;
        speed = sp;
        typeTail = tailType;
        levelControl = lc;
    }

    private void OnMouseUp()
    {
        //print("OnMouseUp");
        if (levelControl != null)
        {
            //print("OnMouseUp 2");
            if (Input.GetMouseButtonUp(0) == true)
            {                
                Vector3 pos = transform.position;
                //print($"OnMouseUp tail={gameObject.name} pos={pos} isMoving={isMoving}");
                pos.z = (isMoving) ? -2 : -1;
                for (int i = 0; i < typeTail; i++)
                {
                    if (znCol[i] == 0)
                    {
                        pos.x += -1 * i;
                        break;
                    }
                }
                levelControl.TranslateTarget(pos);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //print(other.name);
        if (other.CompareTag("ball"))
        {
            other.gameObject.tag = "Untagged";
            int i, col = other.gameObject.GetComponent<BallColor>().NumColor;
            //print($"ball col={col}");
            if (znCol[0] == 0)
            {
                znCol[0] = col;
                other.transform.parent = transform;
                other.transform.localPosition = Vector3.zero;
                other.gameObject.GetComponent<BallMovement>().StopMovement();
            }
            else
            {
                if (znCol[0] == col)
                {
                    for (i = 1; i < typeTail; i++)
                    {
                        if (znCol[i] == 0)
                        {
                            znCol[i] = col;
                            other.transform.parent = transform;
                            Vector3 pos = Vector3.zero;
                            pos.x = i * -0.01f;
                            other.transform.localPosition = pos;
                            other.gameObject.GetComponent<BallMovement>().StopMovement();
                            break;
                        }
                    }
                }
                else
                {
                    levelControl.ErrorBall(other.gameObject);
                }
            }
            bool isFull = true;
            for (i = 0; i < typeTail; i++)
            {
                if (znCol[i] == 0) { isFull = false; break; }
            }
            if (isFull)
            {
                int mult = 1;
                if (isMoving) mult = 2;
                levelControl.DestroyTail(gameObject, mult);
            }
        }
    }
}
