using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject karePrefab;


    [SerializeField]
    private Transform karelerPaneli;
    [SerializeField]
    private Text soruText;

    private GameObject[] karelerDizisi = new GameObject[24];

    [SerializeField]
    private Transform soruPaneli;

    [SerializeField]
    private Sprite[] kareSprites;

    [SerializeField]
    private GameObject sonucPaneli;

    [SerializeField]
    private AudioSource audioSource;

    public AudioClip butonSesi, dogrusesi, bitissesi;



    List<int> bolumDeðerleriListesi = new List<int>();
    int bolenSayi, bolunenSayi,kacinciSoru;
    int dogrusonuc;
    int butonDegeri;
    bool butonabasildimi;


    int kalanHak;

    string ZorlukDerecesi;

    KalanHaklarManager kalanHaklarManager;

    PuanManager puanManager;
    GameObject gecerliKare;

    private void Awake()
    {
        kalanHak = 3;
         
        // game manager içerisindeki audio source a ulaþ 
        audioSource = GetComponent<AudioSource>();

        // sonuc panelini baþlangýçta göstermeyecektir
        sonucPaneli.GetComponent<RectTransform>().localScale = Vector3.zero;

        kalanHaklarManager = Object.FindObjectOfType<KalanHaklarManager>();
        puanManager = Object.FindObjectOfType<PuanManager>();

        
        kalanHaklarManager.KalanHaklariKontrolEt(kalanHak);
    }

    void Start()
    {
        butonabasildimi = false;
        soruPaneli.GetComponent<RectTransform>().localScale = Vector3.zero;


        kareleriOlustur();
    }

    public void kareleriOlustur()
    {
        for (int i = 0; i < 24; i++)
        {
            GameObject kare = Instantiate(karePrefab, karelerPaneli);
            kare.transform.GetChild(1).GetComponent<Image>().sprite = kareSprites[0];
            kare.transform.GetComponent<Button>().onClick.AddListener(() => ButonaBasildi());
            karelerDizisi[i] = kare;
        }
        BolumderleriniTextteYazdir();

        StartCoroutine(DofadeRoutine());
        Invoke("soruPaneliniAc", 2f);
    
    }

    void ButonaBasildi()
    {
        if (butonabasildimi==true)
        {
            // butona týklandýðýnda sesi 1 kereliðine aktif olur 
            audioSource.PlayOneShot(butonSesi);
          
          butonDegeri = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text);

            gecerliKare = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;


            SonucuKontrolEt();
        }
       
        
    }
    void SonucuKontrolEt()
    {
        if (butonDegeri==dogrusonuc)
        {
            audioSource.PlayOneShot(dogrusesi);

            gecerliKare.transform.GetChild(1).GetComponent<Image>().enabled = true;
            gecerliKare.transform.GetChild(0).GetComponent<Text>().text = "";
            gecerliKare.transform.GetComponent<Button>().interactable = false;


            puanManager.PuaniArttir(ZorlukDerecesi);
            bolumDeðerleriListesi.RemoveAt(kacinciSoru);
            if (bolumDeðerleriListesi.Count > 0)
            {
                soruPaneliniAc();
            }
            else
            {
                audioSource.PlayOneShot(bitissesi);
                OyunBitti();
            }


        }
        else
        {
            kalanHak--;
            kalanHaklarManager.KalanHaklariKontrolEt(kalanHak);
        }
        if (kalanHak <= 0)
        {
            OyunBitti();
        }

    }

    void OyunBitti()
    {
        butonabasildimi = false;
        

        // oyun bittiðinde sonuç panelini açar 
        sonucPaneli.GetComponent<RectTransform>().DOScale(1, 0.3f).SetEase(Ease.OutBack);
    }

    IEnumerator DofadeRoutine()
    {
        foreach (var kare in karelerDizisi)
        {
            kare.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
            yield return new WaitForSeconds(0.07f);

        }
    }
     void BolumderleriniTextteYazdir()
    {
        foreach (var kare in karelerDizisi)
        {
            int rasgele = Random.Range(1, 13);
            bolumDeðerleriListesi.Add(rasgele);


            kare.transform.GetChild(0).GetComponent<Text>().text = rasgele.ToString();

        }
        
    }
    void soruPaneliniAc()
    {
        soruyuSor();
        butonabasildimi = true;
        soruPaneli.GetComponent<RectTransform>().DOScale(1, 0.5f);

    }
    void soruyuSor()
    {
        bolenSayi = Random.Range(2, 11);
        kacinciSoru = Random.Range(0, bolumDeðerleriListesi.Count);

        dogrusonuc = bolumDeðerleriListesi[kacinciSoru];

        bolunenSayi = bolenSayi * bolumDeðerleriListesi[kacinciSoru];

        if (bolunenSayi <= 40)
        {
            ZorlukDerecesi = "kolay";
        }
        else if (bolunenSayi > 40  &&  bolunenSayi <= 80)
        {
            ZorlukDerecesi = "orta";
        }
        else
        {
            ZorlukDerecesi = "zor";
        }

        soruText.text = bolunenSayi.ToString() + "  :  " + bolenSayi.ToString();

    }

}
