using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SACharacterLookTarget : MonoBehaviour
{
    [SerializeField]
    private Transform target = null;

    //[SerializeField]
    //private Transform debugTarget = null;

    [SerializeField]
    private Animator ani = null;

    private void Start()
    {

    }

    private void LateUpdate()
    {
        //"Body_Vertical_f"
        //"Body_Horizontal_f"

        //transform.worldToLocalMatrix


        //target.position

        var localPosition = transform.worldToLocalMatrix.MultiplyPoint3x4(target.position);
        var distanceVector = localPosition;

        //transform.forward
        {
            var ignoreYForward = Vector3.forward;
            var ignoreYVector = distanceVector;
            ignoreYForward.y = ignoreYVector.y = 0;

            ignoreYForward = Vector3.Normalize(ignoreYForward);
            ignoreYVector = Vector3.Normalize(ignoreYVector);

            var cross = Vector3.Cross(ignoreYForward, ignoreYVector);
            var sin = cross.magnitude;
            var degree = Mathf.Asin(sin) * (cross.y >= 0 ? 1 : -1);

            ani.SetFloat("Body_Horizontal_f", degree);
        }

        {
            var ignoreXForward = Vector3.forward;
            var ignoreXVector = distanceVector;
            ignoreXForward.x = ignoreXVector.x = 0;

            ignoreXForward = Vector3.Normalize(ignoreXForward);
            ignoreXVector = Vector3.Normalize(ignoreXVector);

            var cross = Vector3.Cross(ignoreXForward, ignoreXVector);
            var sin = cross.magnitude;
            var degree = Mathf.Asin(sin) * (cross.x >= 0 ? -1 : 1);

            ani.SetFloat("Body_Vertical_f", degree);
        }


    }
}
