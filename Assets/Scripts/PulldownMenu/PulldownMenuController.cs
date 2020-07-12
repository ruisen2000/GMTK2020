using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        InitMinigameButtonDict();
        mAnimator = GetComponentInChildren<Animator>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAllStatusButtons();
        UpdateDistanceTravelled(100f);
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
        Application.Quit();
    }

    public void TogglePulldownShow()
    {
        var newBool = !mAnimator.GetBool("Shown");
        mAnimator.SetBool("Shown", newBool);
    }

    public void UpdateDistanceTravelled(float distance)
    {
        DistanceTravelled.text = distance + "km";
    }
}
