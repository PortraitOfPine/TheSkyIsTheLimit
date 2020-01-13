using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class Tree : Plant
{
    #region Variables

        // Editor variables

        [SerializeField, Header("tree attributes"), Tooltip("Age at which the tree will stop growing")]
        private float ageMaxHeight;
        [SerializeField, Tooltip("Age at which the tree will die")]
        private float ageDeath;
        [SerializeField, Tooltip("Age at which the tree will begin spawning seeds")]
        private float ageSpawnSeed;
        [SerializeField, Tooltip("GameObject that represents the seed that can be spawned by this tree")]
        private GameObject seedObject;
        [SerializeField, Tooltip("Maximum magnitude for spawning the seed. Minimum is hard set to 1.0")]
        private float maxSpawnMagnitude;
        [SerializeField, Tooltip("How much time should pass before spawning a new seed")]
        private float spawnTimer;
        [SerializeField, Tooltip("Height gained per age unit")]
        protected float growthFactor;

        // Private variables
        private bool _spawning;
    #endregion

    #region Properties

        // If active, allow the SpawnCycle coroutine to continue running
        public bool Spawning { 
           
            get => _spawning;
            set
            {
                _spawning = value;
                if (value == false)
                {
                    StopCoroutine(nameof(SpawnCycle));
                }
            }
        }

    #endregion

    #region Methods

        protected override void Grow(float deltaTime)
        {
            base.Grow(deltaTime);
            
            // Check if object should die
            if (Age > ageDeath)
            {
                Destroy(gameObject);
            }


            // Check if tree should start spawning seeds
            if (!Spawning && Age > ageSpawnSeed)
            {
                Spawning = true;
                StartCoroutine(nameof(SpawnCycle));
            }

            // Check if object should grow in size
            if (Age < ageMaxHeight)
            {
                this.transform.localScale += new Vector3(0.0f, this.growthFactor *deltaTime, 0.0f);    
            }

            
        }

        // Spawn a seed at a random position around the tree object
        void SpawnSeed()
        {
            // Generate random direction in the y plane
            Vector3 randomDirection = new Vector3(Random.Range(-1.0f, 1.0f),0.0f, Random.Range(-1.0f, 1.0f)).normalized;
            float randomMagnitude = Random.Range(1.0f, maxSpawnMagnitude);
            
            // Instantiate a new seed object following the random direction and a random magnitude
            var currentTransform = this.transform;
            Instantiate(seedObject, currentTransform.position + randomDirection * randomMagnitude, currentTransform.rotation);
        }

        // Coroutine that spawns a new seed after every spawn timer interval
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
        protected override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        void Update()
        {
            Grow(Time.deltaTime);
        }

    #endregion
}
