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
        string levStr = (lang == "ru") ? "�������" : "Level";
        txtLevel.text = $"{levStr} : {level}";
    } 

    public void ViewCurrentLive(int zn)
    {   
        curLive.fillAmount = zn / 100.0f;
        txtCurLive.text = zn.ToString();
    }

    public void ViewBalls(int zn)
    {       
        curBalls.fillAmount = zn / 1000.0f;
        string lang = "ru";
        string balls = (lang == "ru") ? "����" : "Balls";
        txtBalls.text = $"{balls} : {zn}";
    }

    public void ViewLive(int zn)
    {
        string lang = "ru";
        if (lang == "ru")
        {
            txtLive.text = $"������� : {zn}";
        }
        else
        {
            txtLive.text = $"Attempts : {zn}";
        }
    }
}
