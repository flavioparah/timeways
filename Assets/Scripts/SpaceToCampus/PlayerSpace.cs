using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpace : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float angularSpeed;
    [SerializeField] float panelSpeed;
    [SerializeField] float force;
    [SerializeField] Transform station;
    [SerializeField] Transform antenna;
    [SerializeField] Sprite turningSprite;
    [SerializeField] Sprite initialSprite;
    [SerializeField] UlisesConnection ulisesConnection;
    //[SerializeField] float minSizePlayer;
    //[SerializeField] float maxSizePlayer;
    //[SerializeField] float minSizeBone;
    //[SerializeField] float maxSizeBone;
    [SerializeField] Pad pad;
    Panel panel;
    InputReader inputReader;
    Rigidbody2D rb;
    Vector2 direction;
    bool movingRight;
    bool movingLeft;
    bool movingUp;
    bool movingDown;

    float maxDistance;
    [SerializeField] bool mainMenu;

    [SerializeField] GameObject particles;
    [SerializeField] GameObject particlesToLeft;
    [SerializeField] GameObject particlesToRight;
    SolarPanel solarPanel;

    bool interacting;
    bool padInteracting;
    bool isTurning;
    bool isParticleOn;
    bool onPanel;
    bool onPuzzle;
    Transform solarPoint;
    bool waitingAnimation;

    [SerializeField] WavePanel wavePanel;
    [SerializeField] Antenna antennaManager;

    WavePuzzleManager antennaPoint;
    bool isOnWavePanel;
    Coroutine movingCoroutine;
    private void Awake()
    {

    }
    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        maxDistance = Vector2.Distance(station.position, antenna.position);
        inputReader = this.GetComponent<InputReader>();
        inputReader.EnableSpace();
        inputReader.spaceMoveEvent += Move;
        inputReader.cancelSpaceMoveEvent += CancelMove;
        inputReader.pauseEvent += Pause;
        inputReader.interactEvent += Interact;
        inputReader.interactPadEvent += InteractPad;

    }

    private void OnEnable()
    {


    }
    private void OnDisable()
    {
        inputReader.spaceMoveEvent -= Move;
        inputReader.cancelSpaceMoveEvent -= CancelMove;

        inputReader.pauseEvent -= Pause;
        inputReader.interactEvent -= Interact;
        inputReader.interactPadEvent -= InteractPad;
    }

    private void Update()
    {
        if (waitingAnimation) return;
        if (onPanel) return;
        if (isOnWavePanel)
        {
            if (padInteracting)
                wavePanel.ChangeValue(direction);
            return;
        }
        if (direction != Vector2.zero)
        {
            if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(Vector3.forward, direction)) < 1)
                this.GetComponent<SpriteRenderer>().sprite = initialSprite;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.forward, direction), angularSpeed * Time.deltaTime);
            // transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }


        if (movingRight)
        {
            rb.AddForce(Vector2.right * force);
        }
        else if (movingLeft)
        {
            rb.AddForce(Vector2.left * force);
        }

        if (movingUp)
        {
            rb.AddForce(Vector2.up * force);
        }

        else if (movingDown)
        {
            rb.AddForce(Vector2.down * force);
        }


    }
    private void FixedUpdate()
    {
        // rb.velocity = direction * speed;
    }
    private void Move(Vector2 move)
    {

        direction = move;

        movingRight = move.x > 0;
        movingLeft = move.x < 0;
        movingUp = move.y > 0;
        movingDown = move.y < 0;

        if (onPanel)
        {
            if (onPuzzle) return;
            MoveOnPanel();
            return;
        }

        if (isOnWavePanel)
        {
            if (padInteracting)
            {

                return;
            }
            MoveOnAntennaPuzzle();
            return;
        }

        isParticleOn = true;

        if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(Vector3.forward, direction)) > 1)
        {
            this.GetComponent<SpriteRenderer>().sprite = turningSprite;
            if (Vector3.Cross(transform.up, direction).z < 0)
            {
                TurnRight();
                // this.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                TurnLeft();

            }
        }
        else
        {
            TurnBack();
        }

    }

    private void CancelMove()
    {
        //this.GetComponent<SpriteRenderer>().sprite = initialSprite;
        movingRight = false;
        movingLeft = false;
        movingUp = false;
        movingDown = false;
        direction = Vector2.zero;
        isParticleOn = false;
        particles.SetActive(false);
        particlesToRight.SetActive(false);
        particlesToLeft.SetActive(false);
    }

    void Interact()
    {
        if (onPanel)
        {
            if (onPuzzle) return;
            if (solarPanel.PuzzleComplete(solarPoint)) return;
            solarPanel.OpenPuzzle(solarPoint);
            onPuzzle = true;
            CameraManager.Instance.ChangeCamera(solarPoint.Find("puzzleCam").GetComponent<CinemachineVirtualCamera>());
            StartCoroutine(HidingPlayer());
        }

        if (isOnWavePanel)
        {
            if (onPuzzle) return;
            InteractPad();
        }
        //if (solarPanel != null)
        //{
        //    solarPanel.Access();
        //}
    }

    void Pause()
    {

        if (onPuzzle)
        {
            if (isOnWavePanel)
            {
                pad.ClosePadScreen();
                return;
            }

            solarPoint.GetComponentInChildren<PuzzleManager>().TurnScreenOff();
            CameraManager.Instance.ChangeCamera(true, true);
            Show();
            onPuzzle = false;
        }
    }

    void InteractPad()
    {
        if (isOnWavePanel)
        {
            if (onPuzzle)
            {
                pad.ClosePadScreen();
                return;
            }
            if (antennaManager.PuzzleComplete(antennaPoint)) return;

            pad.OpenPad(wavePanel);

            padInteracting = true;
            antennaManager.OpenPuzzle(wavePanel);
            onPuzzle = true;
            return;
            //  CameraManager.Instance.ChangeCamera(solarPoint.Find("puzzleCam").GetComponent<CinemachineVirtualCamera>());
            // StartCoroutine(HidingPlayer());
        }

        if (padInteracting)
        {
            pad.ClosePadScreen();
            return;
        }

        pad.OpenPad(panel);

        padInteracting = true;

    }

    public void ClosePad()
    {
        padInteracting = false;
        onPuzzle = false;
    }
    void TurnLeft()
    {
        particles.SetActive(false);
        particlesToRight.SetActive(false);
        particlesToLeft.SetActive(isParticleOn);
        this.GetComponent<SpriteRenderer>().flipX = false;
    }
    void TurnRight()
    {
        particles.SetActive(false);
        particlesToRight.SetActive(isParticleOn);
        particlesToLeft.SetActive(false);
        this.GetComponent<SpriteRenderer>().flipX = true;
    }

    void TurnBack()
    {
        particles.SetActive(isParticleOn);
        particlesToRight.SetActive(false);
        particlesToLeft.SetActive(false);
        this.GetComponent<SpriteRenderer>().flipX = false;
        this.GetComponent<SpriteRenderer>().sprite = initialSprite;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "SolarPanel")
        {
            if (onPanel) return;
            solarPanel = collision.GetComponent<SolarPanel>();
            CameraManager.Instance.ChangeCamera(true, true);
            GoToPanelPoint();
        }

        if (collision.tag == "Antenna")
        {
            isOnWavePanel = true;
            GoToAntennaPoint(false);

        }

        if (collision.tag == "AntennaBase")
        {
            isOnWavePanel = true;
            GoToAntennaPoint(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.tag == "SolarPanel")
        //{

        //    solarPanel = null;
        //    CameraManager.Instance.ChangeCamera(false, false);
        //}

        //if (collision.tag == "Antenna")
        //{
        //    isOnWavePanel = false;
        //    CameraManager.Instance.ChangeCamera(false, false);
        //}
        //if (collision.tag == "AntennaBase")
        //{
        //    isOnWavePanel = false;
        //    CameraManager.Instance.ChangeCamera(false, false);
        //}
    }

    void GoToPanelPoint()
    {
        TurnRight();
        onPanel = true;
        rb.velocity = Vector3.zero;
        Transform firstPoint = solarPanel.GetPoint(0);
        solarPoint = firstPoint;

        solarPoint.GetComponentInChildren<PuzzleManager>().TurnScreenOn();
        if (movingCoroutine != null)
            StopCoroutine(movingCoroutine);
        movingCoroutine = StartCoroutine(GointToPoint(firstPoint));
    }

    void MoveOnPanel()
    {
        if (waitingAnimation) return;
        solarPoint.GetComponentInChildren<PuzzleManager>().TurnScreenOff();
        Transform newPoint = solarPoint;

        if (movingDown)
            newPoint = solarPanel.PointOnDown(solarPoint);
        else if (movingLeft)
            newPoint = solarPanel.PointOnLeft(solarPoint);
        else if (movingRight)
            newPoint = solarPanel.PointOnRight(solarPoint);
        else if (movingUp)
            newPoint = solarPanel.PointOnUp(solarPoint);

        if (!solarPoint.Equals(newPoint))
        {
            solarPoint = newPoint;

            if (movingCoroutine != null)
                StopCoroutine(movingCoroutine);
            movingCoroutine = StartCoroutine(GointToPoint(solarPoint));
            return;
        }
        onPanel = false;
        Move(direction);
        CameraManager.Instance.ChangeCamera(false, false);
    }

    IEnumerator GointToPoint(Transform point)
    {
        particlesToRight.SetActive(true);
        float factor = 0;

        Vector2 startPos = transform.position;
        Vector2 finalPos = point.position;
        Vector3 startUp = transform.up;
        while (factor < 1)
        {
            factor += Time.deltaTime * panelSpeed;
            transform.position = Vector2.Lerp(startPos, finalPos, factor);
            transform.up = Vector3.Lerp(startUp, point.up, factor);
            if (factor >= 1)
            {
                transform.position = finalPos;
                transform.up = point.up;
                if (onPanel)
                    solarPoint.GetComponentInChildren<PuzzleManager>().TurnScreenOn();

                particlesToRight.SetActive(false);

            }
            yield return null;
        }
    }

    IEnumerator HidingPlayer()
    {
        SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
        float factor = 0;
        float speed = this.speed;
        Color c = Color.white;
        while (factor < 1)
        {
            factor += speed * Time.deltaTime;

            c.a = 1 - factor;
            sr.color = c;

            if (factor >= 1)
            {
                c.a = 0;
                sr.color = c;
            }
            yield return null;
        }
    }

    IEnumerator ShowingPlayer()
    {
        SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
        float factor = 0;
        float speed = this.speed;
        Color c = Color.white;
        while (factor < 1)
        {
            factor += speed * Time.deltaTime;

            c.a = factor;
            sr.color = c;

            if (factor >= 1)
            {
                c.a = 1;
                sr.color = c;
            }
            yield return null;
        }
    }

    public void Show()
    {
        onPuzzle = false;
        StartCoroutine(ShowingPlayer());
    }

    public void SetWaitingState(bool isWaiting)
    {
        waitingAnimation = isWaiting;
    }

    public void ObjectiveCompleted(bool isAntenna)
    {
        if (isAntenna) ulisesConnection.Objective1Complete();
        else ulisesConnection.Objective2Complete();

        InteractPad();
    }
    //--------------------Antenna-------------------

    void GoToAntennaPoint(bool isBase)
    {
        TurnRight();
        isOnWavePanel = true;
        rb.velocity = Vector3.zero;
        Transform antennaPoint = isBase ? antennaManager.GetBasePoint() : antennaManager.GetAntennaPoint();

        this.antennaPoint = antennaManager.GetCurrentPuzzle();

        if (movingCoroutine != null)
            StopCoroutine(movingCoroutine);
        movingCoroutine = StartCoroutine(GointToPoint(antennaPoint));
    }

    void MoveOnAntennaPuzzle()
    {
        if (waitingAnimation) return;



        bool newPointSelected = false;

        if (movingDown || movingLeft)
        {
            newPointSelected = antennaManager.GetPreviousPoint(antennaPoint);
            antennaPoint = antennaManager.GetCurrentPuzzle();
        }


        else if (movingRight || movingUp)
        {
            newPointSelected = antennaManager.GetNextPoint(antennaPoint);
            antennaPoint = antennaManager.GetCurrentPuzzle();
        }


        if (newPointSelected)
        {

            return;
        }


        LeaveWavePanel();
    }

    public void LeaveWavePanel()
    {
        isOnWavePanel = false;
        Move(direction);
        CameraManager.Instance.ChangeCamera(false, false);
    }

    public void SetWavePuzzle(WavePuzzleManager puzzle)
    {
        antennaPoint = puzzle;
    }
    public void LeavePuzzle()
    {
        onPuzzle = false;
    }
}
