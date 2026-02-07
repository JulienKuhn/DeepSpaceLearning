using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public SaveFile currentSave;

    private void Awake()
    {
        instance = this;
    }

}
