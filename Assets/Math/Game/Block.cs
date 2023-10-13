using UnityEngine;
using DG.Tweening;

public class Block : MonoBehaviour
{
    [SerializeField]
    public int blockId;

    [SerializeField]
    private BlockManager bm;

    private int currentPos;
    private PyramidPlace pyramidPlace;
    private Rigidbody rb;
    private Collider collider;

    public bool dragged;
    private float startPosX;
    private ParticleSystem dust;

    [SerializeField]
    private AudioClip moveSound,
        placeSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        dust = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (dragged)
        {
            other.gameObject.TryGetComponent<PyramidPlace>(out pyramidPlace);
        }
    }

    private void TurnOnRb()
    {
        collider.enabled = true;
        collider.isTrigger = false;
        rb.useGravity = true;
    }

    private void TurnOffRb()
    {
        collider.isTrigger = true;
        rb.useGravity = false;
    }

    private void Update()
    {
        if (dragged)
        {
            Drag();
        }
    }

    private void Drag()
    {
        float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        transform.position = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen)
        );
    }

    private void OnMouseDown()
    {
        if (bm.CheckCanDrag(blockId, currentPos))
        {
            PlaySound(moveSound);
            dragged = true;
            pyramidPlace = null;
            TurnOffRb();
            startPosX = transform.position.x;
        }
    }

    private void OnMouseUp()
    {
        if (dragged && pyramidPlace)
        {
            collider.enabled = false;
            if (bm.CheckCorrectPlace(blockId, pyramidPlace.place))
            {
                Sequence placeNewPos = DOTween.Sequence();
                placeNewPos
                    .Append(
                        transform.DOMove(new Vector3(pyramidPlace.transform.position.x, 0), 0.5f)
                    )
                    .AppendCallback(() =>
                    {
                        TurnOnRb();
                        placeNewPos.Kill();

                        bm.PlaceBlock(blockId, pyramidPlace.place, currentPos);
                        currentPos = pyramidPlace.place;
                        pyramidPlace = null;
                    })
                    .Play();
            }
            else
            {
                Sequence placeOldPos = DOTween.Sequence();
                placeOldPos
                    .Append(transform.DOMove(new Vector3(startPosX, 0), 0.5f))
                    .AppendCallback(() =>
                    {
                        TurnOnRb();
                        placeOldPos.Kill();
                    })
                    .Play();
            }
        }
        dragged = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        dust.Play();
        PlaySound(placeSound);
        if (PlayerPrefs.GetInt("Vibro", 1) == 1)
        {
            Handheld.Vibrate();
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }
    }
}
