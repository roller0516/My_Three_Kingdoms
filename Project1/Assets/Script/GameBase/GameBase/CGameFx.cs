
using UnityEngine;
using System.Collections;

// fx table index ------------------------------------------
public enum eFx
{
    fx_touch    = 100,
    fx_attack_0 = 1000,    //fx.
	fx_hit      = 1001,
	fx_die      = 1002,
    end
};

public class CGameFx : MonoBehaviour
{	
	ArrayList SourceArray = new ArrayList();
	ArrayList CloneArray = new ArrayList();
	
	GameObject kRoot;

	private static CGameFx s_Instance = null;
	
	public static CGameFx Instance
	{
		get 
	    {
	    	if (s_Instance == null)
	        {
	        	s_Instance //= new CGameFx();				
					= FindObjectOfType(typeof(CGameFx)) as CGameFx;
	        }
	        return s_Instance;
	    }
	}

	void Awake () 
	{
		if(s_Instance != null)
		{
			//Debug.LogError("Cannot have two instances of CGameFx.");
			return;
		}
		s_Instance = this;

		DontDestroyOnLoad(this);
        //Debug.Log("CGameFx Awake");

        kRoot = GameObject.Find("root_fx"); //fx 어태치 포인트를 만들어 놓는다.
        if (kRoot == null)
        {
            Debug.Log("Cannot find root_fx");
            //kRoot = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //kRoot.name = "root_fx";            
        }
    }

	// Use this for initialization ------------------------------------------------------------
	void Start () 
    {

    }
	
	public void LoadSource()
	{
        //AddSource((int)eFx.fx_touch);
        //AddSource((int)eFx.fx_logo);

    }

/*	
	// Update is called once per fram e--------------------------------------------------------
	void Update () {
	
		//if( Input.GetKeyDown(KeyCode.T) )
		//{
			//Vector3 pos = new Vector3(2, 0, 0);
			//AddClone( 1 , pos );
			
			//RemoveAllClone();

            //print(" " + CloneArray.Count);
		//}
	}
*/
	//------------------------------------------------------------------------------------------
	// Add Fx Source
	GameObject AddSource( int _index, string _name = "")
	{
		GameObject kGO;

        string prefab_name = "fx/" + _name;       //테이블없이 등록. 1000 이상으로하자.
        //Debug.Log("Fx AddSource Load: " + szPrefab + "     index: " + _index);

        GameObject go_fx = (GameObject)Resources.Load(prefab_name, typeof(GameObject));
        if (go_fx == null) {
            Debug.Log("ERROR: CGameFx AddSource Load Failed: " + prefab_name);
            return null;
        }
        kGO = Instantiate(go_fx);

		//kGO.tag = "FxObject";	// tagManager
		//kGO.name = "FX_" + _index;
		//kGO.transform.parent = Camera.main.transform;
        kGO.transform.parent = kRoot.transform;
		kGO.transform.localPosition = new Vector3(0, 100, 0); 
		kGO.transform.localScale = new Vector3(1,1,1);
        //kGO.layer = 9;
        //if(kGO.transform.GetChild(0)) kGO.transform.GetChild(0).gameObject.layer = 9;
        //if(kGO.transform.GetChild(0).GetChild(0)) kGO.transform.GetChild(0).GetChild(0).gameObject.layer = 9;

		CFxInfo kInfo = (CFxInfo)kGO.GetComponent("CFxInfo");
		if( kInfo == null )	kInfo = (CFxInfo)kGO.AddComponent<CFxInfo>();
		kInfo.iID = kGO.GetInstanceID();
		kInfo._index = _index;
		//Debug.Log( kInfo.iID );

		SourceArray.Add( kGO );
		
		return kGO;
	}

	GameObject GetSource( int _index)
	{
		foreach ( GameObject fxGO in SourceArray )			
		{
            if (fxGO == null) continue;
			CFxInfo kInfo = (CFxInfo)fxGO.GetComponent("CFxInfo");			
			if( kInfo._index == _index )
			{
				return fxGO;
			}
		}	
		return null;
	}
	
	// Add Fx Clone	
	GameObject AddClone( int _index, Vector3 pos )
	{
		GameObject kGO = GetSource( _index ); 
        if ( kGO == null) kGO = AddSource( _index ); //Instantiate
        if (kGO == null) return null;

		GameObject fxCloneGO = Instantiate( kGO );
        fxCloneGO.transform.parent = kRoot.transform;
        fxCloneGO.transform.position = pos;

		CFxInfo kInfo = (CFxInfo)fxCloneGO.GetComponent("CFxInfo");
        if (kInfo == null) kInfo = (CFxInfo)fxCloneGO.AddComponent<CFxInfo>();
		kInfo.iID 			= fxCloneGO.GetInstanceID();
		kInfo._index 	= _index;
		kInfo.bRemove		= false;
		//Debug.Log( kInfo.iID );

		CloneArray.Add( fxCloneGO );
			
		return fxCloneGO;
	}
	
	// Add Fx Clone	
	GameObject AddClone( string szPrefab, Vector3 pos )
	{
        //print("fx AddClone : " + szPrefab);

        GameObject fxCloneGO = null;
        GameObject go_fx = (GameObject)Resources.Load(szPrefab, typeof(GameObject));
        if (go_fx == null)
        {
            Debug.Log("ERROR: CGameFx AddClone Load Failed: " + szPrefab);
            return null;
        }
        fxCloneGO = Instantiate(go_fx);
        fxCloneGO.transform.parent = kRoot.transform;
        fxCloneGO.transform.position = pos;

		CFxInfo kInfo = (CFxInfo)fxCloneGO.GetComponent("CFxInfo");
		if( kInfo == null )	kInfo = (CFxInfo)fxCloneGO.AddComponent<CFxInfo>();
		kInfo.iID 			= fxCloneGO.GetInstanceID();
		kInfo._index 	= 0;
		kInfo.bRemove		= false;
		//Debug.Log( kInfo.iID );		
		
		CloneArray.Add( fxCloneGO );
		
		return fxCloneGO;
	}

	int GetID( GameObject fxGO)
	{
		CFxInfo kInfo = (CFxInfo)fxGO.GetComponent("CFxInfo");
		return kInfo.iID;
	}

	public void RemoveClone( GameObject fxGO )
	{
		CloneArray.Remove( fxGO );		
		Destroy (fxGO); 
	}
	
	public void RemoveAllClone()
	{
		foreach ( Object obj in CloneArray )
			Destroy (obj); 
		CloneArray.Clear();
	}
	
	public void RemoveAll()
	{
		//GameObject[] fxGOs = GameObject.FindGameObjectsWithTag ("FxObject");
		//foreach ( Object obj in fxGOs )
		//	Destroy (obj); 
		
		RemoveAllClone();
		
		foreach ( Object obj in SourceArray )
			Destroy (obj); 
		SourceArray.Clear();
	}
	
	public void PlayMovie( string szPrefab )	
	{
#if UNITY_IPHONE || UNITY_ANDROID	
		Handheld.PlayFullScreenMovie ( szPrefab, Color.black, FullScreenMovieControlMode.CancelOnInput);
#else	
		
#endif			
	}
	
	//------------------------------------------------------------------------------------------
    // 소스등록 없이 출력. 일회 소모용.
	public GameObject PlayFx(string szPrefab, Vector3 pos, float _lifetime = 3.0f )
	{
		//Debug.Log("PlayFx : " + szPrefab); 

		GameObject fxCloneGO = AddClone( szPrefab, pos );
		if(fxCloneGO == null )	return null;
		
		CFxInfo kInfo = (CFxInfo)fxCloneGO.GetComponent("CFxInfo");		
        kInfo.fTime_fx_remove = _lifetime;
		kInfo.bRemove = true;

        //ParticleSystemRenderer _renderer = GetComponent<ParticleSystemRenderer>();
        //if (_renderer != null)
        //    GetComponent<Renderer>().sharedMaterial.renderQueue = GetComponentInParent<UIPanel>().startingRenderQueue;

        return fxCloneGO;
	}

	//------------------------------------------------------------------------------------------	
    // 소스등록 후 사용. 반복 사용.
	public GameObject PlayFx(int _index, Vector3 pos, float _lifetime )
	{
		GameObject fxCloneGO = AddClone( _index, pos );
		if(fxCloneGO == null )	return null;
        				
		CFxInfo kInfo = (CFxInfo)fxCloneGO.GetComponent("CFxInfo");		
		kInfo.fTime_fx_remove = _lifetime;
		kInfo.bRemove = true;

        ParticleSystemRenderer _renderer = null;
        _renderer = fxCloneGO.GetComponent<ParticleSystemRenderer>();
        if (_renderer != null)
        {
            fxCloneGO.GetComponent<Renderer>().sharedMaterial.renderQueue = 3000;
            //fxCloneGO.GetComponent<Renderer>().sharedMaterial.renderQueue = GetComponentInParent<UIPanel>().startingRenderQueue;
        }

        return fxCloneGO;
	}

	//------------------------------------------------------------------------------------------
/*	
	public void FadeIn()
	{
	
		GameObject kFx = (GameObject)Instantiate( (GameObject)Resources.Load("fx/curtain", typeof(GameObject) ) );
		kFx.transform.parent = Camera.mainCamera.transform;
		kFx.transform.localPosition = new Vector3(0,0,5);
		
		EffectControl kEC = (EffectControl)kFx.transform.GetComponent("EffectControl");		
		kEC.FadeIn( 1.0f, EffectControl_End.Destroy);
	}
	
	public void FadeOut()
	{	
		GameObject kFx = (GameObject)Instantiate( (GameObject)Resources.Load("fx/curtain", typeof(GameObject) ) );
		kFx.transform.parent = Camera.mainCamera.transform;
		kFx.transform.localPosition = new Vector3(0,0,5);
		
		EffectControl kEC = (EffectControl)kFx.transform.GetComponent("EffectControl");
		kEC.FadeOut( 1.0f, EffectControl_End.Destroy);
	}	
	public void FadeOut2()
	{	
		GameObject kFx = (GameObject)Instantiate( (GameObject)Resources.Load("fx/curtain", typeof(GameObject) ) );
		kFx.transform.parent = Camera.mainCamera.transform;
		kFx.transform.localPosition = new Vector3(0,0,5);
		
		EffectControl kEC = (EffectControl)kFx.transform.GetComponent("EffectControl");
		
		Color _to = new Color( 0.0f,0.0f,0.0f, 0.8f);
		
		kEC.Fade( 1.5f, Color.clear, _to, EffectControl_End.Do_Nothing);
	}	
*/	
}

public class CFxInfo : MonoBehaviour
{
    public int iID = 0;
    public int _index = 0;
    public bool bLoop = false;
    public float fTime_fx_remove = 3.0f;
    public bool bRemove = false;

    void Update()
    {
        if (bRemove)
        {
            fTime_fx_remove -= Time.deltaTime;
            if (fTime_fx_remove < 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }
}

/*
        if (Input.GetMouseButtonDown(0))
        {
            var worldMousePosition = CGame.Instance.GetRaycastObjectPoint(); worldMousePosition.z = 0;
            CGameFx.Instance.PlayFx("Fx/CFX_MagicPoof", worldMousePosition);
        }
*/
