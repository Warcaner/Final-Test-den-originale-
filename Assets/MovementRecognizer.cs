using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Avatar;
using PDollarGestureRecognizer;
using System.IO;
using UnityEngine.Events;

public class MovementRecognizer : MonoBehaviour


{
    public float inputthreshold = 0.1f;

    public Transform movementSource;

    private bool isMoving = false;

    public GameObject debugCubePrefab;


    public float newPositionThreshHoldDistance = 0.05f;

    private List<Vector3> positionsList = new List<Vector3>();


    public GameObject thatWhiteSquare;


    public OVRInput.Button Gay;

    public bool trigActivate;

    public string NewGestureName;

    public bool CreationMode = true;

    private List<Gesture> trainingSet = new List<Gesture>();

    public float recognitionThreshold = 0.8f;

    [System.Serializable]
    public class UnityStringEvent : UnityEvent<string> { }
    public UnityStringEvent onRecognized;


    // Start is called before the first frame update
    void Start()
    {
        string[] gestureFiles = Directory.GetFiles(Application.persistentDataPath, "*.xml");

        foreach (var item in gestureFiles)
        {

            trainingSet.Add(GestureIO.ReadGestureFromFile(item));


        }


    }

    // Update is called once per frame
    void Update()
    {
        trigActivate = OVRInput.Get(Gay);

        /*while (trigActivate == true)
           {
               Destroy(Instantiate(debugCubePrefab, movementSource.position, Quaternion.identity), 3);
           } */

        //start the movement
        if (!isMoving && trigActivate == true)
        {

            StartMovement();


        }


        //Ending The Movement
        else if (isMoving && trigActivate == false)
        {

            EndMovement();


        }


        // Updating The Movement
        else if (isMoving && trigActivate == true)
        {

            UpdateMovement();



        }


    }




    void StartMovement()

    {

        thatWhiteSquare.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        isMoving = true;

        positionsList.Clear();
        positionsList.Add(movementSource.position);

        if (debugCubePrefab)
            Destroy(Instantiate(debugCubePrefab, movementSource.position, Quaternion.identity), 3);


    }


    void EndMovement()
    {
        thatWhiteSquare.GetComponent<Renderer>().material.color = new Color(0, 255, 0);


        isMoving = false;
        // gotta create gesture shit
        Point[] pointArray = new Point[positionsList.Count];

        for (int i = 0; i < positionsList.Count; i++)
        {

            Vector2 screenPoint = Camera.main.WorldToScreenPoint(positionsList[i]);
            pointArray[i] = new Point(screenPoint.x, screenPoint.y, 0);



        }

        // add a new gesture
        Gesture newGesture = new Gesture(pointArray);
        if (CreationMode)
        {

            newGesture.Name = NewGestureName;

            trainingSet.Add(newGesture);

            string fileName = Application.persistentDataPath + "/" + NewGestureName + ".xml";
            GestureIO.WriteGesture(pointArray, NewGestureName, fileName);


        }

        //recognize
        else
        {


            Result result = PointCloudRecognizer.Classify(newGesture, trainingSet.ToArray());

            Debug.Log(result.GestureClass + result.Score);
            if(result.Score > recognitionThreshold)
            {
                onRecognized.Invoke(result.GestureClass);


            }


        } }



        void UpdateMovement()
        {

            thatWhiteSquare.GetComponent<Renderer>().material.color = new Color(0, 0, 255);
            Vector3 lastPosition = positionsList[positionsList.Count - 1];
            if (Vector3.Distance(movementSource.position, lastPosition) > newPositionThreshHoldDistance)

                positionsList.Add(movementSource.position);

            if (debugCubePrefab)
                Destroy(Instantiate(debugCubePrefab, movementSource.position, Quaternion.identity), 3);


        }
    }






