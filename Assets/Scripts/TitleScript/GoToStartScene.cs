using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToStartScene : MonoBehaviour
{
    //  ���� �� ���� ���� �� ���ư��� ��ư�Դϴ�!

    public string sceneName = "StartScene"; // �̵��� �� �̸� (Inspector������ ���� ����)

    public void LoadStartScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}

