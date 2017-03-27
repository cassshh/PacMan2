using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSprite : MonoBehaviour {
    //We need a reference to our SpriteRenderer, which we assign in the Awake function
    SpriteRenderer sRenderer;

    void Awake()
    {
        sRenderer = GetComponent<SpriteRenderer>();
    }


    //Next, we need a public function SetSprite which sets a sprite to our SpriteRenderer and sets the sortingOrder. 
    //We also create an overload for this function, so we can set a sprite with the lowest sortingOrder without setting it as a parameter.
    public void SetSprite(Sprite sprite, int sortingOrder)
    {
        sRenderer.sprite = sprite;
        sRenderer.sortingOrder = sortingOrder;
    }

    public void SetSprite(Sprite sprite)
    {
        SetSprite(sprite, 0);
    }
}
