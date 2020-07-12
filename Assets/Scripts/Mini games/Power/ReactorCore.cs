using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ReactorCore : MonoBehaviour {
    
    private bool m_isEmpty;

    [SerializeField] private GameObject coreCompleteSprite;
    [SerializeField] private Collider2D boxCollider;
    
    public bool MIsEmpty {
        get => m_isEmpty;
        set {
            m_isEmpty = value;
            if(value == false) Invoke("Activate",0.35f);
        } 
    }
    
    public void Activate() {
        if (m_isEmpty == false) {
            coreCompleteSprite.SetActive(true);
            //Disable collider
            boxCollider.enabled = false;
        }
    }

    void Update() {
        if (m_isEmpty) {
            coreCompleteSprite.SetActive(false);
            // ENABLE COLLIDER
            boxCollider.enabled = true;
        }
    }
}
