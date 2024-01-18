using UnityEngine;

namespace UI
{
    public class EndGamePopUpView : MonoBehaviour
    {
        private void Start()
        {
            //gameObject.SetActive(false);
        }

        public void ActivateObject()
        {
            gameObject.SetActive(true);
        }
    }
}
