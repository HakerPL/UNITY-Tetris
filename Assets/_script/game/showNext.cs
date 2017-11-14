using UnityEngine;

namespace AR
{
    public class showNext : MonoBehaviour
    {
        public GameObject[] blocks;
        public Transform spawnPosition;

        private GameObject nextSpawnOld;

        public void spawnNext(int next)
        {
            nextSpawnOld = (GameObject)Instantiate(blocks[next], spawnPosition.position, spawnPosition.rotation);
            nextSpawnOld.GetComponent<rotateBlock>().startRotation();
            nextSpawnOld.GetComponent<blockMove>().enabled = false;
            nextSpawnOld.GetComponent<rotateBlock>().enabled = false;
            nextSpawnOld.transform.parent = spawnPosition.transform;
        }

        public GameObject getNextBlock()
        {
            return nextSpawnOld;
        }
    }
}
