using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Tree : MonoBehaviour
{
    #region Variables

        // Editor variables

        [SerializeField, Header("tree attributes"), Tooltip("Starting age for this particular tree")]
        private float startingAge;
        [SerializeField, Tooltip("Maximum age for this particular tree")]
        private float maxAge;
        [SerializeField, Tooltip("Height gained per age unit")]
        private float growthFactor;
        [SerializeField, Header("Seed attributes"), Tooltip("GameObject that represents the seed that can be spawned by this tree")]
        private GameObject seedObject;
        [SerializeField, Tooltip("Maximum magnitude for spawning the seed. Minimum is hard set to 1.0")]
        private float maxSpawnMagnitude;
        [SerializeField, Tooltip("How much time should pass before spawning a new seed")]
        private float spawnTimer;

        #endregion

    #region Properties

        public float Age { get; set; }

        #endregion

    #region Methods

        void Grow(float deltaTime)
        {
            Age += Time.deltaTime;
            if (Age > maxAge)
            {
                StartCoroutine("SpawnCycle");
            }
            this.transform.localScale += new Vector3(0.0f, growthFactor *Time.deltaTime, 0.0f);
        }
        void SpawnSeed()
        {
            // Generate random direction in the y plane
            Vector3 randomDirection = new Vector3(Random.Range(-1.0f, 1.0f),0.0f, Random.Range(-1.0f, 1.0f)).normalized;
            float randomMagnitude = Random.Range(1.0f, maxSpawnMagnitude);
            
            // Instantiate a new seed object following the random direction and a random magnitude
            var currentTransform = this.transform;
            Instantiate(seedObject, currentTransform.position + randomDirection * randomMagnitude, currentTransform.rotation);
        }

        IEnumerator SpawnCycle()
        {
            for(;;) 
            {
                SpawnSeed();
                yield return new WaitForSeconds(spawnTimer);
            }
        }

    #endregion


    #region Functions

        // Start is called before the first frame update
        void Start()
        {
            Age = startingAge;

        }

        // Update is called once per frame
        void Update()
        {
            if (Age < maxAge)
            {
                Grow(Time.deltaTime);
            }
        }

    #endregion
}
