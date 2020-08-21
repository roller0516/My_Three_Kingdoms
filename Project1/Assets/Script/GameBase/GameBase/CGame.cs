using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//SceneManager.LoadScene(1);        //씬 로드하기

public class CGame : MonoBehaviour
{
    
    public string sMarket_id = "";
    public bool bGameFirst = false;

    public string sVersion_client = "0.1";  //클라이언트 버전
    public string sDevice = "";
    public string sLanguage = "Korean";	    //language 0: korean 1: english 2: japanese 3: chinese
    public string sCountry = "A1";          // ISO 3166-1 alpha-2

    //public CGameDef kDef;     //게임의 로직 클래스.

    public int SceneNumber_cur = 0;
    public bool bGameInit = false;
    public GameObject Root_ui = null;

    public CGameDef kDef;     //게임의 로직 클래스.

    //public MyPlayer kPlayer;

    //------------------------------------------------------------------------------------
    private static CGame s_instance = null; //싱글톤
    public static CGame Instance
    {
        get
        {
            if (s_instance == null)
            {
                //s_instance = new CGame();  //C#
                s_instance = FindObjectOfType(typeof(CGame)) as CGame;
            }
            return s_instance;
        }
    }

    
    void Awake()
    {
        if (s_instance != null)
        {
            //Debug.LogError("Cannot have two instances of CGame.");
            return;
        }
        s_instance = this;

        //Screen.SetResolution(720, 1280, true);  //해상도 강제 세팅
        //Input.multiTouchEnabled = false;    //멀티터치 끄기

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        DontDestroyOnLoad(this);    //씬전환할때 사라지지 않음
        
        kDef = (CGameDef)gameObject.AddComponent(typeof(CGameDef));
    }

    //public MyPlayer GetPlayer()    {        return MyServer.Instance.localGame.kPlayer;    }

    // Use this for initialization
    void Start()
    {
        print("CGame Start");

        StartCoroutine("GameStart");
    }

    IEnumerator GameStart()
    {
        //yield return new WaitForSeconds(1.0f);
        //yield return new WaitUntil(() => MyServer.Instance.bServerInit);

        //kPlayer = GetPlayer();

        //kDef.LocalDB_load();        //로컬 저장 데이타 로드

        bGameInit = true;   //print("bGameInit");

        //CGame.Instance.SceneChange(1);    

        yield return null;
    }

    

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.T))
        {
        }
#endif
    }

    private void OnApplicationQuit()
    {
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
    }


    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            Debug.Log("go to Background");
        }
        else
        {
            Debug.Log("go to Foreground");
        }
    }

    //------------------------------------------------------------------------------------------------
    // 씬 변경을 위한 호출함수.
    public void SceneChange(int number)
    {
        SceneManager.LoadScene(number);

        SceneNumber_cur = number;
        if (SceneNumber_cur != 0)
        {
            //로딩시 화면처리.
            //CGame.Instance.Show_Window("Prefab/WindowLoading", null);
            //GameObject kLoading = (GameObject)Instantiate(Resources.Load("prefab/screen_loading", typeof(GameObject)));
            //kLoading.transform.parent = Camera.main.transform;
            //kLoading.transform.localPosition = new Vector3( 0, 0, 0.5f ); //카메라 바로 앞.
        }
    }


    //------------------------------------------------------------------------------------------------
    // 리소스 이미지 로드.
    public Texture2D GetResourceTexture(string _imagename)
    {
        string imageName = _imagename; // "path/" + _imagename;
        Texture2D texture = (Texture2D)Resources.Load(imageName);
        return texture;
    }

    // GameObject 텍스처 변경.
    public void GameObject_set_texture(GameObject go, Texture2D _tx)
    {
        go.GetComponent<Renderer>().material.mainTexture = _tx;
        //go.GetComponent<Renderer>().material.color = new Color(1,1,1,1.0f);
    }

    // GameObject의 UI Image 의 sprite 변경
    public void GameObject_set_image(GameObject go, string _path) //"image/test"
    {
        //GameObject go = GameObject.FindGameObjectWithTag("userTag1");
        Image myImage = go.GetComponent<Image>();
        myImage.sprite = Resources.Load<Sprite>(_path) as Sprite;
    }

    // GameObject에 prefab을 로드
    public GameObject GameObject_from_prefab(string _prefab_name)
    {
        GameObject go = (GameObject)Instantiate(Resources.Load(_prefab_name, typeof(GameObject)));
        return go;
    }
    // GameObject에 prefab을 로드하여 어태치하기
    public GameObject GameObject_from_prefab(string _prefab_name, GameObject _parent)
    {
        GameObject go = (GameObject)Instantiate(Resources.Load(_prefab_name, typeof(GameObject)));
        if (_parent != null) go.transform.SetParent(_parent.transform);
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = Vector3.zero;
        return go;
    }

    // 객체의 이름을 통하여 자식 요소를 찾아서 리턴하는 함수 
    //UILabel _label = CGame.Instance.GameObject_get_child(obj, "_label").GetComponent<UILabel>();
    public GameObject GameObject_get_child(GameObject source, string strName)
    {
        Transform[] AllData = source.GetComponentsInChildren<Transform>(true); //비활성포함.

        GameObject target = null;

        foreach (Transform Obj in AllData)
        {
            if (Obj.name == strName)
            {
                target = Obj.gameObject;
                break;
            }
        }
        return target;
    }

    //객체에 붙은 Child를 제거
    public void GameObject_del_child(GameObject source)
    {
        Transform[] AllData = source.GetComponentsInChildren<Transform>(true); //비활성포함.
        foreach (Transform Obj in AllData)
        {
            if (Obj.gameObject != source) //자신 제외. 
            {
                Destroy(Obj.gameObject);
            }
        }
    }

    //오브젝트의 스크린좌표 // Screen.width
    public Vector3 GetWorldToScreenPoint( Transform target)
    {
        var wantedPos = Camera.main.WorldToScreenPoint(target.position);
        return wantedPos;
    }

    //오브젝트의 스크린좌표 // Canvas UI 
    //RectTransform rect = Canvas.GetComponent<RectTransform>();
    public Vector2 GetWorldToCanvasPoint(Transform target, RectTransform rect)
    {
        //월드 좌표를 스크린의 좌표로 변환
        var screenPos = Camera.main.WorldToScreenPoint(target.position);
        //카메라의 뒷쪽 영역(180도 회전)일 때 좌푯값 보정
        if (screenPos.z < 0.0f) screenPos *= -1.0f;
        //RectTransform 좌푯값을 전달받을 변수
        var localPos = Vector2.zero;
        //스크린 좌표를 RectTransform 기준의 좌표로 변환
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, screenPos, Camera.main, out localPos);
        return localPos;
    }

    //마우스 포인트에 타겟 피킹
    public GameObject GetRaycastObject()
    {
        RaycastHit hit;        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //마우스 포인트 근처 좌표를 만든다.                                 
        //Ray ray = Camera.main.ScreenPointToRay(new Vector3(640f,540f,0f));   //center        
        //var ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        //마우스 근처에 오브젝트가 있는지 확인
        GameObject target = null;
        if (true == (Physics.Raycast(ray.origin, ray.direction * 1000, out hit)))
        {
            target = hit.collider.gameObject; //있으면 오브젝트를 저장한다.
        }
        return target;
    }
    public Vector3 GetRaycastObjectPoint()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (true == (Physics.Raycast(ray.origin, ray.direction * 1000, out hit)))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    public GameObject GetRaycastObjectCenter()
    {
        RaycastHit hit;
        GameObject target = null;
        //Ray ray = Camera.main.ScreenPointToRay(new Vector3(640f,540f,0f));   //center
        var ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward); //카메라 //AR

        //마우스 근처에 오브젝트가 있는지 확인
        if (true == (Physics.Raycast(ray.origin, ray.direction * 1000, out hit)))
        {
            target = hit.collider.gameObject; //있으면 오브젝트를 저장한다.
        }
        return target;
    }

    public Vector3 GetVectorPos(Vector3 _from, Vector3 _to, float _distance) //특정방향 이동 위치.
    {
        Vector3 a_pos = _from;
        Vector3 b_pos = _to;
        Vector3 dir = (b_pos - a_pos).normalized;
        Vector3 pos = a_pos + (dir * _distance);  //speed * Time.deltaTime
        return pos;
    }

    public Vector3 GetVectorFoward(Transform _from, float _distance) //특정방향 이동 위치.
    {
        Vector3 pos = _from.position + _from.forward * _distance;
        return pos;
    }

    //Vector3 pos = Vector3.Lerp(enemy.transform.position, transform.position, 0.3f); //둘사이 일정 위치 
    //transform.LookAt(Camera.main.transform, Camera.main.transform.up);    //카메라보기.
    
    // 윈도우 팝업 ---------------------------------------------------------------------------------------
    //CGame.Instance.Root_ui = GameObject.Find("Canvas_window"); //scene 초기화 할 때.
    //CGame.Instance.Window_notice("notice message", rt => { if (rt == "0") print("notice ok");  });
    public void Window_notice(string _msg, Action<string> _callback)
    {
        GameObject go = GameObject.Instantiate(Resources.Load("prefab/Window_notice"), Vector3.zero, Quaternion.identity) as GameObject;
        CGame.Instance.Root_ui = GameObject.Find("Canvas_window"); //ui root
        go.transform.SetParent(Root_ui.transform);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;

        CWindowNotice w = go.GetComponent<CWindowNotice>();
        w.Show(_msg, _callback);
    }
    public void Window_ok(string _msg, Action<string> _callback)
    {
        GameObject go = GameObject.Instantiate(Resources.Load("prefab/Window_ok"), Vector3.zero, Quaternion.identity) as GameObject;
        CGame.Instance.Root_ui = GameObject.Find("Canvas_window"); //ui root
        go.transform.SetParent(Root_ui.transform);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;

        CWindowOk w = go.GetComponent<CWindowOk>();
        w.Show(_msg, _callback);
    }
    public GameObject Window_yesno(string _title, string _msg, Action<string> _callback)
    {
        GameObject go = GameObject.Instantiate(Resources.Load("prefab/Window_yesno"), Vector3.zero, Quaternion.identity) as GameObject;
        CGame.Instance.Root_ui = GameObject.Find("Canvas_window"); //ui root
        go.transform.SetParent(Root_ui.transform);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;

        CWindowYesNo w = go.GetComponent<CWindowYesNo>();
        w.Show(_title, _msg, _callback);
        return go;
    }

    public void Window_input(string _msg, Action<string> _callback)
    {
        GameObject go = GameObject.Instantiate(Resources.Load("prefab/Window_input"), Vector3.zero, Quaternion.identity) as GameObject;
        CGame.Instance.Root_ui = GameObject.Find("Canvas_window"); //ui root
        go.transform.SetParent(Root_ui.transform);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;

        CWindowInput w = go.GetComponent<CWindowInput>();
        w.Show(_msg, _callback);
    }
    /*
        public void Window_input2(string _msg, Action<string> _callback)
        {
            GameObject go = GameObject.Instantiate(Resources.Load("prefab/Window_input2"), Vector3.zero, Quaternion.identity) as GameObject;
            CGame.Instance.Root_ui = GameObject.Find("Canvas_window"); //ui root
            go.transform.SetParent(Root_ui.transform);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;

            CWindowInput2 w = go.GetComponent<CWindowInput2>();
            w.Show(_msg, _callback);
        }

        public void Window_wait(float _sec, Action<string> _callback)
        {
            GameObject go = GameObject.Instantiate(Resources.Load("prefab/Window_wait"), Vector3.zero, Quaternion.identity) as GameObject;
            CGame.Instance.Root_ui = GameObject.Find("Canvas_window"); //ui root
            go.transform.SetParent(Root_ui.transform);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
            CWindowWait w = go.GetComponent<CWindowWait>();
            w.Show(_sec, _callback);
        }
        public void Window_FakeScreen(string _msg, Action<string> _callback)
        {
            GameObject go = GameObject.Instantiate(Resources.Load("prefab/Window_FakeScreen"), Vector3.zero, Quaternion.identity) as GameObject;
            CGame.Instance.Root_ui = GameObject.Find("Canvas_window"); //ui root
            go.transform.SetParent(Root_ui.transform);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
            CWindowOk w = go.GetComponent<CWindowOk>();
            w.Show(_msg, _callback);
        }

    */

    public void IconImage_set(Image icon_image, int _item_index)
    {
        TableInfo_item table = CGameTable.Instance.Get_TableInfo_item(_item_index);
        string _path = table.icon;
        icon_image.sprite = Resources.Load<Sprite>(_path) as Sprite;
    }


}

/*
    StartCoroutine("StartCoroutineFunc");     //코루틴 사용하기
    IEnumerator StartCoroutineFunc()
    {
        yield return new WaitForSeconds(5.0f);
    }
/*
     //보상, 리워드객체 생성. -----------------------------------------------------------------------------
    public RewardInfo Reward_create(int _index, int _count, int _star, int _level)
    {
        RewardInfo _reward = new RewardInfo();
        _reward.index = _index;
        _reward.count = _count;
        _reward.star = _star;
        _reward.level = _level;
        return _reward;
    }
*/
