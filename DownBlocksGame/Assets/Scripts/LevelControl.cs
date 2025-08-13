using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Text;
using static UnityEditor.PlayerSettings;

public class LevelControl : MonoBehaviour
{
    [SerializeField] private UI_Control ui_Control;
    [SerializeField] private SpawnBallsControl spawnBallsControl;
    [SerializeField] private SpawnTails spawnTails;
    [SerializeField] private ParticleSystem effectBum;

    private bool isPause = true;
    private bool isBonus = true;
    private int _countLive = 3;
    private int _currentLive = 100;
    private int _countSnow = 0;
    private int _countBalls = 0;
    private int[] _arrCell;
    private List<GameObject> _listTails = new List<GameObject>();
    private int[] reOfs = new int[10] { -5, -4, -3, -2, -1, 0, 1, 2, 3, 4};
    private float _speedTails = 1f;
    private GameObject _movingTail = null;
    private int _score = 0;
    private int _blocks = 0;
    private float timer = 2f;

    private List<LevelInfo> _levels = new List<LevelInfo>();
    private LevelInfo _currentLevel = new LevelInfo(1, 4, 100, 10, 1);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateLevels();
        BeginGame(); 
        spawnTails.SetLevelControl(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPause == false)
        {
            if (timer > 0) timer -= Time.deltaTime;
            else
            {
                timer = 2f;
                if (_movingTail == null) SpawnMovingTail();
            }
        }
    }

    private void CreateLevels()
    {
        _levels.Clear();
        _levels.Add(new LevelInfo(1, 4, 100, 20, 1));
        _levels.Add(new LevelInfo(2, 4, 100, 15, 2));
        _levels.Add(new LevelInfo(3, 4, 200, 30, 2));
        _levels.Add(new LevelInfo(4, 5, 200, 20, 2));
        _levels.Add(new LevelInfo(5, 5, 200, 20, 3));
        _levels.Add(new LevelInfo(6, 5, 300, 30, 3));
        _levels.Add(new LevelInfo(7, 6, 300, 30, 3));
        _levels.Add(new LevelInfo(8, 6, 400, 40, 3));
        _levels.Add(new LevelInfo(9, 6, 500, 50, 3));
        _levels.Add(new LevelInfo(10, 7, 500, 50, 3));
        _levels.Add(new LevelInfo(11, 7, 600, 60, 3));
        _levels.Add(new LevelInfo(12, 7, 700, 70, 3));
        _levels.Add(new LevelInfo(13, 8, 700, 70, 3));
        _levels.Add(new LevelInfo(14, 8, 800, 80, 3));
        _levels.Add(new LevelInfo(15, 8, 900, 80, 3));
        _levels.Add(new LevelInfo(16, 8, 900, 70, 3));
        _levels.Add(new LevelInfo(17, 8, 1000, 70, 3));
        _levels.Add(new LevelInfo(18, 8, 1000, 60, 3));
        _levels.Add(new LevelInfo(19, 8, 1000, 50, 3));
        _levels.Add(new LevelInfo(20, 8, 1000, 40, 3));
    }

    public void SetPause(bool pause)
    {
        isPause = pause;
    }

    private void BeginGame()
    {
        SetPause(false);
        int gmLevel = GameManager.Instance.currentPlayer.currentLevel;
        for (int j = 0; j < _levels.Count; j++)
        {
            if (_levels[j].Level == gmLevel)
            {
                _currentLevel = _levels[j];
                break;
            }
        }
        ui_Control.ViewLevel(_currentLevel.Level);
        _currentLive = _currentLevel.CountBallsInAtempt;
        ui_Control.ViewCurrentLive(_currentLevel.CountBallsInAtempt, (float)_currentLevel.CountBallsInAtempt);
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
        if (_countBalls >= _currentLevel.CountBalls)
        {   //  end game
            SetPause(true);
            ui_Control.ViewWinPanel();
            GameManager.Instance.currentPlayer.LevelComplete();
            GameManager.Instance.SaveGame();
        }
        _countBalls++;
        ui_Control.ViewBalls(_countBalls, (float)_currentLevel.CountBalls);
        int numColor = Random.Range(0, _currentLevel.MaxCountColors);
        int rndBonus = Random.Range(5, 10);
        if (isBonus && ((_countBalls % 10) == rndBonus))
        {
            int typeBonus = Random.Range(2, 4);
            print($"rndBonus={rndBonus}  typeBonus={typeBonus}");
            spawnBallsControl.SpawnBall(numColor, target, typeBonus);
            isBonus = false;
        }
        else spawnBallsControl.SpawnBall(numColor, target);
        SpawnTail();
    }

    private void SpawnMovingTail() 
    {
        int num = Random.Range(0, _currentLevel.MaxMovingTail);
        int ofs = Random.Range(1, 9);
        if (num == 2) ofs = Random.Range(2, 8);
        float currentSpeed = _speedTails + (0.1f * (_currentLevel.Level - 1));
        if (num == 0) currentSpeed *= 1.5f;
        if (num == 2) currentSpeed *= 0.5f;
        _movingTail = spawnTails.SpawnMovingTail(num, reOfs[ofs], 1, currentSpeed);
    }
    private void SpawnTail()
    {
        if (_countBalls > 0)
        {
            //_countBalls %= 10;
            if ((_countBalls % 10) == 0)
            {
                isBonus = true;
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
                    //print($"num={num} maxLen={maxLen} numTail={numTail} tmpofs={freeCells[num].ofs} reOfs={ofs}");
                    _listTails.Add(spawnTails.SpawnTail(numTail, ofs, 1));
                    for (int i = 0; i < numTail + 1; i++) _arrCell[(155 + ofs) - i] = 1;
                }
            }
        }        
    }

    private void ShiftTails()
    {
        if (_countSnow > 0)
        {
            _countSnow--;
            return;
        }
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
        ChangeLive(1);
        Destroy(errBall);
    }

    private void ChangeLive(int zn)
    {
        int delta = _currentLive - zn;
        if (delta > 0)
        {
            _currentLive = delta;
        }
        else
        {
            _currentLive = _currentLevel.CountBallsInAtempt - (zn - _currentLive);
            if (_countLive > 0)
            {
                _countLive--;
                ui_Control.ViewLive(_countLive);
            }
            else
            {
                ui_Control.ViewLive(_countLive);
                ui_Control.ViewCurrentLive(_currentLive, _currentLevel.CountBallsInAtempt);
                isPause = true;
                ui_Control.ViewLossPanel();
            }
        }
        ui_Control.ViewCurrentLive(_currentLive, _currentLevel.CountBallsInAtempt);
    }

    public void DestroyTail(GameObject tail, int mult = 1)
    {
        int i, typeTail = tail.GetComponent<TailControl>().TypeTail;
        _score += mult * typeTail;
        ui_Control.ViewScore(_score);
        GameManager.Instance.currentPlayer.currentScore = _score;
        _blocks++;
        ui_Control.ViewBlocks(_blocks);
        if (mult == 2)
        {
            if (_movingTail == null) SpawnMovingTail();
        }
        Vector3 pos = tail.transform.position;
        int numTail = tail.GetComponent<TailControl>().TypeTail;
        int x = Mathf.RoundToInt(pos.x - 0.5f);
        int y = Mathf.RoundToInt(7.5f - pos.y);
        //print($"pos={pos} numTail={numTail} x={x}");
        for (i = 0; i < numTail; i++)
        {
            _arrCell[159 + (x - 4) - i - 10 * y] = 0;
        }
        if (_listTails.Contains(tail)) _listTails.Remove(tail);
        ParticleSystem effect;
        Quaternion qw = Quaternion.Euler(180f, 0, 0);
        for(i = 0; i < typeTail; i++)
        {
            effect = Instantiate(effectBum, pos, qw);
            Destroy(effect.gameObject, 1f);
            pos.x -= 1f;
        }
        Destroy(tail);
    }

    public void ClearThreeLines()
    {
        int i, firstLine = 1, ty;
        for (i = 10; i < _arrCell.Length; i++) 
        {
            if (_arrCell[i] != 0)
            {
                firstLine = i / 10;
                break;
            }
        }
        for (i = _listTails.Count; i > 0; i--) 
        {
            ty = 15 - Mathf.RoundToInt(7.5f - _listTails[i - 1].transform.position.y);
            if ((ty >= firstLine) && (ty < (firstLine + 3)))
            {
                GameObject tail = _listTails[i - 1];
                _listTails.RemoveAt(i - 1);
                Vector3 pos = tail.transform.position;
                int numTail = tail.GetComponent<TailControl>().TypeTail;
                int x = Mathf.RoundToInt(pos.x - 0.5f); 
                int y = Mathf.RoundToInt(7.5f - pos.y);
                ParticleSystem effect;
                Quaternion qw = Quaternion.Euler(180f, 0, 0);
                for (int j = 0; j < numTail; j++)
                {
                    _arrCell[159 + (x - 4) - j - 10 * y] = 0;
                    effect = Instantiate(effectBum, pos, qw);
                    Destroy(effect.gameObject, 1f);
                    pos.x -= 1f;
                }
                Destroy(tail);
            }
        }
    }

    public void ClearMovingTail()
    {
        ChangeLive(10);
        _movingTail = null;
    }

    public void SnowShift(GameObject tail)
    {
        _countSnow = 1;
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

public class LevelInfo
{
    private int level;
    private int maxCountColors;
    private int countBalls;
    private int countBallsInAtempt;
    private int maxMovingTail;

    public int Level { get { return level; } }
    public int CountBalls { get { return countBalls; } }
    public int CountBallsInAtempt { get { return countBallsInAtempt; } }
    public int MaxMovingTail { get { return maxMovingTail; } }

    public int MaxCountColors { get => maxCountColors; }

    public LevelInfo() { }
    public LevelInfo(int lev, int maxColors, int cntBalls, int cntBinAt, int maxMovTail)
    {
        level = lev;
        maxCountColors = maxColors;
        countBalls = cntBalls;
        countBallsInAtempt = cntBinAt;
        maxMovingTail = maxMovTail;
    }
}
