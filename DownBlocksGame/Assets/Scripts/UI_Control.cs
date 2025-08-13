using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Control : MonoBehaviour
{
    [SerializeField] private GameObject panelLoss;
    [SerializeField] private GameObject panelWin;
    [SerializeField] private Text txtScore;
    [SerializeField] private Text txtBlock;
    [SerializeField] private Image curLive;
    [SerializeField] private Text txtCurLive;
    [SerializeField] private Text txtLive;
    [SerializeField] private Text txtLevel;
    [SerializeField] private Text txtBalls;
    [SerializeField] private Image curBalls;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ViewScore(0);
        ViewBlocks(0);
        ViewCurrentLive(100);
        ViewLive(3);
        ViewBalls(0);
        ViewLevel(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void ViewWinPanel()
    {
        panelWin.SetActive(true);
    }

    public void ViewLossPanel()
    {
        panelLoss.SetActive(true);
    }

    public void Restart()
    {
        panelLoss.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ViewScore(int score)
    {
        txtScore.text = score.ToString();
    }

    public void ViewBlocks(int block)
    {
        txtBlock.text = block.ToString();
    }

    public void ViewLevel(int level) 
    {
        string lang = "ru";
        string levStr = (lang == "ru") ? "Уровень" : "Level";
        txtLevel.text = $"{levStr} : {level}";
    } 

    public void ViewCurrentLive(int zn, float maxCount = 100.0f)
    {   
        curLive.fillAmount = zn / maxCount;
        txtCurLive.text = zn.ToString();
    }

    public void ViewBalls(int zn, float maxBalls = 1000.0f)
    {       
        curBalls.fillAmount = zn / maxBalls;
        string lang = "ru";
        string balls = (lang == "ru") ? "Шары" : "Balls";
        txtBalls.text = $"{balls} : {zn}";
    }

    public void ViewLive(int zn)
    {
        string lang = "ru";
        if (lang == "ru")
        {
            txtLive.text = $"Попытки : {zn}";
        }
        else
        {
            txtLive.text = $"Attempts : {zn}";
        }
    }
}
