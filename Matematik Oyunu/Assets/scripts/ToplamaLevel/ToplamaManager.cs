using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class ToplamaManager : MonoBehaviour
{

    [SerializeField]
    private GameObject KarePrefab;

    [SerializeField]
    private Transform KarelerPaneli;

    private GameObject[] KarelerDizisi = new GameObject[20];

    [SerializeField]
    private GameObject soruPaneli;

    [SerializeField]
    private Text soruText;

    [SerializeField]
    private Sprite[] kareSprites;

    [SerializeField]
    private GameObject sonucPaneli;

    List<int> ToplamaDegerleriListesi = new List<int>();

    int sayi1, sayi2,kacincisoru;
    int butondegeri;
    int dogrusonuc;

    bool butonabasildimi;
    int kalanhak;
    string sorununZorlukDerecesi;


    KalanHaklarManager kalanHaklarManager;
    PuanManager puanTopManager;

 

    GameObject gecerlikare;

    [SerializeField]
     AudioSource audioSource;

    public AudioClip dogruses, yanlisses, bitissesi;


    private void Awake()

    {
        kalanhak = 3;

        audioSource = GetComponent<AudioSource>();

        sonucPaneli.GetComponent<RectTransform>().localScale = Vector3.zero;

        kalanHaklarManager = Object.FindObjectOfType<KalanHaklarManager>();
        puanTopManager = Object.FindObjectOfType<PuanManager>();
       

        kalanHaklarManager.KalanHaklariKontrolEt(kalanhak);
    }


    // Start is called before the first frame update
    void Start()
    {
        butonabasildimi = false;
        soruPaneli.GetComponent<RectTransform>().localScale = Vector3.zero;

        kareleriolustur();
    }


    public void kareleriolustur()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject Kare = Instantiate(KarePrefab, KarelerPaneli);
            Kare.transform.GetChild(1).GetComponent<Image>().sprite = kareSprites[0];
            Kare.transform.GetComponent<Button>().onClick.AddListener(() => ButonaBasildi());
            KarelerDizisi[i] = Kare;
        }
        ToplamaDegerleriniTetxteYazdir();
        StartCoroutine(DoFadeRoutiune());
        Invoke("soruPaneliniAc", 2f);
    }
    void ButonaBasildi()
    {
        if (butonabasildimi == true)
        {
            butondegeri = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text);

            gecerlikare = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

            SonucuKontrolEt();
        }
    }
    void SonucuKontrolEt()
    {
        if (butondegeri == dogrusonuc)
        {
            audioSource.PlayOneShot(dogruses);
            gecerlikare.transform.GetChild(1).GetComponent<Image>().enabled = true;
            gecerlikare.transform.GetChild(0).GetComponent<Text>().text = "";

            gecerlikare.transform.GetComponent<Button>().interactable = false;

            puanTopManager.PuaniArttir(sorununZorlukDerecesi);
            ToplamaDegerleriListesi.RemoveAt(kacincisoru);

            if (ToplamaDegerleriListesi.Count > 0)
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
            audioSource.PlayOneShot(yanlisses);
            kalanhak--;
            kalanHaklarManager.KalanHaklariKontrolEt(kalanhak);
        }

        if (kalanhak <= 0)
        {
            OyunBitti();
        }
    }
    void OyunBitti()
    {
        butonabasildimi = false;
        sonucPaneli.GetComponent<RectTransform>().DOScale(1, 0.3f).SetEase(Ease.OutBack);
       
    }
    IEnumerator DoFadeRoutiune()
    {
        foreach (var Kare in KarelerDizisi)
        {
            Kare.GetComponent<CanvasGroup>().DOFade(1, 0.1f);

            yield return new WaitForSeconds(0.1f);
        }
    }

    void ToplamaDegerleriniTetxteYazdir()
    {
        foreach (var Kare in KarelerDizisi)
        {
            int rasgeleDeger = Random.Range(15, 30);
            ToplamaDegerleriListesi.Add(rasgeleDeger);
            Kare.transform.GetChild(0).GetComponent<Text>().text = rasgeleDeger.ToString();
        }
    }

    void soruPaneliniAc()
    {
        SoruyuSor();
        butonabasildimi = true;
        soruPaneli.GetComponent<RectTransform>().DOScale(1, 0.3F).SetEase(Ease.OutBack);
    }
    void SoruyuSor()
    {
        sayi2 = Random.Range(2, 15);
        kacincisoru = Random.Range(0, ToplamaDegerleriListesi.Count);
        sayi1 = ToplamaDegerleriListesi[kacincisoru]  -  sayi2;
        dogrusonuc = sayi1 + sayi2;

        if (dogrusonuc<=20)
        {
            sorununZorlukDerecesi = "kolay";
        }
        else if (dogrusonuc >20 && dogrusonuc <= 25)
        {
            sorununZorlukDerecesi = "orta";
        }
        else
        {
            sorununZorlukDerecesi = "zor";
        }

        soruText.text = sayi1.ToString() + "  +  " + sayi2.ToString();
    }


}
