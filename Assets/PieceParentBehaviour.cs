using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class PieceParentBehaviour : MonoBehaviour
{
        public int id;
        private Vector3 screenPoint;
        private Vector3 offset;

        private void SnapToGrid()
        {
            Vector3 position = new Vector3(
            Mathf.RoundToInt(this.transform.position.x),
            Mathf.RoundToInt(this.transform.position.y),
            Mathf.RoundToInt(this.transform.position.z)
            );
            gameObject.transform.position = position;
        }
        private void OnMouseUp()
        {
            SnapToGrid();    
        }   

        void OnMouseDown()
            {
                screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

                offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

            }

        void OnMouseDrag()
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;

        }

    
    /*
    public int PieceId;
    public Color PieceColor;

    public void Start()
    {
           
    }
    */

}
