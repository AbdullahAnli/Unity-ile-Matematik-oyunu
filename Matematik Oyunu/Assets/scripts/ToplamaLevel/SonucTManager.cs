using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SonucTManager : MonoBehaviour
{
    

    public void OyunaYenidenBasla()
    {
        SceneManager.LoadScene("ToplamaLevel");
    }


    public void AnaMenuyeDon()
    {
        SceneManager.LoadScene("menuLevel");
    }
}
