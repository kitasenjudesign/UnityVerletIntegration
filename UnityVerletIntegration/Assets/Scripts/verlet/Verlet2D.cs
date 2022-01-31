using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Verlet2D : MonoBehaviour{


    [SerializeField] private Mesh _mesh;
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Vector2Int _num;
    private List<GameObject> _balls;

    private List<VerletPoint> _points;
    private List<VerletStick> _sticks;

    private VerletPoint[,] _points2D;

    void Start(){

        _mesh = MeshUtil.CloneMesh(_mesh,_mesh.name+"clone",Vector3.one);
        _meshFilter.sharedMesh=_mesh;

        _balls = new List<GameObject>();

        _points2D = new VerletPoint[_num.x,_num.y];
        _points = new List<VerletPoint>();
        _sticks = new List<VerletStick>();

        _prefab.gameObject.SetActive(false);

        Debug.Log( _mesh.vertexCount );

        int cnt=0;
         for(int j=0;j<_num.y;j++){
            for(int i=0;i<_num.x;i++){
                
                
                var g = Instantiate(_prefab,transform,false);
                g.gameObject.SetActive(true);
                _balls.Add(g);

                var p = _mesh.vertices[cnt];
                g.transform.position = p;
                g.transform.localScale=Vector3.one*0.05f;

                g.name = "p"+i+"_"+j;//cnt.ToString("000");
                cnt++;

                var vp = new VerletPoint(g.transform.position);
                _points.Add( vp );

                _points2D[i,j] = vp;

            }
         }

        //繋ぐ
        
        for(int j=0;j<_num.y;j++){
            for(int i=0;i<_num.x;i++){
                
                if(i+1<=_num.x-1){
                    MakeStick(i,j,i+1,j);
                    if(j+1<=_num.y-1) MakeStick(i,j,i+1,j+1);
                }
                if(j+1<=_num.y-1){
                    MakeStick(i,j,i,j+1);
                    if(i-1>=0) MakeStick(i,j,i-1,j+1);
                }

            }
        }




    }

    private void MakeStick(int ax,int ay, int bx, int by ){

        var s = new VerletStick(
                _points2D[ ax,ay ],
                _points2D[ bx,by ],
                Vector3.Distance(
                    _points2D[ ax,ay ].position,
                    _points2D[ bx,by ].position
                ),
                0.3f
            );
            _sticks.Add( s );

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

        
        var verts = _mesh.vertices;
        for(int i=0;i<verts.Length;i++){
            verts[i] = _points[i].position;
        }
        _mesh.vertices=verts;

    }



}
