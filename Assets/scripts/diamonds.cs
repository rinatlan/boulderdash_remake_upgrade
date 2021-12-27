using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class diamonds : MonoBehaviour
{
    
    TextMeshProUGUI _numEatenDiamonds;
    
    // Start is called before the first frame update
    void Start()
    {
        _numEatenDiamonds = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        int numDiamondsToDisplay = GameManager.GetDiamonds();
        _numEatenDiamonds.text = System.Convert.ToString(numDiamondsToDisplay);
    }
}
