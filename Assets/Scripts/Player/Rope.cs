using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{

    private LineRenderer lineRenderer;
    private List<RopeSegment> ropeSegments = new List<RopeSegment>{};
    public float RoSegLenght = 0.25f;
    public int SegLenght= 35;
    public Vector3 RopeStartPoint;

    public float LineWidth = 0.1f;
    public Vector2 GravityForce = new Vector2(0f, -1f);



    // Start is called before the first frame update
    void Start()
    {
        this.lineRenderer = this.GetComponent<LineRenderer>();

        for(int i = 0; i < SegLenght; i++)
        {
            this.ropeSegments.Add(new RopeSegment(RopeStartPoint));
            RopeStartPoint.y -= RoSegLenght;
        }

    }

    // Update is called once per frame
    void Update()
    {
        this.DrawRope();   
    }

    private void FixedUpdate()
    {
        this.Simulate();
    }

    public void Simulate()
    {
        for (int i = 0; i < this.SegLenght; i++)
        {
            RopeSegment firstSegment = this.ropeSegments[i];
            Vector2 velocity = firstSegment.posNow - firstSegment.posOld;
            firstSegment.posOld = firstSegment.posNow;
            firstSegment.posNow += velocity;
            firstSegment.posNow += GravityForce * Time.deltaTime;
            this.ropeSegments[i] = firstSegment;
        }

        for(int i = 0; i < SegLenght - 1; i++)
        {
            ApplyConstraint();
        }
    }

    public void ApplyConstraint()
    {
        RopeSegment firsSegment = this.ropeSegments[0];
        firsSegment.posNow = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.ropeSegments[0] = firsSegment;

        for ( int i = 0; i < this.SegLenght - 1; i++)
        {
            RopeSegment firsSeg = this.ropeSegments[i];
            RopeSegment secondSeg = this.ropeSegments[i + 1];

            float dist = (firsSeg.posNow - secondSeg.posNow).magnitude;
            float error = Mathf.Abs(dist - this.RoSegLenght);
            Vector2 changeDir = Vector2.zero;

            if(dist > RoSegLenght)
            {
                changeDir = (firsSeg.posNow - secondSeg.posNow).normalized;

            }
            else if ( dist < RoSegLenght)
            {
                changeDir = (secondSeg.posNow - firsSeg.posNow).normalized;
            }

            Vector2 changeAmount = changeDir * error;
            if(i != 0)
            {
                firsSeg.posNow -= changeAmount * 0.5f;
                this.ropeSegments[i] = firsSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                this.ropeSegments[i + 1] = secondSeg;
            }else
            {
                secondSeg.posNow += changeAmount;
                this.ropeSegments[i + 1] = secondSeg;
            }
        }
    }

    public void DrawRope()
    {
        float lineWidth = this.LineWidth;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        Vector3[] ropePosition = new Vector3[this.SegLenght];
        for (int i = 0; i < this.SegLenght; i++)
        {
            ropePosition[i] = this.ropeSegments[i].posNow;
        }

        lineRenderer.positionCount = ropePosition.Length;
        lineRenderer.SetPositions(ropePosition);
    }

    public struct RopeSegment
    {
        public Vector2 posNow;
        public Vector2 posOld;

        public RopeSegment(Vector2 pos)
        {
            this.posNow = pos;
            this.posOld = pos;
        }

    }
}
