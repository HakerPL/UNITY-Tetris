using UnityEngine;
using UnityEngine.UI;

namespace AR
{
    public class gameManager : MonoBehaviour
    {
        public Text score;
        public Text lvl;

        public int lineToNextLVL = 10;
        public float addSpeed = 0.5f;
        public speedBlocks blockSpeed;

        public float pointForLine = 10f;
        public float bonusForMultipleLinePercent = 0.4f;

        private int currentLVL = 1;
        private int deleteLineCounter = 0;

        public void updateScore(int deleteLine)
        {
            deleteLineCounter += deleteLine;
            int point = int.Parse(score.text);

            point += Mathf.RoundToInt(pointForLine * deleteLine);
            if (deleteLine > 1)
            {
                point += Mathf.RoundToInt((pointForLine * bonusForMultipleLinePercent) * deleteLine);
            }

            score.text = point.ToString();
            if (deleteLineCounter % lineToNextLVL == 0)
            {
                currentLVL++;
                lvl.text = currentLVL.ToString();
                blockSpeed.moveDownAutomatic -= addSpeed;
            }
        }
    }
}
