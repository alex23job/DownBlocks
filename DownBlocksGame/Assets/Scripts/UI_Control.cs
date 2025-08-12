using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Control : MonoBehaviour
{
    [SerializeField] private GameObject panelLoss;
    [SerializeField] private Text txtScore;
    [SerializeField] private Text txtBlock;
    [SerializeField] private Image curLive;
    [SerializeField] private Text txtCurLive;
    [SerializeField] private Text txtLive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ViewScore(0);
        ViewBlocks(0);
        ViewCurrentLive(100);
        ViewLive(3);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void ViewCurrentLive(int zn)
    {
        curLive.fillAmount = zn / 100.0f;
        txtCurLive.text = zn.ToString();
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
