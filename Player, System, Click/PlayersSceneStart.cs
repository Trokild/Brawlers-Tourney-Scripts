using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayersSceneStart : MonoBehaviour
{
    public PlayerStart[] pl;
    public bool sceneLoading = false;
    //public List<AiStart> pl = new List<AiStart>();

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Contenders");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadScene(string s)
    {
        if(!sceneLoading)
        {
            sceneLoading = true;
            StartCoroutine(RealLoadScene(s));
        }
    }

    private IEnumerator RealLoadScene(string s)
    {
        yield return new WaitForSeconds(2f);

        MainSystem.SystemUnitList.Clear();
        MainSystem.PlayerList.Clear();
        MainSystem.Spawner.Clear();
        MainSystem.sys_SelectableUnits.Clear();
        MainSystem.sys_Selected.Clear();
        MainSystem.sort_Selected.Clear();

        SceneManager.LoadScene(s);
    }
}
