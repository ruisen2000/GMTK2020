using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class PulldownStatusButton : MonoBehaviour
{
    public enum Minigame
    {
        Cockpit,
        Oxygen,
        Repair_Bay,
        Power,
        Engines,
    }

    public Minigame Game;

    [SerializeField]
    private Image m_ButtonFill;

    [SerializeField]
    private TextMeshProUGUI m_PercentageText;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateButton(float percentage)
    {
        if (m_PercentageText != null)
        {
            m_PercentageText.text = "" + ((int) percentage) + "%";
        }

        Vector3 newScale = new Vector3(1, (percentage) / 1f, 1);
        m_ButtonFill.transform.localScale = newScale;
    }


}
