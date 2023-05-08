using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TBolumManager : MonoBehaviour
{
    public void ToplamaOyunu()
    {
        SceneManager.LoadScene("ToplamaLevel");
    }

    public void CikarmaOyunu()
    {
        SceneManager.LoadScene("CikarmaLevel");
    }
    public void CarpmaOyunu()
    {
        SceneManager.LoadScene("CarpmaLevel");
    }
    public void BolmeOyunu()
    {
        SceneManager.LoadScene("gameLevel");
    }

    public void GeriDon()
    {
        SceneManager.LoadScene("SelectLevel");
    }

}
