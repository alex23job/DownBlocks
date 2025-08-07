using UnityEngine;

public class TailControl : MonoBehaviour
{
    [SerializeField] private int typeTail;
    [SerializeField] private LevelControl levelControl;

    public int TypeTail {  get { return typeTail; } }

    private int[] znCol = new int[5] { 0, 0, 0, 0, 0};

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetParams(int tailType, LevelControl lc)
    {
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
                //print("OnMouseUp 3");
                levelControl.TranslateTarget(transform.position);
            }
        }
    }
}
