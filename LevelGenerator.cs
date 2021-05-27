using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    #region SerializeFields
    [Header("Cylinder Attributes(silindir özellikleri demektir.)")]
    [Tooltip("Deafult Cylinder Prefab For Instantiate")]
    [SerializeField]
    private GameObject cylinder;
    [Tooltip("Minumumn radius for cylinder size")]
    [SerializeField]
    private float minRadius;
    [Tooltip("Maximum radius for cylinder size")]
    [SerializeField]
    private float maxRadius;
    [Header("Enemy Cylinder Attiributes")]
    [SerializeField]
    private Color enemy_cylinder;
   
    #endregion

    #region Private Variables
    private GameObject previous_cylinder;//yeni bir silindir olşturmadan önce en son olusturdugumuz silindiri içine atabıleceğimiz değişken yaparız.Oluşturulan silindiri kayıt eder ve bunun konumunu baz alarak hareket eder diğer silindir.
                                         //fazladan kod yazmamızı kısaltırız.
    #endregion



    #region Functions 
    private float FindRadius(float minR,float maxR)
    {

        float radius = Random.Range(minR,maxR);

        if (previous_cylinder!=null)
        {
            while (Mathf.Abs(radius - previous_cylinder.transform.localScale.x) < 0.3f)
            {
                radius = Random.Range(minR, maxR);
            }

            

        }
        return radius;
    }

    public void SpawnCylinder()
    {
       

        float radius = FindRadius(minRadius, maxRadius);
        float height = Random.Range(1f, 4f);

       
        cylinder.transform.localScale = new Vector3(radius, height, radius);
        if (previous_cylinder == null)
        {
            previous_cylinder = Instantiate(cylinder, Vector3.zero, Quaternion.identity);
        }
        else//diğer tüm silindirler.
        {

            float spawnPoint = previous_cylinder.transform.position.z + previous_cylinder.transform.localScale.y + cylinder.transform.localScale.y;
                                                                                                                                                  
            previous_cylinder = Instantiate(cylinder, new Vector3(0, 0, spawnPoint), Quaternion.identity);

            if (Random.value < 0.1f)
                                   
            {
                previous_cylinder.GetComponent<Renderer>().material.color = enemy_cylinder;
                previous_cylinder.tag = "Enemy";

            }

        }
        previous_cylinder.transform.Rotate(90, 0, 0);
                                                     
    }






    #endregion

}
