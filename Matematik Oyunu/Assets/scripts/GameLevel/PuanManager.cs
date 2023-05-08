using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PuanManager : MonoBehaviour
{
    public int topPuan;
    public int  puanArtisi;
  
    [SerializeField]
    public Text puanText;


    //[SerializeField]
    //private Text ToplamPuan;
   
    void Start()
    {
        puanText.text = topPuan.ToString();
       
        
    }
    public void PuaniArttir(string zorlukSeviyesi)
    {
        switch (zorlukSeviyesi)
        {
            case "kolay":
                puanArtisi = 5;
                break;
            case "orta":
                puanArtisi = 10;
                break;
            case "zor":
                puanArtisi = 15;
                break;
        }
        topPuan += puanArtisi;
        puanText.text = topPuan.ToString();
      
    }
    
}
