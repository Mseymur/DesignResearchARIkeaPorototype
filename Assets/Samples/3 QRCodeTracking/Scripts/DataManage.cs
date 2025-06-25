using UnityEngine;

public class DataManager : MonoBehaviour
{
    // This makes the DataManager accessible from any script, anywhere.
    public static DataManager Instance;

    // This is the variable that will hold the data between scenes.
    public string itemToShowOnLoad;

    // Awake is called before Start
    void Awake()
    {
        // This is the Singleton pattern. It ensures there is only ever one DataManager.
        if (Instance == null)
        {
            Instance = this;
            // Tell Unity not to destroy this object when we load a new scene.
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If another DataManager already exists, destroy this new one.
            Destroy(gameObject);
        }
    }
}