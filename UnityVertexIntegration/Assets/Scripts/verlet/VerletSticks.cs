using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class VerletSticks : MonoBehaviour{

    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _num = 14;
    private List<GameObject> _balls;

    private List<VerletPoint> _points;
    private List<VerletStick> _sticks;

    void Start(){

        _balls = new List<GameObject>();

        _points = new List<VerletPoint>();
        _sticks = new List<VerletStick>();

        _prefab.gameObject.SetActive(false);

        for(int i=0;i<_num;i++){

            var g = Instantiate(_prefab,transform,false);
            g.gameObject.SetActive(true);
            g.transform.position = new Vector3(
                0,(float)i*0.4f,0
            );
            _balls.Add(g);

            var p = new VerletPoint(g.transform.position);
            _points.Add( p );

        }

        for(int i=0;i<_num-1;i++){
            
            var s = new VerletStick(
                _points[ i+0 ],
                _points[ i+1 ],
                0.4f,
                0.1f
            );
            _sticks.Add( s );

        }

        //verlt pointをつくる

    }

    void Update(){


        var mousePos = Input.mousePosition;
		mousePos.z = 0f;

		// マウス位置座標をスクリーン座標からワールド座標に変換する
		var screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(mousePos);
        screenToWorldPointPosition.z = 0;

        var tgt = screenToWorldPointPosition;
        _points[0].position = tgt;

        for(int i=0;i<_points.Count;i++){
            _points[i].Update();
            _balls[i].transform.position = _points[i].position;
        }
	
        for(int i=0;i<_sticks.Count;i++){
            _sticks[i].Update();
        }

    }



}
