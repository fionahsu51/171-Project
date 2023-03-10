using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Scroller_Outdated : MonoBehaviour
{

    public GameObject otter;
    public SpriteRenderer bg1;
    public SpriteRenderer bg2;
    public Sprite startSprite;
    public Sprite sunSprite;
    public Sprite tranSunTwiSprite;
    public Sprite twiSprite;
    public Sprite tranTwiMidSprite;
    public Sprite midSprite;
    public Sprite tranMidAbSprite;
    public Sprite abSprite;
    public Sprite endSprite;
    private float depth;

    private float time;
    private int section;
    private float sectionSize;

    private float offset;
    private float start;

    public int sunlightLimit;
    public int twilightLimit;
    public int midnightLimit;
    public int end;

    public GameObject bottom;

    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        section = 0;
        //bg1.transform.position = new Vector3(bg1.transform.position.x, -bg1.bounds.size.y/2f, 0f)
        start = bg1.transform.position.y/2f;

        //teleport the bottom collider to the end scene
        float bottomPos = ((-end-1) * bg1.bounds.size.y);
        bottomPos += (6 * bg1.bounds.size.y) / 10;
        bottom.transform.position = new Vector3(bottom.transform.position.x, bottomPos, bottom.transform.position.z);
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 1f) {
            time = 0f;
            //Debug.Log(otter.transform.position.y.ToString() + " vs " + bg1.bounds.size.y.ToString());
        }
        section = (int)Math.Abs(otter.transform.position.y/bg1.bounds.size.y); 

        float halfLength = bg1.bounds.size.y/2;

        //first, find out whether otter is occupying bg1 or bg2
        SpriteRenderer occupying = null;
        SpriteRenderer other = null;
        if (otter.transform.position.y < bg1.transform.position.y + halfLength && otter.transform.position.y > bg1.transform.position.y - halfLength) {
            occupying = bg1;
            other = bg2;
        } else {
            occupying = bg2;
            other = bg1;
        }


        //first, detect if otter is in upper half or lower half
        section = Math.Abs( (int)( (otter.transform.position.y - halfLength) / (bg1.bounds.size.y) ) );
        float currentTop = (- section * bg1.bounds.size.y) + halfLength;
        occupying.transform.position = new Vector3(0f, -section * bg1.bounds.size.y, 0f);
        string half = "";
        if (otter.transform.position.y >= currentTop - halfLength) {
            half = "upper";
        } else {
            half = "lower";
        }
        if (half == "upper") {
            other.transform.position = new Vector3(0f, occupying.transform.position.y + occupying.bounds.size.y, 0f);
        } else {
            other.transform.position = new Vector3(0f, occupying.transform.position.y - occupying.bounds.size.y, 0f);
        }
        
        //next make sure bg1 and bg2 have the right background depending on what depth we're at
        //first, take care of occupied background
        if (section == 0) {
            occupying.sprite = startSprite;
        } else if (section < sunlightLimit) {
            occupying.sprite = sunSprite;
        } else if (section == sunlightLimit) {
            occupying.sprite = tranSunTwiSprite;
        } else if (section < twilightLimit) {  //&& section < midnightBound
            occupying.sprite = twiSprite;
        } else if (section == twilightLimit) {
            occupying.sprite = tranTwiMidSprite;
        } else if (section < midnightLimit) {
            occupying.sprite = midSprite;
        } else if (section == midnightLimit) {
            occupying.sprite = tranMidAbSprite;
        } else if (section < end) {
            occupying.sprite = abSprite;
        } else if (section == end) {
            occupying.sprite = endSprite;
        }


        if (half == "upper") {
            if (section == 1) other.sprite = startSprite;
            else if (section <= sunlightLimit) other.sprite = sunSprite;
            else if (section == sunlightLimit + 1) other.sprite = tranSunTwiSprite;
            else if (section <= twilightLimit) other.sprite = twiSprite;
            else if (section == twilightLimit + 1) other.sprite = tranTwiMidSprite;
            else if (section <= midnightLimit) other.sprite = midSprite;
            else if (section == midnightLimit + 1) other.sprite = tranMidAbSprite;
            else if (section <= end) other.sprite = abSprite;
        }
        else if (half == "lower") {
            if (section < sunlightLimit - 1) other.sprite = sunSprite;
            else if (section == sunlightLimit - 1) other.sprite = tranSunTwiSprite;
            else if (section < twilightLimit - 1) other.sprite = twiSprite;
            else if (section == twilightLimit - 1) other.sprite = tranTwiMidSprite;
            else if (section < midnightLimit - 1) other.sprite = midSprite;
            else if (section == midnightLimit - 1) other.sprite = tranMidAbSprite;
            else if (section < end - 1) other.sprite = abSprite;
            else if (section == end - 1) other.sprite = endSprite;
        }


   }
}

