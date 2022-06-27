using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    [SerializeField] int score;
    [SerializeField] int highscore;
    public Color[] template = { new Color32(255, 81, 81, 255), new Color32(255, 129, 82, 255), new Color32(255, 233, 82, 255), new Color32(163, 255, 82, 255), new Color32(82, 207, 255, 255), new Color32(170, 82, 255, 255) };

    private UIController uiController;

    private float time;
    [SerializeField] float timeOfGame;

    [SerializeField] NumberContentController numberContentController;
    [SerializeField] ContentController contentController;

    public List<int> currentArr;
    [SerializeField] int currentUserValue;
    [SerializeField] int leng;

    private int currentMath;
    private bool canChoose;
    enum math
    {
        Summation = 0,
        Subtraction = 1
    }

    // Start is called before the first frame update
    void Start()
    {
        uiController = GetComponent<UIController>();
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        UpdateSlider();

        if(time < 0)
        {
            GameOver();
        }
    }

    public void UpdateSlider()
    {
        uiController.UpdateSlider(time);
    }

    public void SetSlider()
    {
        uiController.SetSlider(timeOfGame);
    }

    public void OnPressHandle(int index, int value)
    {
       if (!canChoose) return;
       if(value == currentArr[currentUserValue])
        {
            numberContentController.ShowItem(index);
            currentUserValue++;
            if(currentUserValue >= 4)
            {
                UpdateScore();
                contentController.ClearContent();
                StartCoroutine(StartNextTurn());
            }
        }
       else
        {
            numberContentController.ShowItem(index);
            GameOver();
        }
    }

    private void UpdateInfo(List<int> value)
    {
        numberContentController.UpdateInfo(value);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        uiController.GameOver();
    }

    public void UpdateScore()
    {
        score++;
        if(highscore <= score)
        {
            highscore = score;
            PlayerPrefs.SetInt("score", highscore);
            uiController.UpdateHighScore(highscore);
        }
        uiController.UpdateScore(score);
    }

    IEnumerator StartNextTurn()
    {
        yield return new WaitForSeconds(0.5f);
        NextTurn();
    }


    public void NextTurn()
    {
        time = 1000;
        canChoose = false;
        currentArr = new List<int>();
        var list = new List<int>() {0,1,2,3,4,5,6,7,8,9};
        var tempList = new List<int>();
        currentUserValue = 0;

        leng = 4;

        numberContentController.Spaw(leng);

        for(int i = 0; i< leng; i++)
        {
            tempList.Clear();
            for(int j = 0; j < list.Count; j++)
            {
                if (list[j] != -1)
                {
                    tempList.Add(j);
                }
            }
            var index = Random.Range(0, tempList.Count);
            currentArr.Add(list[tempList[index]]);
            list[tempList[index]] = -1;
        }
        UpdateInfo(currentArr);

        StartCoroutine(SpawButton());
    }

    IEnumerator SpawButton()
    {
        yield return new WaitForSeconds(3f);
        numberContentController.HideItem();
        yield return new WaitForSeconds(0.4f);
        contentController.SpawButton(currentArr);
        canChoose = true;

        time = timeOfGame;
    }

    public void Reset()
    {
        Time.timeScale = 1;

        time = timeOfGame;
        SetSlider();
        score = 0;
        highscore = PlayerPrefs.GetInt("score");
        uiController.UpdateScore(score);
        uiController.UpdateHighScore(highscore);

        NextTurn();
    }

}
