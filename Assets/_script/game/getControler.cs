using UnityEngine;

namespace AR
{
    public class getControler : MonoBehaviour
    {
        public GameObject Buttons;
        public GameObject Joytsic;
        public GameObject JandB;

        private CONTROL_TYPE currentControl;

        // Use this for initialization
        void Awake()
        {
            optionsGame options = GameObject.FindGameObjectWithTag("Options").GetComponent<optionsGame>();

            switch(options.getControl())
            {
                case CONTROL_TYPE.buttons:
                    Buttons.SetActive(true);
                    currentControl = CONTROL_TYPE.buttons;
                    break;

                case CONTROL_TYPE.joystick:
                    Joytsic.SetActive(true);
                    currentControl = CONTROL_TYPE.joystick;
                    break;

                case CONTROL_TYPE.JandB:
                    JandB.SetActive(true);
                    currentControl = CONTROL_TYPE.JandB;
                    break;

                default:
                    Buttons.SetActive(true);
                    currentControl = CONTROL_TYPE.buttons;
                    break;
            }
        }

        public CONTROL_TYPE getCurrentControl()
        {
            return currentControl;
        }
    }
}
