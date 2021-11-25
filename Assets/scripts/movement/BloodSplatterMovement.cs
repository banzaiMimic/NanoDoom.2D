using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatterMovement : MonoBehaviour {
  [SerializeField] private Vector2 parallaxEffectMultiplier = new Vector2(-.2f, .01f);
  [SerializeField] public Transform cameraTransform;
  private Vector3 lastCameraPosition;
  private float textureUnitSizeX;

  private void Start() {
    lastCameraPosition = cameraTransform.position;
    Sprite sprite = GetComponent<SpriteRenderer>().sprite;
    Texture2D texture = sprite.texture;
    textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
  }

  private void LateUpdate() {
    Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
    transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);
    transform.position = new Vector2(transform.position.x, -2.7f);
    lastCameraPosition = cameraTransform.position;
  }
}
