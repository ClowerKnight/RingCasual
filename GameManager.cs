using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public bool isPlayerAlive = true;

    public static GameManager gm;
    private GameObject player;

    [SerializeField]
    private Transform playerStartPoint;

    [SerializeField]
    private CameraController cc;

    [SerializeField]
    private float difficulty;


    public float distance;

    private void Awake()
    {
        gm = this;

        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (!isPlayerAlive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }



        if (player!=null)
        {
            distance = Vector3.Distance(player.transform.position, playerStartPoint.position);
            UIManager.ui_m.SetDistanceValue(distance);
        }


       
        cc.speed += Time.timeSinceLevelLoad / 12500*difficulty;
        cc.speed = Mathf.Clamp(cc.speed, 1.5f, 50);
    }

}
