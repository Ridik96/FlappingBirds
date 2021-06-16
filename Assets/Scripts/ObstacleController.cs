﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
   public float gapHeight;
    public float gapMidpoint;
    private float totalHeight;
    private SpriteRenderer upColumn;
    private SpriteRenderer downColumn;
    private AudioSource m_audioSource;

    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        totalHeight = GetComponent<BoxCollider2D>().size.y;
        upColumn = transform.Find("UpColumn").gameObject.GetComponent<SpriteRenderer>();
        downColumn = transform.Find("DownColumn").gameObject.GetComponent<SpriteRenderer>();
        gapHeight = 5f;
        gapMidpoint = Random.Range(5f, totalHeight - 5f);
        UpdateObstacleParams();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bird"))
        {
            GameplayManager.Instance.Points += 1;
            m_audioSource.PlayOneShot(GameplayManager.Instance.GameDatabase.Power);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bird"))
        {
            GameplayManager.Instance.ObstacleObjectPool.SetObject(new Vector3((GameplayManager.Instance.Obstacle.transform.position.x + GameplayManager.Instance.emptySpace), 0, 0), Quaternion.identity);
        }
    }
    
    private void UpdateObstacleParams()
    {
        float M = Mathf.Clamp(gapMidpoint, 0, totalHeight);
        float G = Mathf.Clamp(gapHeight, 0, totalHeight);
        float U;
        float D;

        U = totalHeight - M - G / 2;
        D = M - G / 2;
        upColumn.size = new Vector2(upColumn.size.x, U);
        downColumn.size = new Vector2(downColumn.size.x, D);
    }

}
