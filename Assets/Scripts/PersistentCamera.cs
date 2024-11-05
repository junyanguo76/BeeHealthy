using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentCamera : MonoBehaviour
{
    public bool EnableAdditiveLoading;
    public SceneAsset[] scenes;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (EnableAdditiveLoading)
        {
            LoadScenes();
        }
    }

    void LoadScenes()
    {
        foreach (var scene in scenes)
        {
            SceneManager.LoadScene(scene.name, LoadSceneMode.Additive);
        }
    }
}
