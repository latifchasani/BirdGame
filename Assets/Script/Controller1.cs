using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerASD : MonoBehaviour, INetworkRunnerCallbacks
{
    public static Controller core;

    [Range(1, 10)]
    public float jumpVelocity;
    int point = 0;
    int finalPoint = 0;
    int bestPoint = 0;
    int countPipe = 0;
    int countCoinLevel1 = 0;
    public int pipePerLevel = 1;
    bool isStageBonus = false;
    int pointLevel = 0;
    int currentLevel = 1;
    int coin = 0;

    public Bird birdTrans;
    public GameObject[] birdTransObject;
    public Parallax parallax;
    public GameObject[] parallaxObject;
    public StageCoin stageCoin;
    public GameObject[] stageCoinObject;
    public Score scoreClass;
    public ScoreIn scoreInClass;
    public HighScoreIn highScoreInClass;
    public CharacterManager characterManager;
    public Coin coinClass;
    public Text coinSkinText;
    public LoobyUI lobbyUI;


    public CharacterDatabase characterDB;

    private Vector2 startPosition;

    public float minRotate;
    public float maxRotate;

    public float jumpPower = 0;
    public float movePower = 0;
    Rigidbody2D rb;

    private bool isOnGround;
    private int gameStart = 0;
    private bool isGameOver = false;
    private bool isPlaying = false;
    private static bool isReplay = false;

    public Pipe pp;
    public GameObject []p1;

    public float timeSpawn = 1;
    private float timeLoop = 0;

    public GameObject panelUI;
    public GameObject panelSkin;
    public GameObject panelMenu;
    public GameObject panelRoom;
    public GameObject buttonPlay;
    public GameObject buttonSkin;
    public GameObject buttonClosePanelSkin;
    public int score = 0;
    public GameObject backgroundPanelSkin;
    public GameObject selectedChar;
    public Button buttonSelectChar;
    public GameObject createRoom;

    public AudioSource audioSource;
    public AudioSource audioSourceCoin;
    public AudioClip jumpAudio;
    public AudioClip gameOverAudio;
    public AudioClip coinAudio;

    //================================ config multiplayer
    public Button buttonHost;
    public Button buttonJoin;

    private NetworkRunner _runner;


    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnDisconnectedFromServer(NetworkRunner runner) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }

    private void Awake()
    {
        // core = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        int optionChar = 0;
        if (characterManager.returnSprite() != null)
        {
            optionChar = characterManager.returnSprite();
        } else
        {
            optionChar = 0;
        }

        // PlayerPrefs.DeleteKey("birdSelected");
        // PlayerPrefs.DeleteKey("birdBought");
        // PlayerPrefs.DeleteAll();


        // default char is BLUE
        int indexLength = birdTransObject.Length;
        int selectOption = 0;
        for (int i = 0; i < indexLength; i++)
        {   
            if(charData(i).selected == 1)
            {
                selectOption = i;
            }
        }

        if(PlayerPrefs.GetInt("birdSelected") == 1 && PlayerPrefs.GetInt("birdBought") == 1)
        {
            selectOption = PlayerPrefs.GetInt("birdSelected");
        } else
        {
            PlayerPrefs.GetInt("birdSelected");
        }


        birdTrans = Instantiate(birdTransObject[PlayerPrefs.GetInt("birdSelected")], new Vector2(-0.079f, 0.45f), Quaternion.identity).GetComponent<Bird>();

        parallax = Instantiate(parallaxObject[PlayerPrefs.GetInt("birdSelected")], new Vector2(-5.413975f, -1.280665f), Quaternion.identity).GetComponent<Parallax>();


        // birdTrans.controller = this;

        isOnGround = true;
        rb = birdTrans.gameObject.GetComponent<Rigidbody2D>();


        rb.Sleep();

        scoreClass.gameObject.SetActive(false);

        bestPoint = PlayerPrefs.GetInt("bestPoint");

        coin = PlayerPrefs.GetInt("bestCoin");
        coinClass.updateCoin(coin);

        panelUI.gameObject.SetActive(false);
        panelSkin.gameObject.SetActive(false);
        panelMenu.gameObject.SetActive(false);
        panelRoom.gameObject.SetActive(false);
        createRoom.gameObject.SetActive(false);
        scoreInClass.finalUpdateScore(0);
        highScoreInClass.bestScore(bestPoint);

        audioSource.clip = jumpAudio;
        audioSource.Stop();
        audioSourceCoin.clip = coinAudio;
        audioSourceCoin.Stop();

        backgroundPanelSkin.gameObject.SetActive(false);
        selectedChar.gameObject.SetActive(false);

        
        
    }



    void Update()
    {
        var data = new NetworkInputData();
        if (isOnGround && Input.GetMouseButtonDown(0) && isPlaying == true)
        {
            rb.velocity = Vector2.up * jumpVelocity;
            
            // data.movement = Vector2.up * jumpVelocity;
            gameStart = 1;

            if (isGameOver == false)
            {
                audioSource.clip = jumpAudio;
                audioSource.Play();
            }
                
        }


        if (rb.velocity.y < 0.1f && rb.velocity.y != 0)
        // if (data.movement.y < 0.1f && data.movement.y != 0)
        {
            rotateDown();

        }
        else if (data.movement.y > 0.1f && data.movement.y != 0 && isGameOver == false)
        // else if (rb.velocity.y > 0.1f && rb.velocity.y != 0 && isGameOver == false)
                {
            rotateUp();
        }
        else
        {
            birdTrans.birdImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        if (isReplay == true)
        {
            isPlaying = true;
            buttonPlay.gameObject.SetActive(false);
            buttonSkin.gameObject.SetActive(false);
        }

        if (isOnGround && gameStart == 1 && isGameOver == false)
        {

            scoreClass.gameObject.SetActive(true);

            timeLoop += Time.deltaTime;


            if (isStageBonus == false)
            {
                if (timeLoop >= timeSpawn)
                {
                    //RESET Counter
                    timeLoop = 0;

                    if (countPipe < pipePerLevel)
                    {
                        if (currentLevel == 1)
                        {
                            spawnPipe();
                        }
                        else
                        {
                            spawnPipe2();
                        }

                    }

                    countPipe += 1;

                }

                if (pointLevel == pipePerLevel)
                {
                    isStageBonus = true;
                    StartCoroutine(setBonusStage(2));
                    currentLevel += 1;
                }

            }
            else
            {
                if (timeLoop >= timeSpawn)
                {
                    //RESET Counter
                    timeLoop = 0;

                    if (countCoinLevel1 < 1)
                    {
                        Debug.Log("currentLevel ELSE : " + currentLevel);

                        if (currentLevel == 2)
                        {
                            stageCoin = Instantiate(stageCoinObject[0], new Vector2(0, -1), Quaternion.identity).GetComponent<StageCoin>();
                        }
                        else if (currentLevel != 1)
                        {
                            stageCoin = Instantiate(stageCoinObject[1], new Vector2(0, -1), Quaternion.identity).GetComponent<StageCoin>();
                        }

                    }

                    countCoinLevel1 += 1;
                }
            }
        }

    }

    

    IEnumerator setBonusStage(float duration)
    {
        yield return new WaitForSeconds(duration);

        timeLoop = 0;
        countPipe = 0;
        pointLevel = 0;
        countCoinLevel1 = 0;

        this.isStageBonus = false;
    }

    private void spawnPipe()
    {
        Debug.Log("spawnPipe1");
        pp = Instantiate(p1[UnityEngine.Random.Range(0, p1.Length)], new Vector2(10, 0), Quaternion.identity).GetComponent<Pipe>();

        pp.parentPipe.transform.localPosition = new Vector3(0, UnityEngine.Random.Range(pp.batasBawah, pp.batasAtas), 0);
    }

    private void spawnPipe2()
    {
        Debug.Log("p1.Length : "+ p1.Length);
        pp = Instantiate(p1[UnityEngine.Random.Range(0, p1.Length)], new Vector2(UnityEngine.Random.Range(8, 10), 0), Quaternion.identity).GetComponent<Pipe>();

        pp.parentPipe.transform.localPosition = new Vector3(0, UnityEngine.Random.Range(pp.batasBawah, pp.batasAtas), 0);
    }

    public void rotateUp()
    {
        float smooth = 5.0f;

        // Rotate the cube by converting the angles into a quaternion.
        Quaternion target = Quaternion.Euler(0, 0, maxRotate);

        // Dampen towards the target rotation
        // birdTrans.birdImage.transform.rotation = Quaternion.Slerp(birdTrans.birdImage.transform.rotation, target, Time.deltaTime * smooth);

        var obj = new NetworkObject();
        obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, target, Time.deltaTime * smooth);

    }

    public void rotateDown()
    {
        float smooth = 5.0f;

        // Rotate the cube by converting the angles into a quaternion.
        Quaternion target = Quaternion.Euler(0, 0, minRotate);

        // Dampen towards the target rotation
        // birdTrans.birdImage.transform.rotation = Quaternion.Slerp(birdTrans.birdImage.transform.rotation, target, Time.deltaTime * smooth);

        var obj = new NetworkObject();
        obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, target, Time.deltaTime * smooth);
    }


    public void replay()
    {
        audioSource.Stop();
        Time.timeScale = 1;
        isReplay = true;
        SceneManager.LoadScene("BirdScene");
    }

    public void playGame()
    {
        isPlaying = true;
        buttonPlay.gameObject.SetActive(false);
        buttonSkin.gameObject.SetActive(false);

        // panelMenu.gameObject.SetActive(true);
    }

    public void skin()
    {
        panelSkin.gameObject.SetActive(true);
        backgroundPanelSkin.gameObject.SetActive(true);
        selectedChar.gameObject.SetActive(true);
        buttonPlay.gameObject.SetActive(false);
        buttonSkin.gameObject.SetActive(false);

        if(isGameOver == true)
        {
            panelUI.gameObject.SetActive(false);
        }

        int bestCoin = PlayerPrefs.GetInt("bestCoin");

        Character charSkin = charData(characterManager.returnSprite());

        coinSkinText.text = charSkin.coin.ToString();

        if (charSkin.characterName == "BLUE")
        {
            coinSkinText.text = "0";
        }


        if ((bestCoin < charSkin.coin) || (bestCoin < charSkin.coin))
        {
            // button text = Buy disable
            buttonSelectChar.GetComponentInChildren<Text>().text = "BUY";
            buttonSelectChar.GetComponentInChildren<Button>().interactable = false;

            if (charSkin.buy == 1 && PlayerPrefs.GetInt("birdBought_" + charSkin.characterName) != 0)
            {
                buttonSelectChar.GetComponentInChildren<Text>().text = "Select";
                buttonSelectChar.GetComponentInChildren<Button>().interactable = true;
            }
        }
        else if ((bestCoin > charSkin.coin) || (bestCoin > charSkin.coin))
        {
            // button text = Buy enable
            buttonSelectChar.GetComponentInChildren<Text>().text = "BUY";
            buttonSelectChar.GetComponentInChildren<Button>().interactable = true;

            // if (charSkin.buy == 1)
            if(charSkin.buy == 1 && PlayerPrefs.GetInt("birdBought_"+charSkin.characterName) != 0)
            {
                buttonSelectChar.GetComponentInChildren<Text>().text = "Select";
                buttonSelectChar.GetComponentInChildren<Button>().interactable = true;
            }
        }

        if (charSkin.characterName == "BLUE")
        {
            buttonSelectChar.GetComponentInChildren<Text>().text = "Select";
            buttonSelectChar.GetComponentInChildren<Button>().interactable = true;
        }

    }

    public void setButtonSelect(int selectOption)
    {
        Character charGlobal = charData(selectOption);
        Character charBlue = charData(0);
        Character charRed = charData(1);
        Character charGreen = charData(2);

        int bestCoin = PlayerPrefs.GetInt("bestCoin");

        if (charGlobal.characterName == "BLUE")
        {
            coinSkinText.text = "0";
            buttonSelectChar.GetComponentInChildren<Text>().text = "Select";
            buttonSelectChar.GetComponentInChildren<Button>().interactable = true;
        }


        if (charGlobal.characterName != "BLUE")
        {
            coinSkinText.text = charGlobal.coin.ToString();

            if ((bestCoin < charGlobal.coin) || (bestCoin < charGlobal.coin))
            {
                // button text = Buy disable
                buttonSelectChar.GetComponentInChildren<Text>().text = "BUY";
                buttonSelectChar.GetComponentInChildren<Button>().interactable = false;

                if (charGlobal.buy == 1 && PlayerPrefs.GetInt("birdBought_" + charGlobal.characterName) != 0)
                {
                    buttonSelectChar.GetComponentInChildren<Text>().text = "Select";
                    buttonSelectChar.GetComponentInChildren<Button>().interactable = true;
                }
            }
            else if ((bestCoin > charGlobal.coin) || (bestCoin > charGlobal.coin))
            {
                // button text = Buy enable
                buttonSelectChar.GetComponentInChildren<Text>().text = "BUY";
                buttonSelectChar.GetComponentInChildren<Button>().interactable = true;

                // if (PlayerPrefs.GetInt("birdBought") == 1)
                if(charGlobal.buy == 1 && PlayerPrefs.GetInt("birdBought_"+ charGlobal.characterName) != 0)
                {
                    buttonSelectChar.GetComponentInChildren<Text>().text = "Select";
                    buttonSelectChar.GetComponentInChildren<Button>().interactable = true;
                }
            }
        }

    }

    private Character charData(int option)
    {
        Character character = characterDB.GetCharacter(option);
        return character;
    }

    public void selectChar()
    {
        Character charGlobal = charData(characterManager.returnSprite());

        if (buttonSelectChar.GetComponentInChildren<Text>().text == "Select" && buttonSelectChar.GetComponentInChildren<Button>().interactable == true)
        {
            int indexLength = birdTransObject.Length;
            for (int i = 0; i < indexLength; i++)
            {
                charData(i).selected = 0;
            }

            charGlobal.selected = 1;

            PlayerPrefs.SetInt("birdSelected", characterManager.returnSprite());

        }
        else if (buttonSelectChar.GetComponentInChildren<Text>().text == "BUY" && buttonSelectChar.GetComponentInChildren<Button>().interactable == true)
        {
            int currentCoin = 0;

            // save coin after buy
            currentCoin = coin - charGlobal.coin;
            PlayerPrefs.SetInt("bestCoin", currentCoin);

            // set selected char after buy
            int indexLength = birdTransObject.Length;
            for (int i = 0; i < indexLength; i++)
            {
                Debug.Log("i : " + i);
                charData(i).selected = 0;
            }

            charGlobal.selected = 1;
            charGlobal.buy = 1;






            PlayerPrefs.SetInt("birdSelected", characterManager.returnSprite());
            PlayerPrefs.SetInt("birdBought_" + charGlobal.characterName, 1);

        }

        closePanelSkin();
        SceneManager.LoadScene("BirdScene");
    }

    public void closePanelSkin()
    {
        panelSkin.gameObject.SetActive(false);
        backgroundPanelSkin.gameObject.SetActive(false);
        selectedChar.gameObject.SetActive(false);
        buttonPlay.gameObject.SetActive(true);
        buttonSkin.gameObject.SetActive(true);

        if (isGameOver == true)
        {
            Time.timeScale = 1;


            isReplay = true;

            SceneManager.LoadScene("BirdScene");
        }
    }


    public void gameOver()
    {
        panelUI.gameObject.SetActive(true);
        if (bestPoint < finalPoint)
        {
            bestPoint = finalPoint;
            PlayerPrefs.SetInt("bestPoint", bestPoint);
            highScoreInClass.bestScore(bestPoint);
        }
        isGameOver = true;
        scoreClass.gameObject.SetActive(false);
        Time.timeScale = 0;

        audioSource.clip = gameOverAudio;
        audioSource.Play();

    }

    public int incrementPoint()
    {   
        point += 1;
        pointLevel += 1;
        scoreClass.updateScore(point);

        audioSourceCoin.clip = coinAudio;
        audioSourceCoin.Play();

        return point;
    }

    public int finalIncrementPoint()
    {
        finalPoint += 1;
        scoreInClass.finalUpdateScore(finalPoint);
        return finalPoint;
    }

    public int currentCoin()
    {
        coin += 1;
        PlayerPrefs.SetInt("bestCoin", coin);

        coinClass.updateCoin(coin);
        return coin;
    }

    //===================== setup multiplayer

    

    async void StartGame(GameMode mode)
    {
        Debug.Log("masukStartSession : "+mode);
        // Create the Fusion runner and let it know that we will be providing user input
        _runner = gameObject.AddComponent<NetworkRunner>();
        _runner.ProvideInput = true;

        // Start or join (depends on gamemode) a session with a specific name
        StartGameResult result = await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionName = "TestRoom",
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }

    [SerializeField] private bool _sharedMode;

    public void startSession()
    {
        Debug.Log("masukStartSession");
        StartGame(GameMode.Shared);
    }


    public void startHostMode()
    {
        /*if (_runner == null)
        {
            StartGame(GameMode.Host);    
        }*/

        // panelRoom.gameObject.SetActive(true);
        createRoom.gameObject.SetActive(true);
        // lobbyUI.Setup();
    }

    public void startJoinMode()
    {
        /*if (_runner == null)
        {
            StartGame(GameMode.Client);
        }*/

        List<SessionInfo> sessions = new List<SessionInfo>();

        foreach (SessionInfo info in sessions)
        {
            getSessionInfo(info);
        }

        Debug.Log("masukStartJoinMode");
    }

    public void getSessionInfo(SessionInfo info)
    {
        Debug.Log("masukgetSessionInfo");
    }

    [SerializeField] private NetworkPrefabRef _playerPrefab; // Character to spawn for a joining player
    private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {

            //Vector3 spawnPosition = new Vector3((player.RawEncoded % runner.Config.Simulation.DefaultPlayers) * 3, 1, 0);
            Vector3 spawnPosition = new Vector3(0, 0, 0);
            // Vector2 spawnPosition = new Vector2(0, 5);
            NetworkObject networkPlayerObject = runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);
            _spawnedCharacters.Add(player, networkPlayerObject);
            isPlaying = true;
        }
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        var data = new NetworkInputData();

        /*if (Input.GetKey(KeyCode.A))
            data.movement += Vector2.left;

        if (Input.GetKey(KeyCode.D))
            data.movement += Vector2.right;*/

        if (Input.GetMouseButtonDown(0))
            data.movement += Vector2.up * 9;

        input.Set(data);
    }

}
