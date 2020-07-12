using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ReactorCore : MonoBehaviour {
    
    private bool m_isEmpty;

    [SerializeField] private GameObject coreCompleteSprite;
    public bool MIsEmpty {
        get => m_isEmpty;
        set => m_isEmpty = value;
    }

    void Update() {
        if (m_isEmpty) {
            coreCompleteSprite.SetActive(false);
        }
    }
}
