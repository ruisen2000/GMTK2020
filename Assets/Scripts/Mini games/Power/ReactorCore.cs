using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ReactorCore : MonoBehaviour {
    
    private bool m_isEmpty;

    [SerializeField] private Color _tempEmptyColor = new Color(0f, 0f, 0f, 0f);

    public bool MIsEmpty {
        get => m_isEmpty;
        set => m_isEmpty = value;
    }

    void Update() {
        if (m_isEmpty) {
            GetComponent<SpriteRenderer>().color = _tempEmptyColor;
        }
    }
}
