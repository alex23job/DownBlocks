using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Control : MonoBehaviour
{
    [SerializeField] private GameObject panelLoss;
    [SerializeField] private Text txtScore;
    [SerializeField] private Text txtBlock;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ViewScore(0);
        ViewBlocks(0);
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
}
