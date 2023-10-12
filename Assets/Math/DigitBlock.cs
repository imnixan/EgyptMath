using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DigitBlock : MonoBehaviour
{
    public int number;
    public int currentColumn;
    public int wantedColumn;
    private bool holded;
    private BuildController bc;
    private Rigidbody2D rb;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        currentColumn = 1;
    }

   
    public void SetBc(BuildController bc){
        this.bc = bc;
    }

    public int GetNumber(){
        return number;
    }

    public int GetOldColumn(){
        return currentColumn;
    }

    void OnMouseDown(){
        if(bc.CanBeTouched(this)){
            holded = true;
            // bc.SetKinematic(true);
        }
        
    }

    void OnCollisionEnter2D(Collision2D other){
        rb.isKinematic = true;
    }
    void OnMouseUp(){
        // bc.SetKinematic(false);
        if(holded){
            rb.isKinematic = false;
            holded = false;
            if(wantedColumn > 0){
               
                bc.IPlaceHere(this, wantedColumn);
            }else{
                transform.position = bc.GetMyOldPos(currentColumn);
            }
        }
    }

    public void PlaceInNewColumn(Vector2 place){
  
        transform.position = place;
        currentColumn = wantedColumn;
        wantedColumn = 0;
    }

    void Update(){
        if(holded){
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePos.x, mousePos.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(holded){
            int columnNum = Int32.Parse(other.tag);
            if(columnNum != currentColumn){
                if(bc.CanGoHere(columnNum, number)){
             
                    wantedColumn = columnNum;
                  
                }else{
                  
                    wantedColumn = 0;
                }
                
            }
        }

    }

    void OnTriggerExit2D(Collider2D other){
        // wantedColumn = 0;
    }



}
