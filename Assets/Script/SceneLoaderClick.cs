using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderClick : MonoBehaviour
{
    // Method ini akan dijalankan dengan parameter namaScene yang akan di-load
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
