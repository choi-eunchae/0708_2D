using System.Collections;
using JetBrains.Annotations;
using UnityEditor.Build.Content;
using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f; //핀이 위로 이동하는 속도
    private bool IsPinned = false; //핀이 타켓에 고정되었는지 여부
    private bool IsLaunchered = false; //핀이 발사되었는지 여부
    public GameObject BoomSprite;


    //FixedUpdate는 일정한 시간 간격으로 호출됨 (물리 연산에 적합)
    private void FixedUpdate()
    {
        //핀이 아직 고정되지 않았고 발사된 상태라면 위로 이동
        if (IsPinned == false && IsLaunchered == true)
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }
    }

    //다른 오브젝트와 충돌 시 호출
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsPinned = true; //충돌 시 핀을 고정 상태로 변경
        if (collision.gameObject.tag == "TargetCircle")
        {
            GameObject childObject = transform.Find("Square").gameObject;
            childObject.GetComponent<SpriteRenderer>().enabled = true;
            transform.SetParent(collision.gameObject.transform);
            GameManager.instance.DecreaseGoal();
        }
        else if (collision.gameObject.tag == "Pin")
        {
            // 다른 핀과 충돌 시 게임 오버
            Destroy(collision.gameObject);
            GameManager.instance.SetGameOver(false);
            Boom();
        }
    }

    public void Launch()
    {
        IsLaunchered = true;

    }
    private void Boom()
    {
        float force = 250f;
        float offset = 0.2f;

        for (int i = 0; i < 6; i++)
               {
            Vector3 spawnPos = transform.position + new Vector3((i < 3 ? -offset : offset), 0, 0);
            GameObject shard = Instantiate(BoomSprite, spawnPos, Quaternion.identity);

            Rigidbody2D rb = shard.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 dir = (i < 3) ? new Vector2(-3, 3) : new Vector2(3, 3);
                rb.AddForce(dir.normalized * force);
            }

            // 자동 제거 (옵션): 2초 후 파편 제거
            Destroy(shard, 2f);
        }
    }

}
