using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    
    TextMeshProUGUI _score;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _score = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        int scoreToDisplay = GameManager.GetPoints();
        _score.text = Convert.ToString(scoreToDisplay);
    }
}
