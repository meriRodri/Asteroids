using UnityEngine;

namespace Code.Managers
{
    public class ScreenManager : MonoBehaviour
    {
        private void Start()
        {
            Vector2 bounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            General.MaxX = bounds.x;
            General.MaxY = bounds.y;
        }
    }
}
