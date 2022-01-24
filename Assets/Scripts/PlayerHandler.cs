using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DigitalRuby.AnimatedLineRenderer;

public class PlayerHandler : MonoBehaviour
{
  float speed;
  public LineRenderer lineRenderer;
  public GameObject spawn;
  public AnimatedLineRenderer alr;
  public GameObject levelHandler;
  public GameObject goal;
  List<Vector3> lasers;
  public int limit=10;

  // Start is called before the first frame update
  void Start()
  {
    speed = 50;
    lasers = new List<Vector3>();
  }

  // Update is called once per frame
  void Update()
  {
    transform.eulerAngles -=  Vector3.forward * Input.GetAxis("Horizontal")*speed*Time.deltaTime;

    if(Input.GetKeyDown("space")){
      alr.Reset();
      lasers = new List<Vector3>();
      lasers.Add((Vector3)spawn.transform.position);

      Shoot(transform.position, spawn.transform.position - transform.position, null, 10);
      // draw lines since its donezo
      /* lineRenderer.useWorldSpace = true; */
      /* lineRenderer.positionCount = lasers.Count; */
      /* lineRenderer.SetPositions(lasers.ToArray()); */
      foreach(var pos in lasers){
        alr.Enqueue(pos);
      }
    }
}
void Shoot(Vector2 pos, Vector2 dir, Collider2D lastCollider , int limit){
    if(limit <=0){
      return;
    }
    RaycastHit2D hit = Physics2D.RaycastAll(pos, dir).ToList().First(x=>x.collider!=null && x.collider != lastCollider);

    if(hit){
      lasers.Add((Vector3)hit.point);
      switch(hit.collider.tag){
        case "Mirror":
          Shoot(hit.point, Vector2.Reflect(dir,hit.normal), hit.collider, limit-1);
          break;
        case "Goal":
          WaitAndWin();
          break;
        default:
          break;
      }
    }else{
            // kill laser animation or smt xd
    }
  }
  void WaitAndWin(){
    float delay = alr.SecondsPerLine * (lasers.Count - 3);
    Invoke("Win", delay);
  }
  void Win(){
    goal.GetComponent<GoalHandler>().Kill();
    Invoke("NextLevel", 1.5f);
  }
  void NextLevel(){
    levelHandler.GetComponent<LevelHandler>().NextLevel();
  }
}
