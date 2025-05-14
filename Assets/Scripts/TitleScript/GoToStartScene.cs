using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToStartScene : MonoBehaviour
{
    //  엔딩 씬 전용 메인 씬 돌아가기 버튼입니다!

    public string sceneName = "StartScene"; // 이동할 씬 이름 (Inspector에서도 설정 가능)

    public void LoadStartScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}

