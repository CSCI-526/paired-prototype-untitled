using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadKitchenScene()
    {
        SceneManager.LoadScene("KitchenScene");
    }
}