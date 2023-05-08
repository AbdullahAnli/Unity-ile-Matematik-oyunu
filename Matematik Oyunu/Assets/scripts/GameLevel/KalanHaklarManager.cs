using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class KalanHaklarManager : MonoBehaviour
{
    [SerializeField]
    private GameObject kalanHak1, kalanHak2, kalanHak3;

   
    public void KalanHaklariKontrolEt(int kalanhak)
    {
        switch (kalanhak)
        {
            case 3:
                kalanHak1.SetActive(true);
                kalanHak2.SetActive(true);
                kalanHak3.SetActive(true);
                break;
            case 2:
                kalanHak1.SetActive(true);
                kalanHak2.SetActive(true);
                kalanHak3.SetActive(false);
                break;
            case 1:
                kalanHak1.SetActive(true);
                kalanHak2.SetActive(false);
                kalanHak3.SetActive(false);
                break;
            case 0:
                kalanHak1.SetActive(false);
                kalanHak2.SetActive(false);
                kalanHak3.SetActive(false);
                break;


        }
    }
  
}
