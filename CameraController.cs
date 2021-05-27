using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 1f;//inspectorden hızı istediğimiz gibi kontrol etmemizi sağlar.Ve vektorde de işleme sokabilme şansı verir.
    private void Update()//KAMERAMIZI Z EKSENI UZERINDE GİTMESİNİ SAĞLAMAK.
        //update() FONKSIYONU KAÇ FPS ALIYORSAK ONA GÖRE İŞLEM YAPAN BİR FONKSIYONDUR.VE BUNDAN DOLAYI HIZI FARKLI OLAN MAKİNELERDE OYNANMAYA 
        //BASLANDIGI ANDAN İTİBAREN HER KULLANICI FARKLI BİR GÖRÜNTÜ OLUSUR BUNU ENGELLEMEK İÇİN time.deltatime kullanırız.
        //Time.deltaTime 1/fps sayısı demektir.300fps alıyorsak o kadar tekrarlanır 50fps alıyorsak 50 kere tekrarlanır.
    {
        //Z EKSENI ÜZERİNİ DEĞİŞTİRDİĞİMİZ İÇİN  KAMERA LOCAK EKSENI AŞAĞIYA BAKIYOR VE BU KOD İLE START VERDİĞİMİZ ZAMAN KAMERAMIZ AŞAĞIYA DOĞRU İNMEYE BAŞLAR. 
        //transform.Translate(new Vector3(0, 0, 1));.VE TRANSLATE FONSKIYONU LOCAL EKSENDE CALSIIR.
        //Space.World: BU TRANSLATE FONKSYINOMUZUN GLOBAL EKSENDE CALISMAASINI SAĞLAR.
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);//BELİRLENEN OBJENİN İÇERİSİNE GİREREK ONUN VEKTOR DOĞRULTUSUNDA HAREKET ETMESİNİ SAĞLARIZ.
       
    }
}
