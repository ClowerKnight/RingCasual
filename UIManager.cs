using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager ui_m;

    [SerializeField]
    private GameObject gameOver_panel;

    [SerializeField]
    private TMP_Text distance_value;

    [SerializeField]
    private RectTransform health_bar;

    [SerializeField]
    public TMP_Text highScore_text;


    public void Awake()
    {
        ui_m = this;
        
    }


    public void SetPlayerHealth(float health)
    {
        health_bar.localScale = new Vector3(health/10, 1.0f, 1.0f);
    }

   
    public void OpenGameOverUI()
    {
        if (gameOver_panel!=null)
        {
            gameOver_panel.SetActive(true);
        }
        
     }
    public void SetDistanceValue(float distance)
    {
        distance_value.text = distance.ToString("f0");
        
    }
 
     public void SetHighScoreText()
    {
        highScore_text.text = PlayerPrefs.GetFloat("HighScore").ToString("F1");
    }


}
