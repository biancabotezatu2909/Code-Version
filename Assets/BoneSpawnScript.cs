using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneSpawnScript : MonoBehaviour
{
    public GameObject bonePrefab;
    private GameObject currentBone;
    public float spawnRate = 1.5f; 
    private float timer = 0;
    public LogicScript logic;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    void SpawnBoneBetweenPipes(GameObject leftPipe, GameObject rightPipe)
    {
        if (currentBone == null) // Only spawn a new bone if there isn't one already
        {
            // Calculate the initial X and Y position for the bone
            float initialX = (leftPipe.transform.position.x + rightPipe.transform.position.x) / 2;
            float minY = Mathf.Min(leftPipe.transform.position.y / 2 - 15, rightPipe.transform.position.y / 2 - 15);
            float maxY = Mathf.Min(leftPipe.transform.position.y / 2 + 15, rightPipe.transform.position.y / 2 + 15);
            float initialY = Random.Range(minY, maxY);

            // Spawn the bone
            Vector3 spawnPosition = new Vector3(initialX, initialY, 0);
            currentBone = Instantiate(bonePrefab, spawnPosition, Quaternion.identity);
        }
    }

    void UpdateBonePosition(GameObject leftPipe, GameObject rightPipe)
    {
        if (currentBone != null)
        {
            Debug.Log("Bone appeared");
            // Update the X position of the bone to remain between the pipes
            float updatedX = (leftPipe.transform.position.x + rightPipe.transform.position.x) / 2;
            Vector3 bonePosition = currentBone.transform.position;
            currentBone.transform.position = new Vector3(updatedX, bonePosition.y, bonePosition.z);
        }
    }

    bool ArePipesVisible(GameObject leftPipe, GameObject rightPipe)
    {
        Vector3 leftPipeScreenPos = Camera.main.WorldToViewportPoint(leftPipe.transform.position);
        Vector3 rightPipeScreenPos = Camera.main.WorldToViewportPoint(rightPipe.transform.position);

        return (leftPipeScreenPos.x > 0 && leftPipeScreenPos.x < 1) && (rightPipeScreenPos.x > 0 && rightPipeScreenPos.x < 1);
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Find all pipes
        GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");
        
        if (pipes.Length > 1) // Need at least two pipes to form a gap
        {
            
            // Sort pipes by their X position
            List<GameObject> sortedPipes = new List<GameObject>(pipes);
            sortedPipes.Sort((a, b) => a.transform.position.x.CompareTo(b.transform.position.x));

            // Get the latest pair of pipes
            GameObject leftPipe = sortedPipes[sortedPipes.Count - 2];
            GameObject rightPipe = sortedPipes[sortedPipes.Count - 1];
            Debug.Log("Left Pipe X: " + leftPipe.transform.position.x + ", Right Pipe X: " + rightPipe.transform.position.x);
            if (leftPipe.transform.position.x < rightPipe.transform.position.x && ArePipesVisible(leftPipe, rightPipe))
            {
                if (currentBone == null && timer > spawnRate)
                {
                    
                    SpawnBoneBetweenPipes(leftPipe, rightPipe);
                    timer = 0;
                }

                UpdateBonePosition(leftPipe, rightPipe);
            }
            else
            {
                // Destroy the bone if it's no longer between visible pipes
                if (currentBone != null)
                {
                    
                    Destroy(currentBone);
                }
            }
        }
        else
        {
            // Destroy the bone if no pipes or not enough pipes are found
            if (currentBone != null)
            {
                Destroy(currentBone);
            }
        }
    }



}
