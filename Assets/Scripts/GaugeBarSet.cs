using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GaugeBarSet : MonoBehaviour
{
    public Image GaugeBarImage;  //게이지 이미지
    static float Gauge = 0;  //게이지 양 저장 변수
    string grade;  //등급
    public GameObject[] MinamObjects;  // 레벨에 따른 미남이 오브젝트 배열
    static int currentLevel = 0;  // 현재 레벨
    public GameObject EvolutionPanel; //진화 알림 패널
    

    private void Start() {
        EvolutionPanel.SetActive(false);
        PlayerPrefs.SetInt("레벨",currentLevel);
        MinamObjects[currentLevel].SetActive(true);  //현재 레벨 미남이가 보이게
    }

    private void Update() {
        //게임 종료가 되면 값을 받아와 게이지바를 채움
        if(PlayerPrefs.GetInt("운동하기")==1){
            grade = PlayerPrefs.GetString("등급");
            GaugeFill(grade);
            PlayerPrefs.SetString("운동하기버튼","비활성화");
            PlayerPrefs.SetInt("운동하기",3);  //게이지바가 계속 올라가는 걸 막음
            CheckLevelUp();  //레벨업 체크
        }
        if(PlayerPrefs.GetInt("식단하기")==1){
            grade = PlayerPrefs.GetString("등급");
            GaugeFill(grade);
            PlayerPrefs.SetString("식단하기버튼","비활성화");
            PlayerPrefs.SetInt("식단하기",3);  //게이지바가 계속 올라가는 걸 막음
            CheckLevelUp();  //레벨업 체크
        }
        if(PlayerPrefs.GetInt("공부하기")==1){
            grade = PlayerPrefs.GetString("등급");
            GaugeFill(grade);
            PlayerPrefs.SetString("공부하기버튼","비활성화");
            PlayerPrefs.SetInt("공부하기",3);  //게이지바가 계속 올라가는 걸 막음
            CheckLevelUp();  //레벨업 체크
        }
    }
    public void GaugeFill(string grade)  //등급별로 게이지바 채우기
    {
        float amount = 0;
        switch(grade){
            case "A" :amount = 0.5f; break;
            case "B" : amount = 0.3f; break;
            case "C" : amount = 0.1f; break;
        }
        Gauge += amount;
        Debug.Log(Gauge);
        GaugeBarImage.fillAmount = Gauge; 
    }
    private void LevelUp()  // 레벨업
    {
        Debug.Log("레벨업");
        currentLevel++;
        PlayerPrefs.SetInt("레벨",currentLevel);
        MinamObjects[currentLevel-1].SetActive(false);  // 이전 레벨 캐릭터 비활성화
        MinamObjects[currentLevel].SetActive(true);  // 현재 레벨 캐릭터 활성화
        Gauge = 0;  //게이지 초기화
        GaugeBarImage.fillAmount = Gauge;  //게이지바 초기화
        //게임여부 다 초기화 해줌 
        PlayerPrefs.SetInt("운동하기",0);
        PlayerPrefs.SetInt("식단하기",0);
        PlayerPrefs.SetInt("공부하기",0);
        EvolutionPanel.SetActive(true);
    }
    
    private void CheckLevelUp()  // 레벨업 체크
    {
        //게이지바를 다 채우면
        if (GaugeBarImage.fillAmount >= 1) 
        {
            LevelUp();
        }
    }
}
