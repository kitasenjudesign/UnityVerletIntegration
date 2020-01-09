using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class VerletStick {

    private VerletPoint _point0;
    private VerletPoint _point1;

    private float _length;
    private float _elasticity;

    public  VerletStick(
        VerletPoint p0, VerletPoint p1, float length, float elasticity
    ){

        _point0 = p0;
        _point1 = p1;
        _length = length;
        _elasticity = elasticity;

    }

    public void Update(){

        var delta = _point1.position - _point0.position;

        //二点の距離
        var distance = delta.magnitude;
        //基本距離        
        var difference = _length - distance;

        var offset = (difference * delta / distance)  * _elasticity;

        this._point0.AddCoord( -offset );
        this._point1.AddCoord(  offset );

    }

}

/*
// VerletStick
function VerletStick(point0, point1, length, elasticity) {
	if (!elasticity || elasticity > 0.5 || 0 > elasticity) {
		this.elasticity = 0.2;
	} else {
		this.elasticity = elasticity;
	}
	this._point0 = point0;
	this._point1 = point1;
	if (!length || length < 0) {
		this._length = point0.getDistance(point1);
	} else {
		this._length = length;
	}
}
VerletStick.prototype.update = function() {
	var delta = this._point1.subtract(this._point0);
	var distance = delta.getLength();
	var difference = this._length - distance;
	var offsetX = (difference * delta.x / distance)  * this.elasticity;
	var offsetY = (difference * delta.y / distance)  * this.elasticity;
	this._point0.addCoordinates(-offsetX, -offsetY);
	this._point1.addCoordinates(offsetX, offsetY);
};
VerletStick.prototype.render = function(graphics) {
	graphics.beginStroke("black")
	.setStrokeStyle(0.5)
	.moveTo(this._point0.x, this._point0.y)
	.lineTo(this._point1.x, this._point1.y);
};*/
