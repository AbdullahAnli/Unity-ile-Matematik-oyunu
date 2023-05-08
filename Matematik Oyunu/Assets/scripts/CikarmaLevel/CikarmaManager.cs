using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class CikarmaManager : MonoBehaviour
{
    [SerializeField]
    private GameObject KarePrefab;

    [SerializeField]
    private Transform KarelerPaneli;

    private GameObject[] KarelerDizisi = new GameObject[18];


    [SerializeField]
    private GameObject soruPaneli;

    [SerializeField]
    private Text soruText;


    [SerializeField]
    private Sprite[] kareSprites;

    [SerializeField]
    private GameObject sonucPaneli;

    [SerializeField]
    AudioSource audioSource ;

   public AudioClip  dogrusesi, yanlissesi, bitissesi;


    List<int> CikarmaDegerleriListesi = new List<int>();

    KalanHaklarManager kalanHaklarManager;
    PuanManager puanCManager;

    GameObject gecerlikare;


    int sayi1 , sayi2;
    int kacincisoru;
    int dogrusonuc;
    int butondegeri;
    bool butonabasildimi;
    int kalanhak;

    string sorununZorlukDerecesi;


    private void Awake()

    {
        kalanhak = 3;

        audioSource = GetComponent<AudioSource>();

        sonucPaneli.GetComponent<RectTransform>().localScale = Vector3.zero;

        kalanHaklarManager = Object.FindObjectOfType<KalanHaklarManager>();
        puanCManager = Object.FindObjectOfType<PuanManager>();

        kalanHaklarManager.KalanHaklariKontrolEt(kalanhak);
    }


    public void kareleriolustur()
    {
        for (int i = 0; i < 18; i++)
        {
            GameObject Kare = Instantiate(KarePrefab, KarelerPaneli);
            Kare.transform.GetChild(1).GetComponent<Image>().sprite = kareSprites[0];
            Kare.transform.GetComponent<Button>().onClick.AddListener(() => ButonaBasildi());
            KarelerDizisi[i] = Kare; 
           
        }
        CikarmaDegerleriniTetxteYazdir();
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

    void OyunBitti()
    {
        butonabasildimi = false;
        sonucPaneli.GetComponent<RectTransform>().DOScale(1, 0.3f).SetEase(Ease.OutBack);


    }
    void SonucuKontrolEt()
    {
        if (butondegeri == dogrusonuc)
        {
            audioSource.PlayOneShot(dogrusesi);
            gecerlikare.transform.GetChild(1).GetComponent<Image>().enabled = true;
            gecerlikare.transform.GetChild(0).GetComponent<Text>().text = "";


            puanCManager.PuaniArttir(sorununZorlukDerecesi);

            gecerlikare.transform.GetComponent<Button>().interactable = false;

            CikarmaDegerleriListesi.RemoveAt(kacincisoru);

            if (CikarmaDegerleriListesi.Count > 0)
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
            audioSource.PlayOneShot(yanlissesi);
            kalanhak--;
            kalanHaklarManager.KalanHaklariKontrolEt(kalanhak);
        }

        if (kalanhak <= 0)
        {
            OyunBitti();
        }
    }



    void Start()
    {
        butonabasildimi = false;
        soruPaneli.GetComponent<RectTransform>().localScale = Vector3.zero;

        kareleriolustur();
    }

    IEnumerator DoFadeRoutiune()
    {
        foreach (var Kare in KarelerDizisi)
        {
            Kare.GetComponent<CanvasGroup>().DOFade(1, 0.1f);

            yield return new WaitForSeconds(0.1f);
        }
    }

    void soruPaneliniAc()
    {
        SoruyuSor();
        butonabasildimi = true;
        soruPaneli.GetComponent<RectTransform>().DOScale(1, 0.3F).SetEase(Ease.OutBack);
    }

    void CikarmaDegerleriniTetxteYazdir()
    {
        foreach (var Kare in KarelerDizisi)
        {
            int rasgeleDeger = Random.Range(1, 15);
            CikarmaDegerleriListesi.Add(rasgeleDeger);
            Kare.transform.GetChild(0).GetComponent<Text>().text = rasgeleDeger.ToString();
        }
    }

    void SoruyuSor()
    {
        sayi2 = Random.Range(2, 15);
        kacincisoru = Random.Range(0, CikarmaDegerleriListesi.Count);
        sayi1 = CikarmaDegerleriListesi[kacincisoru] + sayi2;
        dogrusonuc = sayi1 - sayi2;


        if (dogrusonuc <= 8)
        {
            sorununZorlukDerecesi = "kolay";
        }
        else if (dogrusonuc > 8 && dogrusonuc <= 12)
        {
            sorununZorlukDerecesi = "orta";
        }
        else
        {
            sorununZorlukDerecesi = "zor";
        }

        soruText.text = sayi1.ToString() + "  -  " + sayi2.ToString();


    }

}
