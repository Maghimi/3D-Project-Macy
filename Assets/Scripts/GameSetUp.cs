using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetUp : MonoBehaviour
{
    int redBallsRemaining = 7;
    int blueBallsRemaining = 7;
    float ballRadius;
    float ballDiameter;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform cueBallPosition;
    [SerializeField] Transform headBallPoition;
    // Start is called before the first frame update
    void Start()
    {
        ballRadius = ballPrefab.GetComponent<SphereCollider>().radius * 100f;
        ballDiameter = ballRadius * 2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void PlaceAllBalls()
    {
        PlaceCueBall();
        PlaceRandomBalls();
    }
    void PlaceCueBall()
    {
      GameObject ball = Instantiate(ballPrefab, cueBallPosition.position, Quaternion.identity);
      ball.GetComponent<Ball>().MakeCueBall();  
    }
    void PlaceEightBall(Vector3 position)
    {
        GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
        ball.GetComponent<Ball>().MakeEightBall();
    }
    void PlaceRandomBalls()
    {
        int NumInThisRow = 1;
        int rand;
        Vector3 firstInRowPosition = headBallPoition.position;
        Vector3 currentPositiom = firstInRowPosition;

        void PlaceRedBall(Vector3 position)
        {
            GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
            ball.GetComponent<Ball>().BallSetup(true);
            redBallsRemaining--;
        }
        void PlaceBlueBall(Vector3 position)
        {
            GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
            ball.GetComponent<Ball>().BallSetup(false);
            blueBallsRemaining--;
        }
        for (int i = 0; i<5; i++)
        {
            for (int j = 0; j< NumInThisRow; j++)
            {
                if(i == 2 && j == 1)
                {
                    PlaceEightBall(currentPositiom);
                }
                else if (redBallsRemaining > 0 && blueBallsRemaining > 0)
                {
                    rand = Random.Range(0, 2);
                    if (rand == 0)
                    {
                        PlaceRedBall(currentPositiom);
                    }
                    else
                    {
                        PlaceBlueBall(currentPositiom);
                    }
                }
                else if (redBallsRemaining > 0)
                {
                    PlaceRedBall(currentPositiom);
                }
                else
                {
                    PlaceBlueBall(currentPositiom);
                }
                currentPositiom += new Vector3(1, 0, 0).normalized * ballDiameter;
            }
            firstInRowPosition += new Vector3(-1, 0, -1).normalized * ballDiameter;
            currentPositiom = firstInRowPosition;
            NumInThisRow ++;
        }
    }
}
