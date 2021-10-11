using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{

    [SerializeField] string _sceneName;

    float loadDelay = 3f;

    /// <summary>
    /// action to check whether player reached the flag
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player == null) { return; }

        //play flag wave
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Raise");

        //load new level
        StartCoroutine(LoadAfterDelay());
    }

    IEnumerator LoadAfterDelay()
    {
        PlayerPrefs.SetInt(_sceneName + "Unlocked", 1);
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(_sceneName);

    }
}
