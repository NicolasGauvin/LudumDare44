using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SlideController : MonoBehaviour
{

    public Sprite[] slides;
    public float timeSlide;
    public Image holder;
    public int index;
    public float nextSlideTime;

    private AssetBundle myLoadedAssetBundle;
    private string[] scenePaths;

    // Start is called before the first frame update
    void Start()
    {
        GetNextTimeSlide();
        NextSlide();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextSlideTime)
        {
            GetNextTimeSlide();
            NextSlide();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetNextTimeSlide();
            NextSlide();
        }
    }

    private void GetNextTimeSlide()
    {
        nextSlideTime = Time.time + timeSlide;
    }

    private void NextSlide()
    {
        if(index < slides.Length)
        {
            holder.GetComponent<Image>().sprite = slides[index];
            index++;
        }
        else
        {
            SceneManager.LoadScene("Scene1");
        }
    }

}
