using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    
    [SerializeField] float lifeTime;
    [SerializeField] Vector3 dir;

    void Update() {
        transform.Translate(dir * Time.deltaTime);
    }

    public void StartBullet(bool type, Vector3 spawnPosition, bool toRigth) {
        Invoke(nameof(FinishUse), lifeTime);
        if (type) gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        else gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        if (!toRigth) dir *= -1;
        transform.position = spawnPosition;
    }

    void FinishUse() {
        Destroy(gameObject);
    }
}
