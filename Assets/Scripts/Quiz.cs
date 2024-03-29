using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    public List<QuizandAnswer> QnA;  //질문 리스트
    public GameObject[] options;  //정답을 옵션 배열
    public int currentQuestion;  //질문 할 개수
    public Text QuestionText;  //질문 텍스트
    public Text ScoreText;  //점수 
    public GameObject QuizPanel;  //질문을 띄울 패널
    public GameObject GoPanel;  //게임오버 패널
    public GameObject TutorialPanel; //게임 방법 패널
    public StartBtn StartBtn;  //시작 버튼
    
    
    private int totalQuiz = 0;  //총 질문 개수
    public int score = 0;  //정답 개수
    public bool check; //이미지 정,오답 구분
    public string grade; //등급 나누기
    private int knowledge; //지식 수준
    
    //시작
    private void Start(){
        totalQuiz = QnA.Count;
        GoPanel.SetActive(false);
        makeQuestion();
        StudySounds.instance.StudyBGM(); //BGM 재생
    }

    //게임 시작 버튼 눌렀을 때
    public void GameStart(){
        StartBtn.StartButton();
    }

    //게임 종료
    public void GameOver(){
        QuizPanel.SetActive(false);
        GoPanel.SetActive(true);
        ScoreText.text = "맞춘 개수 : "+score;
        PlayerPrefs.SetInt("공부하기",1);  //공부하기 게임 했는지 안했는지
        ScoreGrade(); //등급 구하기
        PlayerPrefs.SetString("등급",grade); //게이지로 등급 보내주기
        PlayerPrefs.SetString("게임실행여부","게임종료");  //게임종료했는지 저장
    }

    private string ScoreGrade(){
        //맞춘 개수 별 등급나누기
        if(score == totalQuiz){
            grade = "A";
            knowledge = 20;
        }
        else if(score >= (totalQuiz/2)){
            grade = "B";
            knowledge = 15;
        }
        else if(score == 0){
            grade = "C";
            knowledge = 10;
        }
        Stats.Knowledge += knowledge;
        return grade;
    }

    //정답
    public void correct(){
        score+=1; 
        QnA.RemoveAt(currentQuestion);
        makeQuestion();
        StudySounds.instance.Correct(); //정답 시 효과음
        check = true; //정답
    }
    
    //오답
    public void wrong(){
        if (currentQuestion >= 0 && currentQuestion < QnA.Count)
        {
            QnA.RemoveAt(currentQuestion);
            StudySounds.instance.Wrong(); //오답 시 효과음
            check = false; //오답
        }
        makeQuestion();
    }

    //질문
    void makeQuestion(){
        if(QnA.Count > 0){
            currentQuestion = Random.Range(0,QnA.Count);
            QuestionText.text = QnA[currentQuestion].Quiz; 
            SetAnswers();
        }
        else{
            Debug.Log("다 풀었습니다.");
            GameOver();
        }
    }

    //정답 구분
    void SetAnswers(){
        for(int i = 0; i<options.Length; i++){
            options[i].GetComponent<Answer>().isCorrect=false;
            options[i].transform.GetChild(0).GetComponent<Text>().text=QnA[currentQuestion].Answers[i];

            if(QnA[currentQuestion].CorrectAnswer==i+1){
                options[i].GetComponent<Answer>().isCorrect=true;
            }
        }
    }
} 
