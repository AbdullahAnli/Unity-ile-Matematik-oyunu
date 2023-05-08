using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SonucManager : MonoBehaviour
{
    

    
    public void OyunaYenidenBasla()
    {
       
        SceneManager.LoadScene("gameLevel");
    }


    public void AnaMenuyeDon()
    {
       
        SceneManager.LoadScene("menuLevel");
    }

    public void CikarmaYenidenBasla()
    {
       
        SceneManager.LoadScene("CikarmaLevel");

       
    }
    public void CarpmaYenidenbasla()
    {
       
        SceneManager.LoadScene("CarpmaLevel");
    }
}
