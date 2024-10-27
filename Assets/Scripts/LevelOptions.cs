using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOptions : MonoBehaviour
{
    public void OnSelectEasy()
    {
        SceneManager.LoadScene(Constant.LEVEL_EASY_SCENE);
    }

    public void OnSelectMedium()
    {
        SceneManager.LoadScene(Constant.LEVEL_MEDIUM_SCENE);
    }

    public void OnSelectHard()
    {
        SceneManager.LoadScene(Constant.LEVEL_HARD_SCENE);
    }
}
