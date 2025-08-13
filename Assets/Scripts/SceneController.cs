using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    public UnitManager um => UnitManager.Instance;
    public Turnmanager tm => Turnmanager.Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }
    public void NextStage()
    {
        Debug.Log("다음 스테이지.");

        SceneManager.LoadScene("Scene2", LoadSceneMode.Single);
    }
}
