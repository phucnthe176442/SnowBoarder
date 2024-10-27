using UnityEngine;
using UnityEngine.SceneManagement;

public class Skip : MonoBehaviour
{
    public void SkipScene(){
        SceneManager.LoadScene(2);
    }
}
