using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afterimage : MonoBehaviour{
  Material m;
  MeshFilter mf = null;
  // GameObject afterimageObj = null;
  Coroutine fadeOutCoroutine = null;
  float originAlpha = 0f;
  
  // MeshFilter mf의 mesh를 가져옵니다.
  public Mesh mesh { get { return mf.mesh; } }
  
  
  public void InitAfterImage(Material material)
  {
    // afterImageObj = new GameObject();
    // mr에 MeshRenderer 컴포넌트를 넣어줍니다.
    MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();
    
    // m에 새로운 Material을 넣어줍니다.
    m = new Material(material);
    
    // originAlpha에 m의 투명도를 넣어줍니다.
    originAlpha = m.color.a;
    
    // mr의 메터리얼을 m으로 설정합니다.
    mr.material = m;
    
    // mf에 MeshFilter 컴포넌트를 넣어줍니다.
    mf = gameObject.AddComponent<MeshFilter>();
    
    // 오브젝트를 비활성화 합니다.
    gameObject.SetActive(false);
  }
  
  public void CreateAfterImage(Vector3 position, Quaternion rot, float time)
  { 
    // fadeoutCoroutine에 값이 없을 때 실행합니다.1
    if ( fadeoutCoroutine == null ) 
    {
      // 오브젝트를 활성화 합니다.
      gameObject.SetActive(true);
      
      // 오브젝트의 위치를 position으로 설정합니다.
      gameObject.transform.position = position;
      
      // 오브젝트의 방향을 rot으로 설정합니다.
      gameObject.transform.rotation = rot;
      
      // MeshFilter mf의 mesh를 mesh로 설정합니다.(mesh는 mf의 mesh를 가져옵니다.)
      mf.mesh = mesh;
      
      // fadeoutCoroutine에 FadeOut코루틴을 넣어줍니다.
      fadeoutCoroutine = StartCotoutine(FadeOut(time));
    }
  }
  
  
  IEnumerator FadeOut(float time)
  {
    // time이 0이상인 경우 반복
    while(time > 0f)
    {
      // time이 감소하도록 합니다.
      time -= Time.deltaTime;
      
      // 메터리얼 m의 알파값을 줄여줍니다.
      m.color = new Color(m.color.r, m.color.g, m.color.b, originAlpha * time);
      yield return null;
    }
    
    // 오브젝트를 비활성화합니다.
    gameObject.SetActive(false);
    
    // fadeoutCoroutine을 비워줍니다.
    fadeoutCoroutine = null;
  }
}
