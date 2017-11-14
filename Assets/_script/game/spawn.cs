using UnityEngine;

namespace AR
{
    public class spawn : MonoBehaviour
    {
        public GameObject[] blocks;
        public Transform spawnPosition;

        public showNext nextSpawnShow;

        private int nextSpawn;
        private GameObject block;

        private Vector3 spawnRoundPosition;

        private speedBlocks speeds;

        void Start()
        {
            speeds = GetComponent<speedBlocks>();

            spawnRoundPosition = spawnPosition.position;
            spawnRoundPosition.x = Mathf.RoundToInt(spawnRoundPosition.x);
            spawnRoundPosition.y = Mathf.RoundToInt(spawnRoundPosition.y);

            randomNextSpawn();
            GameObject firstBlock = (GameObject)Instantiate(blocks[nextSpawn], spawnRoundPosition, spawnPosition.rotation);
            firstBlock.transform.parent = gameObject.transform;
            firstBlock.SendMessage("moveSpeed", speeds);
            firstBlock.SendMessage("rotationSpeed", speeds.speedRotation);

            randomNextSpawn();
            nextSpawnShow.spawnNext(nextSpawn);
        }

        public void spawnNextBlock()
        {
            block = nextSpawnShow.getNextBlock();
            block.transform.position = spawnRoundPosition;
            block.transform.rotation = spawnPosition.rotation;
            block.transform.parent = gameObject.transform;
            block.GetComponent<blockMove>().setComponent(true);

            block.SendMessage("moveSpeed", speeds);
            block.SendMessage("rotationSpeed", speeds.speedRotation);

            randomNextSpawn();
            nextSpawnShow.spawnNext(nextSpawn);

        }

        private void randomNextSpawn()
        {
            nextSpawn = Random.Range(0, blocks.Length);
        }
    }
}
