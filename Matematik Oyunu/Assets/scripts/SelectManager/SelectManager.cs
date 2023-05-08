using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ToplamaBtn, CikarmaBtn;
    [SerializeField]
    private  GameObject CarpBtn, BolBtn;

    public void Tbolumu()
    {
        SceneManager.LoadScene("TBolumManager");
    }

    public void BolmeOyunu()
    {
       
        SceneManager.LoadScene("BolBolumLevel");
    }
   public void toplamaOyunu()
    {
        
        SceneManager.LoadScene("ToplamaLevel");
    }
    public void carpmaOyunu()
    {
        SceneManager.LoadScene("CARPBolumLevel");
    }
    public void cikarmaBolumu()
    {
       
        SceneManager.LoadScene("CBolumLevel");
    }
    public void Geri()
    {
        SceneManager.LoadScene("menuLevel");
    }



}
