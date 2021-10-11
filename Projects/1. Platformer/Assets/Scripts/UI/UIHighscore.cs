using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHighscore : MonoBehaviour
{
    TMP_Text _text;
    string _key = "Highscore";

    void Start()
    {
        _text = GetComponent<TMP_Text>();
        _text.SetText($"Highscore is {PlayerPrefs.GetInt(_key)}");
    }
}
