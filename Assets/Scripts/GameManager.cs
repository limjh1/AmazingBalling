using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class GameManager : MonoBehaviour
{
    public UnityEvent onReset;

    public static GameManager instance;

    public GameObject readyPannel;
    public Text scoreText;
    public Text bestScoreText;
    public Text messageText;

    public bool isRoundActive = false;

    private int score = 0;

    public ShooterRotator shooterRotator;
    public CamFollow cam;

    
    void Awake()
    {
        //싱글톤
        instance = this;
        UpdateUI();
    }

    void Start(){
        StartCoroutine("RoundRoutine");
    }

    public void AddScore(int newScore){
        score += newScore;
        UpdateBestScore();
        UpdateUI();
    }

    void UpdateBestScore(){
        if(GetBestScore() < score)
            PlayerPrefs.SetInt("BestScore",score);
    }

    int GetBestScore(){
        int bestScore = PlayerPrefs.GetInt("BestScore");
        return bestScore;
    }

    void UpdateUI(){
        scoreText.text = "Score: " + score;
        bestScoreText.text = "Best Score: " + GetBestScore();
    }

    public void OnBallDestroy(){
        UpdateUI();
        isRoundActive = false;
    }

    public void Reset(){
        score = 0;
        UpdateUI();

        //라운드를 다시 처음부터 시작
        StartCoroutine("RoundRoutine");

    }

    IEnumerator RoundRoutine(){
        //ready
        onReset.Invoke();

        readyPannel.SetActive(true);
        cam.SetTarget(shooterRotator.transform,CamFollow.State.idle);
        shooterRotator.enabled = false;

        isRoundActive = false;
        messageText.text = "Ready...";

        yield return new WaitForSeconds(3f);

        //play
        isRoundActive = true;
        readyPannel.SetActive(false);
        shooterRotator.enabled = true; //자동으로 OnEnabled 함수도 실행

        cam.SetTarget(shooterRotator.transform,CamFollow.State.Ready);

        while(isRoundActive == true)
        {
            yield return null;
        }

        //end
        readyPannel.SetActive(true);
        shooterRotator.enabled = false;

        messageText.text = "Wait for next stage...";

        yield return new WaitForSeconds(3f);

        Reset();
    }
}
