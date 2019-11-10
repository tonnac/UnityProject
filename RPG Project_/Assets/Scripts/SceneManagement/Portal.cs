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
        enum DestinationIdentifier
        {
            A, B, C, D, E
        }

        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeInTime = 2f;
        [SerializeField] float fadeWaitTime = 0f;
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
            if(sceneToLoad < 0)
            {
                Debug.LogError("Scene to load not set.");
                yield break;
            }

            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeOutTime);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            yield return new WaitForSeconds(fadeWaitTime);

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            yield return fader.FadeIn(fadeInTime);
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
            return Array.Find(portals, (portal) => this != portal && this.destination == portal.destination);
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