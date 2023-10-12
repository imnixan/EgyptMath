using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BuildController : MonoBehaviour
{

    public AudioClip error, succes;
    public GameObject ReplayBtn;
    public List<GameObject>  pyramidSteps;
    public GameObject[] columnsTrigger;
    private List<List<DigitBlock>> columns = new List<List<DigitBlock>>();
    private Dictionary<int,float> coords = new Dictionary<int,float>();
    private Vector2 min, max;
    private AudioSource sound;
    private bool gameEnd;
    void Start(){
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        sound = GetComponent<AudioSource>();
        columns.Add(new List<DigitBlock>());
        columns.Add(new List<DigitBlock>());
        columns.Add(new List<DigitBlock>());
        min = Camera.main.ViewportToWorldPoint (new Vector2 (0,0)); 
        max = Camera.main.ViewportToWorldPoint (new Vector2 (1,1)); 
        coords.Add(1,-5.5f);
        coords.Add(2, 0);
        coords.Add(3, 5.5f);
        foreach(GameObject trig in columnsTrigger){
            trig.SetActive(false);
        }
        BuildPyramid();
    }

    public bool CanBeTouched(DigitBlock db){
        if(gameEnd){
            return false;
        }
        int curColumn = db.GetOldColumn() - 1;
        if(columns[curColumn].Count == 1 || columns[curColumn][columns[curColumn].Count - 1] == db){
            return true;
        }

        return false;
    }


    private void BuildPyramid(){
        if(GameRules.digits < 6){
            pyramidSteps.RemoveAt(0);
            pyramidSteps.RemoveAt(0);
        }
        int y = 0;
        while(pyramidSteps.Count > 0){
            int step = Random.Range(0, pyramidSteps.Count);
            GameObject newStep = Instantiate(pyramidSteps[step], new Vector2(coords[1], y), new Quaternion());
            columns[0].Add(newStep.GetComponent<DigitBlock>());
            pyramidSteps.RemoveAt(step);
            y++;
        }

        foreach(DigitBlock db in columns[0]){
            db.SetBc(this);
        }

        foreach(GameObject trig in columnsTrigger){
            trig.SetActive(true);
        }
        
    }


    public bool CanGoHere(int columnNum, int number){
    

        int columnNumber = columnNum - 1;
        if((columns[columnNumber].Count < GameRules.digits) && ((columns[columnNumber].Count == 0) || (columns[columnNumber][columns[columnNumber].Count - 1].GetNumber() > number ))){
            return true;
        }
        
        return false;
    }

    public void IPlaceHere(DigitBlock db, int column){
        columns[db.GetOldColumn() - 1].Remove(db);
        columns[column - 1].Add(db);
        db.PlaceInNewColumn(new Vector2(coords[column], columns[column - 1][columns[column - 1].Count - 1].gameObject.transform.position.y + 1));
        CheckWin();

    }

    private void CheckWin(){
        bool win = false;

        foreach(List<DigitBlock> column in columns){
            int iterations = 0;
            int previousNumber = 10;
            foreach(DigitBlock db in column){
                iterations ++;
                if(previousNumber > db.GetNumber()){
                    previousNumber = db.GetNumber();
                }else{
                    previousNumber = 10;
                    iterations = 0;
                    break;
                }
            }
            if(previousNumber == 1 && iterations == GameRules.digits){
                win = true;
                break;
            }
        }
            if(win){
                if(PlayerPrefs.GetInt("sound") == 0){
                    sound.PlayOneShot(succes);
                }
                gameEnd = true;
                ReplayBtn.transform.position = Vector2.zero;
            }
    }

    public Vector2 GetMyOldPos(int column){
        if(PlayerPrefs.GetInt("sound") == 0){
            sound.PlayOneShot(error);
        }
        if(PlayerPrefs.GetInt("vibro")==0){
            Handheld.Vibrate();
        }
        return new Vector2(coords[column], columns[column - 1][columns[column - 1].Count - 1].gameObject.transform.position.y + 1);
    }

    public void Back(){
        SceneManager.LoadScene("Menu");
    }    

    public void Replay(){
        SceneManager.LoadScene("Pyramids");
    }

}
