using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;





public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject startBtn, exitBtn;
    [SerializeField]
    AudioSource audioSource;


    public AudioClip butonsesi;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        FadeOut();
    }

    void FadeOut() {
        startBtn.GetComponent<CanvasGroup>().DOFade(1, 0.8f);
        exitBtn.GetComponent<CanvasGroup>().DOFade(1, 0.8f).SetDelay(0.5f);
        
    }
    public void ExitGame()
    {
        audioSource.PlayOneShot(butonsesi);
        Application.Quit();

    }
    public void StartGameLevel()
    {
        audioSource.PlayOneShot(butonsesi);
        SceneManager.LoadScene("SelectLevel");
    }

    public void PuanTablosu()
    {
        SceneManager.LoadScene("PuanLevel");
    }


}
