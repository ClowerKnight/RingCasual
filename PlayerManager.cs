using UnityEngine;

public class PlayerManager : MonoBehaviour

{
    #region Constants
    private const float size_scalel = 0.27f ;
    private const float checker_radius = 0.17f;
    private const float offset = 0.04f;
    #endregion

    #region SerializeField
    [SerializeField]
    private Vector3 default_size = new Vector3(1, 1, 1);
    [SerializeField]
    private LayerMask cylinder_layer;
    [SerializeField]
    private AudioClip click_sound,death_sound;
   
  
    #endregion

    [HideInInspector]
    public bool can_collect=false;


    public float health = 10.0f;


    #region Unity
    private void Update()
    {
        
        Transform cyl = Physics.OverlapSphere(transform.position, checker_radius, cylinder_layer)[0].transform;
     
        float cyl_radius = cyl.localScale.x * size_scalel;


       
        if (health<=0)
        {
            Death();
        }

        if (cyl_radius>transform.localScale.y)
        {
            Death();
        }

        if (cyl.CompareTag("Enemy"))
        {
            if (cyl_radius+offset>transform.localScale.y)
            {
                Death();
            }
        }

        if (cyl_radius + offset > transform.localScale.y) 
                                                         
                                                         
        {
            can_collect = true;
        }
        else
        {
            can_collect = false;
        }



        ChangeRingRadius(cyl_radius);
        HealthCounter();
    }
    #endregion


    private void HealthCounter()
    {
        health = Mathf.Clamp(health,-1, 10.0f);
        
        if (health>=0)
        {
            health -= Time.deltaTime;
            UIManager.ui_m.SetPlayerHealth(health);
        }
     
    }

    public void IncreaseHealth(float value)
    {
        health += value;
    }

    #region Functions
    private void Death()
    { 
        if (Camera.main !=null)
        {
            Camera.main.GetComponent<CameraController>().enabled = false;
        }

        UIManager.ui_m.OpenGameOverUI();


        GameManager.gm.isPlayerAlive = false;
        Camera.main.GetComponent<AudioSource>().PlayOneShot(death_sound, 0.4F);

        if (GameManager.gm.distance>PlayerPrefs.GetFloat("HighScore"))
        {
            PlayerPrefs.SetFloat("HighScore", GameManager.gm.distance);

        }
        UIManager.ui_m.SetHighScoreText();
        

       


        Destroy(this.gameObject);
    }

    

    private void ChangeRingRadius(float cyl_radius)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase==TouchPhase.Began)
            {
             
                Camera.main.GetComponent<AudioSource>().PlayOneShot(click_sound, 0.01f);
            }
         
            if (touch.phase==TouchPhase.Stationary)
            {
           



                Vector3 target_vector = new Vector3(default_size.x, cyl_radius, cyl_radius);
                transform.localScale = Vector3.Slerp(transform.localScale, target_vector, 0.4f);


            }
            
        }
        else
        {
            transform.localScale = Vector3.Slerp(transform.localScale, default_size, 0.275f);
        }



    }

    #endregion


}
