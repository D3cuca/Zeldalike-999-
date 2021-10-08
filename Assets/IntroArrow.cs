using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroArrow : MonoBehaviour
{
    public enum ArrowType
    {
        Up,
        Down,
        Left,
        Right
    }


    public ArrowType ThisArrowType;
    public Vector3 Change;

    public GameObject Block;
    public GameObject Sprite;
    public Vector3 PressedPositionBlock;
    public Vector3 PressedPositionSprite;
    public Vector3 BlockPressedScale;

    public Vector3 IdlePositionBlock;
    public Vector3 IdlePositionSprite;
    public Vector3 IdleScaleBlock;

    public IntroRope ThisRope;

    // Start is called before the first frame update
    void Start()
    {
        IdlePositionBlock = Block.transform.position;
        IdlePositionSprite = Sprite.transform.position;
        IdleScaleBlock = Block.transform.localScale;

        ThisRope = this.gameObject.GetComponentInChildren<IntroRope>();
    }

    // Update is called once per frame
    void Update()
    {
        Change = Vector3.zero;
        Change.x = Input.GetAxisRaw("Horizontal");
        Change.y = Input.GetAxisRaw("Vertical");

        if (Change != Vector3.zero)
        {
            if (ThisArrowType == ArrowType.Up && Change.y > 0)
            {
                BePressed();
                if (!ThisRope.DroppedThisTime)
                {
                    ThisRope.AddSegment();
                    ThisRope.AddSegment();
                    ThisRope.AddSegment();
                    ThisRope.AddSegment();
                }
            }
            if (ThisArrowType == ArrowType.Down && Change.y < 0)
            {
                BePressed();
                if (!ThisRope.DroppedThisTime)
                {
                    ThisRope.AddSegment();
                    ThisRope.AddSegment();
                    ThisRope.AddSegment();
                    ThisRope.AddSegment();
                }
            }
            if (ThisArrowType == ArrowType.Right && Change.x > 0)
            {
                BePressed();
                if (!ThisRope.DroppedThisTime)
                {
                    ThisRope.AddSegment();
                    ThisRope.AddSegment();
                    ThisRope.AddSegment();
                    ThisRope.AddSegment();
                }
            }
            if (ThisArrowType == ArrowType.Left && Change.x < 0)
            {
                BePressed();
                if (!ThisRope.DroppedThisTime)
                {
                    ThisRope.AddSegment();
                    ThisRope.AddSegment();
                    ThisRope.AddSegment();
                    ThisRope.AddSegment();
                }
            }
        }
        else
        {
            Idle();
            ThisRope.DroppedThisTime = false;
        }

    }

    public void DropString()
    {

    }

    public void BePressed()
    {
        Block.transform.localScale = BlockPressedScale;
        Block.transform.position = PressedPositionBlock;
        Sprite.transform.position =  PressedPositionSprite;
    }

    public void Idle()
    {
        Block.transform.localScale = IdleScaleBlock;
        Block.transform.position = IdlePositionBlock;
        Sprite.transform.position = IdlePositionSprite;
        Debug.Log("Yes");
    }
}
