using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class PhysicsTool
{
    [Obsolete]
    /// <summary>
    /// 锥形扫描范围
    /// </summary>
    /// <param name="origin"> 扫描的原点 </param>
    /// <param name="direction"> 扫描的方向 </param>
    /// <param name="angleDegree"> 扫描的角度 </param>
    /// <param name="generatrix"> 扫描的长度 </param>
    /// <param name="segments"> 扫描的精度 </param>
    /// <param name="tag"> 标签 </param>
    /// <returns></returns>
    public static GameObject ConeScanOneOfTag(Vector3 origin, Vector3 direction, float angleDegree, float generatrix, int segments, string tag)
    {
        if (segments == 0)
        {
            segments = 1;
            Debug.Log("精度需要大于0");
        }
        if (angleDegree > 180)
        {
            angleDegree %= 180;
            Debug.Log("锥形角请在180度以内");
        }
        RaycastHit raycastHit;
        //常量
        Vector3 tangent = new Vector3(direction.normalized.y,-direction.normalized.x,0);
        Vector3 OrthVector = new Vector3(
            direction.normalized.x * direction.normalized.z,
            direction.normalized.y * direction.normalized.z,
            -(direction.normalized.x * direction.normalized.x + direction.normalized.y * direction.normalized.y));
        float deltaAngle = 360 / segments; //每个扫描面相隔射线的夹角
        float varyAngle = angleDegree / (2 * segments); //每个锥形的与法向量的夹角
        //变量
        float height; //圆锥的高s
        float bilgeRadius; //底边半径
        Vector3 circleCenter = new Vector3();//圆心
        Vector3 currentDirection; //射出射线的向量
        bool hit;

        for (int i = 1; i < ((segments / 2) + 1); i++) // 圆锥
        {
            bilgeRadius = generatrix * Mathf.Sin(varyAngle * i);
            height = generatrix * Mathf.Cos(varyAngle * i);
            circleCenter.x = origin.x + height * direction.normalized.x;
            circleCenter.y = origin.y + height * direction.normalized.y;
            circleCenter.z = origin.z + height * direction.normalized.z;
            for (int j = 0; j < ((segments / 2) + 1); j++) // 斜线
            {
                currentDirection.x = circleCenter.x +
                    bilgeRadius * (tangent.x * Mathf.Cos(deltaAngle * j) + OrthVector.x * Mathf.Sin(deltaAngle * j));
                currentDirection.y = circleCenter.y +
                    bilgeRadius * (tangent.y * Mathf.Cos(deltaAngle * j) + OrthVector.y * Mathf.Sin(deltaAngle * j));
                currentDirection.z = circleCenter.z +
                    bilgeRadius * (tangent.z * Mathf.Cos(deltaAngle * j) + OrthVector.z * Mathf.Sin(deltaAngle * j));
                currentDirection = currentDirection - origin;
                hit = Physics.Raycast(origin, currentDirection, out raycastHit, generatrix);
                Debug.DrawRay(origin, currentDirection, Color.yellow, 50f);
                if (hit)
                {
                    if (raycastHit.transform.gameObject.tag == tag)
                    {
                        return raycastHit.transform.gameObject;
                    }
                }
            }
        }
        return null;
    }

    [Obsolete]
    public static GameObject ConeScanOneOfHashCode(Vector3 origin, Vector3 direction, float angleDegree, float generatrix, int segments, int hashCode)
    {
        if (segments == 0)
        {
            segments = 1;
            Debug.Log("精度需要大于0");
        }
        if (angleDegree > 180)
        {
            angleDegree %= 180;
            Debug.Log("锥形角请在180度以内");
        }
        RaycastHit raycastHit;
        //常量
        Vector3 tangent = new Vector3(direction.normalized.y, -direction.normalized.x, 0);
        Vector3 OrthVector = new Vector3(
            direction.normalized.x * direction.normalized.z,
            direction.normalized.y * direction.normalized.z,
            -(direction.normalized.x * direction.normalized.x + direction.normalized.y * direction.normalized.y));
        float deltaAngle = 360 / segments; //每个扫描面相隔射线的夹角
        float varyAngle = angleDegree / (2 * segments); //每个锥形的与法向量的夹角
        //变量
        float height; //圆锥的高s
        float bilgeRadius; //底边半径
        Vector3 circleCenter = new Vector3();//圆心
        Vector3 currentDirection; //射出射线的向量
        bool hit;

        for (int i = 1; i <= ((segments / 1) + 0); i++) // 圆锥
        {
            bilgeRadius = generatrix * Mathf.Sin(varyAngle * i);
            height = generatrix * Mathf.Cos(varyAngle * i);
            circleCenter.x = height * direction.normalized.x - origin.x;
            circleCenter.y = height * direction.normalized.y - origin.y;
            circleCenter.z = height * direction.normalized.z - origin.z;
            for (int j = 1; j <= segments; j++) // 斜线
            {
                currentDirection.x = circleCenter.x +
                    bilgeRadius * (tangent.x * Mathf.Cos(deltaAngle * j) + OrthVector.x * Mathf.Sin(deltaAngle * j));
                currentDirection.y = circleCenter.y +
                    bilgeRadius * (tangent.y * Mathf.Cos(deltaAngle * j) + OrthVector.y * Mathf.Sin(deltaAngle * j));
                currentDirection.z = circleCenter.z +
                    bilgeRadius * (tangent.z * Mathf.Cos(deltaAngle * j) + OrthVector.z * Mathf.Sin(deltaAngle * j));
                currentDirection = currentDirection - origin;
                hit = Physics.Raycast(origin, currentDirection.normalized, out raycastHit, generatrix);
                Debug.DrawRay(origin, currentDirection.normalized, Color.yellow, 50f); //扫秒绘制
                if (hit)
                {
                    if (raycastHit.transform.gameObject.GetHashCode() == hashCode)
                    {
                        return raycastHit.transform.gameObject;
                    }
                }
            }
        }
        return null;
    }

    public static GameObject ConeHashCode(Vector3 origin, Vector3 direction, float angleDegree, float generatrix, int segments, int hashCode)
    {
        if (segments == 0)
        {
            segments = 1;
            Debug.Log("精度需要大于0");
        }
        if (angleDegree > 180)
        {
            angleDegree %= 180;
            Debug.Log("锥形角请在180度以内");
        }        
        float varyAngle = angleDegree / (2 * segments); //每个锥形的与法向量的夹角
        float height = generatrix *Mathf.Cos(varyAngle); ; //圆锥的高s
        float bilgeRadius = generatrix * Mathf.Sin(varyAngle); ; //底边半径
        RaycastHit raycastHit;
        bool hit;
        hit = Physics.SphereCast(origin,bilgeRadius,direction.normalized, out raycastHit, height);
        if (hit)
        { 
            Vector3 GOposition = raycastHit.transform.position;
            float IntersectionAngle = Vector3.Angle((GOposition - origin), direction) % varyAngle;            
            if ( IntersectionAngle < 0.1f)
            {
                return raycastHit.transform.gameObject;
            }
        }   
        return null;
    }
}
