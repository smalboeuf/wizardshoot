using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundCounterUI : MonoBehaviour
{
    private TextMeshProUGUI _roundCounterText;
    // Start is called before the first frame update
    void Start()
    {
        _roundCounterText = GetComponent<TextMeshProUGUI>();    
    }

    public void SetRound(int roundNumber)
    {
        _roundCounterText.text = roundNumber.ToString();
    }
}
