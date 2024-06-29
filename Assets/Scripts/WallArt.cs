using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class WallArt : MonoBehaviour
{
    public Texture artTexture;
    public VideoClip vidClip;

    public VideoPlayer videoPlayer;
    public Transform videoScreen;
    public GameObject ArtPiece;

    private bool inRange = false;

    private bool isVidPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        videoScreen.gameObject.SetActive(false);
        videoPlayer.clip = vidClip;

        Material newMat = new Material(ArtPiece.GetComponent<MeshRenderer>().material);
        newMat.mainTexture = artTexture;

        ArtPiece.GetComponent<MeshRenderer>().material = newMat;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y) && inRange)
        {
            if (isVidPlaying) return;

            foreach (WallArt item in GameObject.FindObjectsOfType<WallArt>())
            {
                item.StopThisShow();
            }

            videoScreen.gameObject.SetActive(true);


            videoPlayer.Play();
            videoPlayer.loopPointReached += VideoPlayer_loopPointReached;
            isVidPlaying = true;
        }
    }

    private void VideoPlayer_loopPointReached(VideoPlayer source)
    {
        videoScreen.gameObject.SetActive(false);
        isVidPlaying = false;


    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerMovement player))
        {
            inRange = true;
            if (isVidPlaying) return;
            PromptsManager.Instance.ShowPrompt("Press Y To Interact", 5f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement player))
        {
            inRange = false;

            PromptsManager.Instance.RemovePrompt();
        }
    }

    public void StopThisShow()
    {
        videoScreen.gameObject.SetActive(false);
        isVidPlaying = false;
    }
}
