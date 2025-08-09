using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Text;

public class LevelControl : MonoBehaviour
{
    [SerializeField] private UI_Control ui_Control;
    [SerializeField] private SpawnBallsControl spawnBallsControl;
    [SerializeField] private SpawnTails spawnTails;

    private bool isPause = true;
    private int _countBalls = 0;
    private int[] _arrCell;
    private List<GameObject> _listTails = new List<GameObject>();
    private int[] reOfs = new int[10] { -5, -4, -3, -2, -1, 0, 1, 2, 3, 4};

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
        _arrCell = new int[160];
        for (int i = 0; i < 160; i++)
        {
            _arrCell[i] = 0;
        }
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
        SpawnTail();
    }

    private void SpawnTail()
    {
        if (_countBalls > 0)
        {
            _countBalls %= 10;
            if (_countBalls == 0)
            {
                ShiftTails();
            }
            List<FreeCell> freeCells = new List<FreeCell>();
            FreeCell tmp = new FreeCell(0, 0);
            StringBuilder ssb = new StringBuilder();
            for (int i = 0; i < 10; i++) 
            {
                ssb.Append($"{_arrCell[150 + i]} ");
                if (_arrCell[150 + i] == 0)
                {
                    tmp.len++;
                }
                else
                {
                    if (tmp.len > 0)
                    {
                        tmp.ofs += tmp.len - 1;
                        freeCells.Add(tmp);
                        tmp = new FreeCell(0, i + 1);
                    }
                    else
                    {
                        tmp.ofs++;
                    }
                }
            }
            print(ssb.ToString());
            if (tmp.len > 0)
            {
                tmp.ofs += tmp.len - 1;
                freeCells.Add(tmp);
            }

            if (freeCells.Count > 0)
            {   
                /*StringBuilder sb = new StringBuilder();
                for (int j = 0; j < freeCells.Count; j++)  { sb.Append($"{j}) {freeCells[j]}  "); }
                print(sb.ToString());*/

                if (freeCells[0].len == 10)
                {
                    int numTail = Random.Range(0, 5);
                    _listTails.Add(spawnTails.SpawnTail(numTail, 0, 1));
                    for (int i = 0; i < numTail + 1; i++) _arrCell[155 - i] = 1;
                }
                else
                {
                    int num = Random.Range(0, freeCells.Count);
                    int maxLen = freeCells[num].len, minLen = 0;
                    if (maxLen > 1) minLen = 1;
                    if (maxLen > 4) maxLen = 5;
                    int numTail = Random.Range(minLen, maxLen);
                    int ofs = reOfs[freeCells[num].ofs];
                    print($"num={num} maxLen={maxLen} numTail={numTail} tmpofs={freeCells[num].ofs} reOfs={ofs}");
                    _listTails.Add(spawnTails.SpawnTail(numTail, ofs, 1));
                    for (int i = 0; i < numTail + 1; i++) _arrCell[(155 + ofs) - i] = 1;
                }
            }
        }        
    }

    private void ShiftTails()
    {
        int i, cntLoss = 0;
        for (i = 0; i < 150; i++)
        {
            _arrCell[i] = _arrCell[i + 10];
        }
        for (i = 0; i < 10; i++) _arrCell[150 + i] = 0;
        Vector3 pos;
        for (i = 0; i < _listTails.Count; i++)
        {
            pos = _listTails[i].transform.position;
            pos.y -= 1f;
            _listTails[i].transform.position = pos;
        }
        for (i = 0; i < 10; i++)
        {
            if (_arrCell[i] > 0) cntLoss++;
        }
        if ( cntLoss > 0 )
        {   //  поражение
            isPause = true;
            ui_Control.ViewLossPanel();
        }
    }

    public void TranslateTarget(Vector3 pos)
    {
        //print($"target={pos}");
        SpawnBall(pos);
    }

    public void ErrorBall(GameObject errBall)
    {
        Destroy(errBall);
    }

    public void DestroyTail(GameObject tail)
    {
        Vector3 pos = tail.transform.position;
        int numTail = tail.GetComponent<TailControl>().TypeTail;
        int x = Mathf.RoundToInt(pos.x - 0.5f);
        int y = Mathf.RoundToInt(7.5f - pos.y);
        //print($"pos={pos} numTail={numTail} x={x}");
        for (int i = 0; i < numTail; i++)
        {
            _arrCell[159 + (x - 4) - i - 10 * y] = 0;
        }
        if (_listTails.Contains(tail)) _listTails.Remove(tail);
        Destroy(tail);
    }
}

public struct FreeCell
{
    public FreeCell(int l, int o)
    {
        len = l;
        ofs = o;
    }

    public int len;
    public int ofs;

    public override string ToString()
    {
        return $"<len={len}, ofs={ofs}>";
    }
}
