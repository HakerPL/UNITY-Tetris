using UnityEngine;
using UnityEngine.Audio;

namespace AR
{
    public enum SOUND_TYPE { ON = 1, OFF };

    public class optionsGame : MonoBehaviour
    {
        public AudioMixer soundMixer;
        public SOUND_TYPE musicState = SOUND_TYPE.ON;
        public SOUND_TYPE effectsState = SOUND_TYPE.ON;
        public CONTROL_TYPE control = CONTROL_TYPE.buttons;

        public void setControl(CONTROL_TYPE newControl)
        {
            control = newControl;
        }

        public CONTROL_TYPE getControl()
        {
            return control;
        }

        public void setMusicState(SOUND_TYPE newMusicState)
        {
            musicState = newMusicState;
            if (musicState == SOUND_TYPE.ON)
            {
                soundMixer.SetFloat("MusicVol", -10);
            }
            else
            {
                soundMixer.SetFloat("MusicVol", -80);
            }
        }

        public SOUND_TYPE getMusicState()
        {
            return musicState;
        }

        public void setEffectsState(SOUND_TYPE newEffectsState)
        {
            effectsState = newEffectsState;
            if (effectsState == SOUND_TYPE.ON)
            {
                soundMixer.SetFloat("EffectVol", 0);
            }
            else
            {
                soundMixer.SetFloat("EffectVol", -80);
            }
        }

        public SOUND_TYPE getEffectsState()
        {
            return effectsState;
        }
    }
}
