using UnityEngine;

public static class CameraExtensionMethods
{
    public struct Borders
    {
        public float up;
        public float down;
        public float right;
        public float left;
    }

    public static void Follow(this Camera camera, GameObject target, float tension)
    {
        Stage stage = Object.FindObjectOfType<Stage>();
        Vector3 position = target.transform.position;

        Vector2 screenBounds = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camera.transform.position.z));
        screenBounds.x -= camera.transform.position.x;
        screenBounds.y -= camera.transform.position.y;

        float xMin = stage.m_borderLeft + screenBounds.x;
        float xMax = stage.m_borderRight - screenBounds.x;
        float yMin = stage.m_borderDown + screenBounds.y;
        float yMax = stage.m_borderUp - screenBounds.y;

        if (position.x < xMin) position.x = xMin;
        else if (position.x > xMax) position.x = xMax;
        if (position.y < yMin) position.y = yMin;
        else if (position.y > yMax) position.y = yMax;
        position.z = camera.transform.position.z;

        camera.transform.position = Vector3.Lerp(camera.transform.position, position, Time.fixedDeltaTime * tension);
    }
    public static Borders GetBorders(this Camera camera, GameObject content)
    {
        Borders screenBorders;

        Vector2 screenBounds = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camera.transform.position.z));
        screenBounds.x -= camera.transform.position.x;
        screenBounds.y -= camera.transform.position.y;

        float contentWidth = content.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        float contentHeight = content.GetComponent<SpriteRenderer>().bounds.size.y / 2;

        screenBorders.left = camera.transform.position.x - screenBounds.x + contentWidth;
        screenBorders.right = camera.transform.position.x + screenBounds.x - contentWidth;
        screenBorders.down = camera.transform.position.y - screenBounds.y + contentHeight;
        screenBorders.up = camera.transform.position.y + screenBounds.y - contentHeight;

        return screenBorders;
    }
}
