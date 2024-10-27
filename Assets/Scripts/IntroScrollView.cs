using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScrollView : MonoBehaviour
{
    [SerializeField] private float delay = 25f;

    void Start()
    {
        StartCoroutine(IeSwitchScene(delay));
    }

    private IEnumerator IeSwitchScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(Constant.MENU_SCENE);
    }
}