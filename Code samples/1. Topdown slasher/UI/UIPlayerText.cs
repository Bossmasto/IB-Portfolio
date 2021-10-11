using System.Collections;
using TMPro;
using UnityEngine;

public class UIPlayerText : MonoBehaviour
{
    TMP_Text _tmText;

    void Awake()
    {
        _tmText = GetComponent<TMP_Text>();
    }

    public void HandlePlayerInitialized()
    {
        _tmText?.SetText("Player Joined");
        StartCoroutine(ClearTextAfterDelay());
    }

    IEnumerator ClearTextAfterDelay()
    {
        yield return new WaitForSeconds(2);
        _tmText?.SetText(string.Empty);
    }
}
