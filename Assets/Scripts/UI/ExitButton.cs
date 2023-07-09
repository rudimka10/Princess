using UnityEngine;
using UnityEngine.UI;

namespace UI
{
   [RequireComponent(typeof(Button))]
   public class ExitButton : MonoBehaviour
   {
      public void OnClick()
      {
         Application.Quit();
      }
   }
}
