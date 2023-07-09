using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class StartGameButton : MonoBehaviour
    {
        public void OnClick()
        {
            SceneManager.LoadScene(1);
        }
        
    }
}
