using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField]
    private Vector3 axis = new Vector3(0, 0, 0);

    [SerializeField]
    private LayerMask player_layer;

    [SerializeField]
    private Color collectable_color, nonCollectable_color;

    [SerializeField]
    private AudioClip pickup_sound;


    private PlayerManager pm;

    private void Awake()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    private void Update()
    {
        transform.Rotate(axis*Time.deltaTime);



       


        if (pm.can_collect)
        {

             axis.y = 360;

            GetComponent<MeshRenderer>().material.color = collectable_color;

          
            bool touchingToPlayer = Physics.CheckSphere(transform.position, 0.2f, player_layer);
            if (touchingToPlayer )
               
            {
                pm.IncreaseHealth(2.0f);
               
                Camera.main.GetComponent<AudioSource>().PlayOneShot(pickup_sound,0.4f);
               
                Destroy(this.gameObject);
            }
        }
        else
        {
            axis.y = 80;
            GetComponent<MeshRenderer>().material.color = collectable_color;
        }



        
    }
}
