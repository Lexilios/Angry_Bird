using Unity.Cinemachine;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    private CinemachineTargetGroup targetGroup;
    private void Start()
    {
        targetGroup = GetComponent<CinemachineTargetGroup>();
    }
    public void SelectTarget(GameObject target)
    {
        targetGroup.Targets[0] = new CinemachineTargetGroup.Target { Object = target.transform, Weight = 1.0f, Radius = 0.5f };  
    }
}
