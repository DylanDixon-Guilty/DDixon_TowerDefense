using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set;}

    public Health playerHealth;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
            return;
        }
        playerHealth = GetComponent<Health>();
    }
}
