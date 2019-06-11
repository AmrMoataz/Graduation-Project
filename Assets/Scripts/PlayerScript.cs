using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public Text ScoreText;
    public GameObject Canvas;
    public Text FinalScoreText;

    private int _score;
    private Animator _anim;
    private GameObject _playerCanvas;
    private Button _shootingBtn;
    

    private void Start()
    {
        _playerCanvas = GameObject.Find("PlayerCanvas");
        if (_playerCanvas != null)
        {
            Canvas = _playerCanvas;
            ScoreText =  _playerCanvas.GetComponent<FillPlayerCanvasVars>().ScoreText;
            FinalScoreText = _playerCanvas.GetComponent<FillPlayerCanvasVars>().GameOverText;
            _shootingBtn = _playerCanvas.GetComponent<FillPlayerCanvasVars>().ShootingButton;	
           _shootingBtn.onClick.AddListener(GetComponent<RayCastShooting>().Shoot);
        }
        
        
        
        _score = 0;
        Time.timeScale = 1;
        _anim = Canvas.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            StartCoroutine(DeathRoutine());
        }


    }
      
    public void UpdateScore()
    {
        _score += 10;
        ScoreText.text = _score.ToString();
    }

    private IEnumerator DeathRoutine()
    {
        FinalScoreText.text = "Score: " + _score;
        _anim.SetBool("GameOver", true);
        yield return new WaitForSeconds(2.0f);    
        Time.timeScale = 0;
    }

    public void Restart()
    {
       SceneManager.LoadScene("Menu 3D"); 
        //Application.Quit();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
