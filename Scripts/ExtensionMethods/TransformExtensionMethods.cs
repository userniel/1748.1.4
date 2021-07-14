using UnityEngine;

public static class TransformExtensionMethods
{
    public static Vector2 GetLocalPosition(this Transform transform)
    {
        return new Vector2(transform.localPosition.x, transform.localPosition.y);
    }
    public static Vector2 GetPosition(this Transform transform)
    {
        return new Vector2(transform.position.x, transform.position.y);
    }
    public static void SetLocalPositionX(this Transform transform, float x)
    {
        transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
    }
    public static void SetLocalPositionY(this Transform transform, float y)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
    }
    public static void SetLocalPositionZ(this Transform transform, float z)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
    }
    public static void SetLocalPosition(this Transform transform, float x, float y)
    {
        transform.localPosition = new Vector3(x, y, transform.localPosition.z);
    }
    public static void SetLocalPosition(this Transform transform, Vector2 localPosition)
    {
        transform.localPosition = new Vector3(localPosition.x, localPosition.y, transform.localPosition.z);
    }
    public static void SetPositionX(this Transform transform, float x)
    {
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
    public static void SetPositionY(this Transform transform, float y)
    {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
    public static void SetPositionZ(this Transform transform, float z)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }
    public static void SetPosition(this Transform transform, float x, float y)
    {
        transform.position = new Vector3(x, y, transform.localPosition.z);
    }
    public static void SetPosition(this Transform transform, Vector2 position)
    {
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }
    public static void TranslateX(this Transform transform, float x)
    {
        transform.Translate(x, 0.0f, 0.0f);
    }
    public static void TranslateY(this Transform transform, float y)
    {
        transform.Translate(0.0f, y, 0.0f);
    }
    public static void TranslateZ(this Transform transform, float z)
    {
        transform.Translate(0.0f, 0.0f, z);
    }
    public static void Translate(this Transform transform, float x, float y)
    {
        transform.Translate(x, y, 0.0f);
    }
    public static void Translate(this Transform transform, Vector2 translation)
    {
        transform.Translate(translation.x, translation.y, 0.0f);
    }
    public static Vector3 GetLocalRotation(this Transform transform)
    {
        return new Vector3(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z);
    }
    public static void SetLocalRotation(this Transform transform, float x, float y)
    {
        transform.localRotation = Quaternion.Euler(new Vector3(x, y, transform.localRotation.z));
    }
    public static void SetLocalRotation(this Transform transform, Vector2 localRotation)
    {
        transform.localRotation = Quaternion.Euler(new Vector3(localRotation.x, localRotation.y, transform.localRotation.z));
    }
    public static void SetLocalRotation(this Transform transform, float z)
    {
        transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.x, transform.localRotation.y, z));
    }
    public static Vector2 GetLocalScale(this Transform transform)
    {
        return new Vector2(transform.localScale.x, transform.localScale.y);
    }
    public static void SetLocalScaleX(this Transform transform, float x)
    {
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }
    public static void SetLocalScaleY(this Transform transform, float y)
    {
        transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);
    }
    public static void SetLocalScale(this Transform transform, float xy)
    {
        transform.localScale = new Vector3(xy, xy, transform.localScale.z);
    }
    public static void SetLocalScale(this Transform transform, float x, float y)
    {
        transform.localScale = new Vector3(x, y, transform.localScale.z);
    }
    public static void SetLocalScale(this Transform transform, Vector2 localScale)
    {
        transform.localScale = new Vector3(localScale.x, localScale.y, transform.localScale.z);
    }
    public static void Reset(this Transform transform)
    {
        transform.position = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }
}
