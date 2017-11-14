using UnityEngine;
using UnityEngine.UI;

namespace AR
{
    public class activeButtons : MonoBehaviour
    {
        public Image buttons;
        public Image joystick;
        public Image jandb;

        public Text musicText;
        public Text effectsText;

        private Color check;
        private Color uncheck;

        private string musicTextON;
        private string musicTextOFF;

        private string effectsTextON;
        private string effectsTextOFF;

        // Use this for initialization
        void Start()
        {
            check = new Color(0, 255, 0);
            uncheck = new Color(255, 255, 255);
            musicTextON = "MUSIC ON";
            musicTextOFF = "MUSIC OFF";

            effectsTextON = "EFFECTS ON";
            effectsTextOFF = "EFFECTS OFF";
    }

        public void setCheckButton(CONTROL_TYPE control)
        {
            switch(control)
            {
                case CONTROL_TYPE.buttons:
                    buttons.color = check;
                    joystick.color = uncheck;
                    jandb.color = uncheck;
                    break;

                case CONTROL_TYPE.joystick:
                    buttons.color = uncheck;
                    joystick.color = check;
                    jandb.color = uncheck;
                    break;

                case CONTROL_TYPE.JandB:
                    buttons.color = uncheck;
                    joystick.color = uncheck;
                    jandb.color = check;
                    break;
            }
        }

        public void setMusicButton(SOUND_TYPE musicType)
        {
            switch (musicType)
            {
                case SOUND_TYPE.ON:
                    musicText.text = musicTextON;
                    break;

                case SOUND_TYPE.OFF:
                    musicText.text = musicTextOFF;
                    break;
            }
        }

        public void setEffectsButton(SOUND_TYPE effectsType)
        {
            switch (effectsType)
            {
                case SOUND_TYPE.ON:
                    effectsText.text = effectsTextON;
                    break;

                case SOUND_TYPE.OFF:
                    effectsText.text = effectsTextOFF;
                    break;
            }
        }
    }
}
