using UnityEngine;

public class DeadArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BlueZero>() != null)
        {
            collision.transform.position = Vector2.zero;
            return;
        }
        if(collision.GetComponent<CharacterStats>() == null)
        {
            collision.GetComponent<CharacterStats>().Kill();
        }
    }
}
