using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanController : MonoBehaviour
{
    [Header("Variables")]
    public Rigidbody humanRB;
    public bool moveLeft, isPatrolling, isTVOn, bookNeedsFix, isLightsOn, isLights2On, isLights3On, isToasterOn, isFaucetOn, isPCOn, isToiletFaucetOn, isKitchenFaucetOn;
    public Transform patrolPointTarget;
    public NavMeshAgent humanAgent; //access speed through navmeshagent speed
    public GameObject toyFriend;
    public bool hasToyFriend;

    [SerializeField] private bool isWaiting;
    [SerializeField] private float totalWaitTime = 3f;
    [SerializeField] private float totalDistractedTime = 7f;
    [SerializeField] private List<HumanPatrolPoints> patrolPoints;
    [SerializeField] private List<HumanPatrolPoints> distractionPoints;

    private int currentPatrolIndex;
    private float waitTimer;
    private bool waiting;
    private float distractTimer;
    private bool distracted;
    private bool isTouchingToy;
    private Vector3 offset = new Vector3(0f, 10f, -1f);
    public bool AlreadyDistracted;
    private float speed = 1.0f;
    private Animator anim;
    public HearingScript hearing;
    public GameObject player;
    public GameObject hearingObject;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        humanRB = gameObject.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        humanAgent = this.GetComponent<NavMeshAgent>();
        hearingObject = GameObject.FindGameObjectWithTag("HearingObject");
        hearing = hearingObject.GetComponent(typeof(HearingScript)) as HearingScript;
        if (humanAgent == null) { return; }

        if (patrolPoints != null && patrolPoints.Count >= 2)
        {
            currentPatrolIndex = 0;
            SetPatrolDestination();
        }

        hasToyFriend = true;
    }

    // Update is called once per frame  
    void Update()
    {
        Patrol();
        TakeAction();
    }

    void Patrol()
    {
        //anim.SetBool("isPatrolling", true);
        if (isPatrolling && humanAgent.remainingDistance <= 1.0f)
        {
            isPatrolling = false;
            if (isWaiting)
            {
                waiting = true;
                anim.SetBool("isWaiting", true);
                //anim.SetBool("isPatrolling", false);
            }
            else
            {
                ChangePatrolPoint();
                SetPatrolDestination();
            }
        }

        if (waiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer > totalWaitTime)
            {
                waiting = false;
                waitTimer = 0f;
                anim.SetBool("isWaiting", false);
                //anim.SetBool("isPatrolling", true);

                ChangePatrolPoint();
                SetPatrolDestination();
            }
        }
    }

    private void SetPatrolDestination()
    {
        if (patrolPoints != null)
        {
            Vector3 targetVector = patrolPoints[currentPatrolIndex].transform.position;
            humanAgent.SetDestination(targetVector);
            isPatrolling = true;
        }
    }

    private void ChangePatrolPoint()
    {
        int randomPoint = UnityEngine.Random.Range(0, patrolPoints.Count);
        if (currentPatrolIndex != randomPoint)
            currentPatrolIndex = randomPoint;
    }

    private void TakeAction()
    {
        if (hearing.GetIsHeard()) {
            Debug.Log("Working");
            MoveToSound(player.transform);
        } else if (!hearing.GetIsHeard()) {
            isPatrolling = true;
            if (isTVOn) {
                hasToy();
                distracted = true;
                TurnOffTv();
            } else if (bookNeedsFix) {
                hasToy();
                distracted = true;
                PutBookBack();
            } else if (isLightsOn) {
                hasToy();
                distracted = true;
                TurnLightsOn();
            } else if (isToasterOn) {
                hasToy();
                distracted = true;
                TurnToasterOff();
            } else if (isFaucetOn) {
                hasToy();
                distracted = true;
                TurnFaucetOff();
            } else if (isLights2On) {
                hasToy();
                distracted = true;
                TurnLights2On();
            } else if (isLights3On) {
                hasToy();
                distracted = true;
                TurnLights3On();
            } else if (isPCOn) {
                hasToy();
                distracted = true;
                TurnPCOff();
            } else if (isToiletFaucetOn) {
                hasToy();
                distracted = true;
                TurnToiletFaucetOff();
            } else if (isKitchenFaucetOn) {
                hasToy();
                distracted = true;
                TurnKitchenFaucetOff();
            } else {
                if (!hasToyFriend && toyFriend.gameObject.activeSelf && GameObject.FindGameObjectWithTag("Toy") != null)
                {
                    Vector3 target = GameObject.FindGameObjectWithTag("Toy").transform.position;
                    humanAgent.SetDestination(target);

                    if (isTouchingToy) {
                        GameObject.FindGameObjectWithTag("Toy").GetComponent<InteractableObject>().DropToy();
                        Destroy(GameObject.FindGameObjectWithTag("Toy"));
                        //GameObject.FindGameObjectWithTag("Toy").SetActive(false);
                        hasToyFriend = true;
                    }
                }
                AlreadyDistracted = false;
                isPatrolling = true;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other) { 
        isTouchingToy = (other.tag == "Toy") ? true : false; 
    }

    private void hasToy()
    {
        if (hasToyFriend)
        {
            Instantiate(toyFriend, transform.position + offset, Quaternion.identity);
            hasToyFriend = false;

        }
    }

    #region Distraction Functions
    private enum Distraction //ENUMS TO DISTINGUISH BETWEEN DISTRACTIONS IF WE ARE TO ADD MORE LATER
    {
        TV,
        Bookshelf,
        Toaster,
        Lights,
        Faucet,
        Lights2,
        Lights3,
        PC,
        toiletFaucet,
        kitchenFaucet
    }

    private void TurnOffTv() {
        MoveToDistraction((int)Distraction.TV); //Move to corresponding distraction location
        Transform target = distractionPoints[(int)Distraction.TV].transform.parent;
        Vector3 targetPos = new Vector3(target.position.x, this.transform.position.y, target.position.z);

        if (distracted && humanAgent.remainingDistance <= 0.1f) //distracted for 7 seconds by default
        {
            transform.LookAt(targetPos);
            anim.SetBool("isTVOn", true);
            distractTimer += Time.deltaTime;
            if (distractTimer > totalDistractedTime)
            {
                distracted = false;
                distractTimer = 0f;
            }
        }

        if (humanAgent.remainingDistance < 0.5f && !distracted) //No longer distracted, return to patrolling
        {
            isTVOn = false;
            anim.SetBool("isTVOn", false);
        }
    }

    private void PutBookBack()
    {
        MoveToDistraction((int)Distraction.Bookshelf);
        Transform target = distractionPoints[(int)Distraction.Bookshelf].transform.parent;
        Vector3 targetPos = new Vector3(target.position.x, this.transform.position.y, target.position.z);

        if (distracted && humanAgent.remainingDistance <= 0.1f)
        {
            transform.LookAt(targetPos);
            anim.SetBool("bookNeedFix", true);
            distractTimer += Time.deltaTime;
            if (distractTimer > totalDistractedTime)
            {
                distracted = false;
                distractTimer = 0f;
            }
        }

        if (humanAgent.remainingDistance < 0.5f && !distracted)
        { 
            bookNeedsFix = false;
            anim.SetBool("bookNeedFix", false);
        }
    }

    private void TurnToasterOff()
    {
        MoveToDistraction((int)Distraction.Toaster);
        Transform target = distractionPoints[(int)Distraction.Toaster].transform.parent;
        Vector3 targetPos = new Vector3(target.position.x, this.transform.position.y, target.position.z);

        if (distracted && humanAgent.remainingDistance <= 0.1f)
        {
            transform.LookAt(targetPos);
            anim.SetBool("toasterOn", true);
            distractTimer += Time.deltaTime;
            if (distractTimer > totalDistractedTime)
            {
                distracted = false;
                distractTimer = 0f;
            }
        }

        if (humanAgent.remainingDistance < 0.5f && !distracted)
        {
            isToasterOn = false;
            anim.SetBool("toasterOn", false);
        }
    }

    private void TurnLightsOn()
    {
        MoveToDistraction((int)Distraction.Lights);
        Transform target = distractionPoints[(int)Distraction.Lights].transform.parent;
        Vector3 targetPos = new Vector3(target.position.x, this.transform.position.y, target.position.z);

        if (distracted && humanAgent.remainingDistance <= 0.1f)
        {
            transform.LookAt(targetPos);
            anim.SetBool("lightNeedsFix", true);
            distractTimer += Time.deltaTime;
            if (distractTimer > totalDistractedTime)
            {
                distracted = false;
                distractTimer = 0f;
            }
        }

        if (humanAgent.remainingDistance < 0.5f && !distracted)
        {
            isLightsOn = false;
            anim.SetBool("lightNeedsFix", false);
        }
    }

    private void TurnFaucetOff()
    {
        MoveToDistraction((int)Distraction.Faucet);
        Transform target = distractionPoints[(int)Distraction.Faucet].transform.parent;
        Vector3 targetPos = new Vector3(target.position.x, this.transform.position.y, target.position.z);

        if (distracted && humanAgent.remainingDistance <= 0.1f)
        {
            transform.LookAt(targetPos);
            anim.SetBool("faucetOn", true);
            distractTimer += Time.deltaTime;
            if (distractTimer > totalDistractedTime)
            {
                distracted = false;
                distractTimer = 0f;
            }
        }

        if (humanAgent.remainingDistance < 0.5f && !distracted)
        {
            isFaucetOn = false;
            anim.SetBool("faucetOn", false);
        }
    }
    private void TurnLights2On()
    {
        MoveToDistraction((int)Distraction.Lights2);
        Transform target = distractionPoints[(int)Distraction.Lights2].transform.parent;
        Vector3 targetPos = new Vector3(target.position.x, this.transform.position.y, target.position.z);

        if (distracted && humanAgent.remainingDistance <= 0.1f)
        {
            transform.LookAt(targetPos);
            anim.SetBool("lightNeedsFix", true);
            distractTimer += Time.deltaTime;
            if (distractTimer > totalDistractedTime)
            {
                distracted = false;
                distractTimer = 0f;
            }
        }

        if (humanAgent.remainingDistance < 0.5f && !distracted)
        {
            isLights2On = false;
            anim.SetBool("lightNeedsFix", false);
        }
    }
    private void TurnLights3On()
    {
        MoveToDistraction((int)Distraction.Lights3);
        Transform target = distractionPoints[(int)Distraction.Lights3].transform.parent;
        Vector3 targetPos = new Vector3(target.position.x, this.transform.position.y, target.position.z);

        if (distracted && humanAgent.remainingDistance <= 0.1f)
        {
            transform.LookAt(targetPos);
            anim.SetBool("lightNeedsFix", true);
            distractTimer += Time.deltaTime;
            if (distractTimer > totalDistractedTime)
            {
                distracted = false;
                distractTimer = 0f;
            }
        }

        if (humanAgent.remainingDistance < 0.5f && !distracted)
        {
            isLights3On = false;
            anim.SetBool("lightNeedsFix", false);
        }
    }
    private void TurnPCOff()
    {
        MoveToDistraction((int)Distraction.PC);
        Transform target = distractionPoints[(int)Distraction.PC].transform.parent;
        Vector3 targetPos = new Vector3(target.position.x, this.transform.position.y, target.position.z);

        if (distracted && humanAgent.remainingDistance <= 0.1f)
        {
            transform.LookAt(targetPos);
            anim.SetBool("isPCOn", true);
            distractTimer += Time.deltaTime;
            if (distractTimer > totalDistractedTime)
            {
                distracted = false;
                distractTimer = 0f;
            }
        }

        if (humanAgent.remainingDistance < 0.5f && !distracted)
        {
            isPCOn = false;
            anim.SetBool("isPCOn", false);
        }
    }

    private void TurnToiletFaucetOff()
    {
        MoveToDistraction((int)Distraction.toiletFaucet);
        Transform target = distractionPoints[(int)Distraction.toiletFaucet].transform.parent;
        Vector3 targetPos = new Vector3(target.position.x, this.transform.position.y, target.position.z);

        if (distracted && humanAgent.remainingDistance <= 0.1f)
        {
            transform.LookAt(targetPos);
            anim.SetBool("faucetOn", true);
            distractTimer += Time.deltaTime;
            if (distractTimer > totalDistractedTime)
            {
                distracted = false;
                distractTimer = 0f;
            }
        }

        if (humanAgent.remainingDistance < 0.5f && !distracted)
        {
            isToiletFaucetOn = false;
            anim.SetBool("faucetOn", false);
        }
    }

    private void TurnKitchenFaucetOff()
    {
        MoveToDistraction((int)Distraction.kitchenFaucet);
        Transform target = distractionPoints[(int)Distraction.kitchenFaucet].transform.parent;
        Vector3 targetPos = new Vector3(target.position.x, this.transform.position.y, target.position.z);

        if (distracted && humanAgent.remainingDistance <= 0.1f)
        {
            transform.LookAt(targetPos);
            anim.SetBool("faucetOn", true);
            distractTimer += Time.deltaTime;
            if (distractTimer > totalDistractedTime)
            {
                distracted = false;
                distractTimer = 0f;
            }
        }

        if (humanAgent.remainingDistance < 0.5f && !distracted)
        {
            isKitchenFaucetOn = false;
            anim.SetBool("faucetOn", false);
        }
    }

    private void MoveToDistraction(int distractionIndex)
    {
        isPatrolling = false;
        //anim.SetBool("isPatrolling", true);

        if (distractionPoints != null)
        {
            Vector3 targetVector = distractionPoints[distractionIndex].transform.position;
            humanAgent.SetDestination(targetVector);
        }
    }

    private void MoveToSound(Transform location)
    {
        isPatrolling = false;

        Vector3 targetVector = location.transform.position;
        humanAgent.SetDestination(targetVector);
    }
    #endregion
}
