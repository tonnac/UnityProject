namespace RPG.SceneManagement
{
    using System;
    using System.Collections;
    using System.Threading.Tasks;

    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.AI;
    
    public class Portal : MonoBehaviour 
    {
        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        private void OnTriggerEnter(Collider other) 
        {
            if(other.tag == "Player")
            {
                StartCoroutine(Transition());

                // Task qwe = AwaitTransition_();
                // Debug.Log("wwwewe");
                // await qwe;
            }
        }

        private IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            Debug.Log(otherPortal.spawnPoint.position);
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            Portal[] portals = Resources.FindObjectsOfTypeAll(typeof(Portal)) as Portal[];
            return Array.Find(portals, (portal) => this != portal);
        }

        private async Task AwaitTransition_()
        {
            print("start AwaitTransition");
            await Task.Factory.StartNew(() =>
            {
                for (uint i = 0; i < 4000000000; ++i)
                {

                }
            });
            print("Scene Loaded");
        }
    }
}