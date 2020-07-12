using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PulldownMenuController : MonoBehaviour
{
    [SerializeField]
    private Text DistanceTravelled; 


    [SerializeField]
    private PulldownStatusButton[] m_MainStatusButtons;
    [SerializeField]
    private PulldownStatusButton[] m_BottomStatusButtons;
    [SerializeField]
    private Minigame[] m_MinigameScripts;


    private Dictionary<Minigame, List<PulldownStatusButton>> GameButtonDict;
    private Animator mAnimator;

    public bool menuDown;

    private void Awake()
    {
        InitMinigameButtonDict();
        if (DataHolder.allData == null)
        {
            DataHolder.allData = new DataHolder();
        }
        mAnimator = GetComponentInChildren<Animator>();
    }


    // Start is called before the first frame update
    void Start()
    {
        menuDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAllStatusButtons();

        if(DataHolder.allData.shipHealth < 0)
        {
            QuitGame();
        }

    }

    private void UpdateAllStatusButtons()
    {
       foreach (var kvp in GameButtonDict)
       {
            foreach(var btn in kvp.Value)
            {
                btn.UpdateButton(kvp.Key.Health);
            }
       }
    }

    private void InitMinigameButtonDict()
    {
        GameButtonDict = new Dictionary<Minigame, List<PulldownStatusButton>>();
        foreach (var minigame in m_MinigameScripts)
        {
            List<PulldownStatusButton> buttonList = new List<PulldownStatusButton>();
            foreach (var mb in m_MainStatusButtons)
            {
                if (mb.Game.ToString().Equals(minigame.name))
                {
                    buttonList.Add(mb);
                }
            }

            foreach (var bb in m_BottomStatusButtons)
            {
                if (bb.Game.ToString().Equals(minigame.name))
                {
                    buttonList.Add(bb);
                }
            }

            GameButtonDict.Add(minigame, buttonList);
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("EndScreen");
    }

    public void TogglePulldownShow()
    {
        var newBool = !mAnimator.GetBool("Shown");
        mAnimator.SetBool("Shown", newBool);
        AudioController.instance.Play("ValveTwist");

        if (menuDown)
        {
            menuDown = false;
        }
        else
        {
            menuDown = true;
        }

    }

    public void UpdateDistanceTravelled(float distance)
    {
        DataHolder.allData.distanceTravelled = distance;
        DistanceTravelled.text = distance.ToString("F2") + "km";
    }
}
