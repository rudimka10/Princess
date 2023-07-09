using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class HomeButton : MonoBehaviour
    {
        public void OnClick()
        {
            SceneManager.LoadScene(0);
        }
    }
}
