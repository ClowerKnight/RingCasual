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
        //Clamp:ilk olarak neyi tanımladıgımız,en düşüğü,en fazlası canı -1 ile 10 arasında tutar.
        health = Mathf.Clamp(health,-1, 10.0f);
        
        if (health>=0)//can 0 dan daha düşükse azalmaya devam etmeycektir.
        {
            health -= Time.deltaTime;
            UIManager.ui_m.SetPlayerHealth(health);
        }
     
    }

    public void IncreaseHealth(float value)//point scripti üzerindne erişeceğiz.collect pointten
    {
        health += value;
    }

    #region Functions
    private void Death()//öldükten sonra kamera takibininde sonlanmasını yapıcaz.
        //cameramız main camera oldugu için kamerayı ayrı bir tranımlamaya gerek yok.
    {   //stop kamera controller
        if (Camera.main !=null)//kamera objesine erişemediği durumlarda hata vermesi yerine es geçmesini söylüyoruz.
        {
            Camera.main.GetComponent<CameraController>().enabled = false;
        }

        //Open GamrOverUI
        UIManager.ui_m.OpenGameOverUI();


        //PlayerAlive Boolean
        GameManager.gm.isPlayerAlive = false;
        //death soun
        Camera.main.GetComponent<AudioSource>().PlayOneShot(death_sound, 0.4F);

        //save high score
        //PlayerPrefs:get ler bir player prefin değerini almamıza sağlıyor/setlerde ayaarlamamızı sağlıyor.
        if (GameManager.gm.distance>PlayerPrefs.GetFloat("HighScore"))
        {
            PlayerPrefs.SetFloat("HighScore", GameManager.gm.distance);

        }
        //set high score text
        UIManager.ui_m.SetHighScoreText();
        

       


        //destroy player gameobject
        Destroy(this.gameObject);//this.gameobject yüzüğü temsil ediyor.ve yüzğüüümüz çarptıgı anda oyun durur ve yüzük ekrandan yok olur.

    }

    

    private void ChangeRingRadius(float cyl_radius)//cyl_Radiusları buraya atmamızdakı neden tanımlı olmayıp hata vermeleri hem burda tanımlayıp hem de yukarıda ChangeRingRadius(cyl_radius);
        //da tanımlama yapmalıyız ki sıkıntı çıkmasın.
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase==TouchPhase.Began)//ekrana dokundugumuzda 1 defa çalılacak
            {
                //PLAY SOUND EFFECT 
                Camera.main.GetComponent<AudioSource>().PlayOneShot(click_sound, 0.01f);
            }
            //CHANGE RING RADIUS
            if (touch.phase==TouchPhase.Stationary)//ekrana dokundugumuz müddet boyunca calısacak.
            {
                //localScale:BİR OBJENIN SCALE DEĞERİNİ DEĞİŞTİRMEK İSTEDİĞİMİZ ZAMAN KULLANDIGIMIZ KOD.
                //transform.localScale = default_size;//ekrana tıkladııgmız zaman playeriimizın boyutu verdiğimiz değere dönüşür.
                /*Physics.CheckSphere FONKSIYONU BELIRLEDIGIMIZ KONUM VE ÇAPTAKI (1F) HAYALİ OLARAK BİR KÜRE OLUSTURUR(CHECHSPHERE DEDİĞİMİZ İÇİZ)
                VE OYUN KONTROL EDER BU KAPSUL HERHANGİ BİR COLLİDER'A DEĞİYOR MU DİYE YÜZÜĞÜN MERKEZ NOKTASINDAN OLUSTURUR BU KÜREYİ.
                ve olusturudumuuz CYLINDER_LAYER İLE SADECE SİLİNDİRLER İLE TEMASA GEÇİP DİĞERLERİ COLLİDERLER İLE TEMASA GEÇMESİNİ İSTEMEDİĞİMİZ İÇİN ONU SCRİPT(INSPECTOR) TARAFINDAN AYARLARIZ.
                CYLINDER OLARAK TANIMLADIGIMIZ LAYERLARI PREFABLS OLARAK YAPTIGIMIZ CYLINDERLERDİR ONLARA TANIMLARIZ.
                */
                //bool solid =Physics.CheckSphere(transform.position, 1f,cylinder_layer);//physics bool yapıda oldugu için bool'a tanımladık.
                // Physics.OverlapSphere(transform.position, 1f, cylinder_layer);//OverlapSphere:BİR DİZİDİR.İÇİNDE BİR ŞEY OLUP OLMADIGINA BAKMAZ DOGRUDAN İÇİNDEKİ OBJENIN ÖZELLİKLERİNİ BİZE VERİR.

                //her tıkladıgımız zaman bize onun konumunu verir yuvarlayarak.

                //Vector3 cyl_vector = cyl.localScale;//SİLİNDİRİN ÖLÇĞLERİNİ VEC3 OLARKA KAYIT EDİYORUIZ



                Vector3 target_vector = new Vector3(default_size.x, cyl_radius, cyl_radius);
                transform.localScale = Vector3.Slerp(transform.localScale, target_vector, 0.4f);//İLK OLARAK YÜZÜĞÜMÜZÜN BOYUTUNU  transform.localScale İLE TEMSİL EDİYORUZ.BU YÜZÜĞÜN BOYUTUNU BİR EŞİTLEME YAPIYORUZ.
                                                                                                  //Vector3.Slerp: 3 PARAMETRESİ VARDIR.(BİRİNCİYÜZÜĞÜNBÜYÜKLÜĞÜ,YÜZÜĞÜN OLMASINI İSTEDİĞİMİZBÜYÜKLÜĞÜ,KAÇSANIYEİÇERİSİNDE A VEKTORUNDEN B VECTORUNE GEÇİŞ OLACAGIDIR.)
                                                                                                  //KENDİ BÜYÜKLÜĞÜNDEN İSTEDİĞİMİZ BÜYÜKLÜĞE 0.125SN'DE GEÇİŞ YAPAR.
                                                                                                  //new Vector3(default_size.x, cyl_radius,cyl_radius);//SİLİNDİR VEKTÖRÜNÜ X'İ EŞİTLEDİK.TEK YÖNLÜ BİR VEKTOR KULLANACAGIMIZ İÇİN VEC3 KULLANMAMIZA GEREK YOK DİREKT FLOATTAN
                                                                                                  //TEK YÖN KULLANACAGIMIZ İÇİN İŞİMİZİ HALLEDERİZ.


            }
            
        }
        else
        {
            transform.localScale = Vector3.Slerp(transform.localScale, default_size, 0.275f);//MOUSTEN ELIMIZI ÇEKTİĞİMİZ ANDA KENDI ASIL BOYUTUNE DÖNMESİNİ YAPMA.
        }



    }

    #endregion


}
