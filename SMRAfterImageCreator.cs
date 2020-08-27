using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMRAfterImageCreator : MonoBehaviour
{
  [SerializeField]
  Material afterImageMeterial;
  
  SkinnedMeshRenderer smr;
  Afterimage[] afterImages;
  
  int afterImageCount;
  int currentAfterImageIndex;
  float remainAfterImageTime;
  float creatAfterImageDelay;
  Coroutine creatAfterImageCoroutine = null;
  
  bool isCreating = false;
  
  public void Setup(SkinnedMeshRenderer smr, int maxNumber, float remainTime)
  {
    // 이 클래스의 smr에 인자 smr을 넣어줍니다.
    this.smr = smr;
    
    // 잔상 이미지 수를 maxNumber로 설정합니다.
    afterImageCount = maxNumber;
    
    // 잔상 유지 시간을 remainTime으로 설정합니다.
    remainAfterImageTime = remainTime;
    
    // 잔상 생성 딜레이를 설정합니다.
    creatAfterImageDelay = remainAfterImageTime / (float)afterImageCount + 0.1f;
    
    // 잔상을 만드는 함수를 호출합니다.
    CreatAfterImages();
  }
  
  void CreatAfterImages()
  {
    // 잔상을 afterImageCount 수만큼 생성합니다.
    afterImages = new Afterimage[afterImageCount];
    
    
    fot (int i = 0; i < afterImages.Length; i++)
    {
      // 오브젝트 newObj를 만듭니다.
      GameObject newObj = new GameObject();
      
      // afterImages i 번째 인덱스에 Afterimage 컴포넌트를 추가합니다.
      afterImages[i] = newObj.AddComponent<Afterimage>();
      
      // afterImageMaterial을 인자값으로 넣어 InitAfterImage 함수를 실행합니다.
      afterImages[i].InitAfterImage(afterImageMaterial);
    }
  }
  
  
}
