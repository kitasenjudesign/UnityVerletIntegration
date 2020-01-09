using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class VerletPoint {

    public Vector3 position;
    public Vector3 velocity;
    private Vector3 _oldPosition;

    public  VerletPoint(Vector3 pos){

        position = pos;//Vector3.zero;
        velocity = Vector3.zero;
        _oldPosition = pos;//Vector3.zero;
    }

    public Vector3 GetVelocity(){
        
        var v = new Vector3(
            position.x - _oldPosition.x,
            position.y - _oldPosition.y,
            position.z - _oldPosition.z
        );
        return v;

    }

    /*
VerletPoint.prototype.getVelocity = function() {
	var velocity = new createjs.Point(this.x - this._oldX, this.y - this._oldY);
	return velocity;
}; */

    public void AddCoord( Vector3 offset){

        position += offset;

    }

    public void Update(){

        var tmpX = position.x;
        var tmpY = position.y;
        var tmpZ = position.z;

        var v = GetVelocity() * 0.99f;
	    AddCoord( v );

        _oldPosition.x = tmpX;
        _oldPosition.y = tmpY;
        _oldPosition.z = tmpZ;

    }






    /*
        VerletPoint.prototype.update = function() {
            var tempX = this.x;
            var tempY = this.y;
            var velocity = this.getVelocity();
            this.addCoordinates(velocity.x, velocity.y);
            this._oldX = tempX;
            this._oldY = tempY;
        };    
     */

}


/**

<script>
function VerletPoint(x, y) {
	this.x = this._oldX = x;
	this.y = this._oldY = y;
}
VerletPoint.prototype.update = function() {
	var tempX = this.x;
	var tempY = this.y;
	var velocity = this.getVelocity();
	this.addCoordinates(velocity.x, velocity.y);
	this._oldX = tempX;
	this._oldY = tempY;
};
VerletPoint.prototype.constrain = function(rect) {
	var left = rect.x;
	var right = left + rect.width;
	var top = rect.y;
	var bottom = top + rect.height;
	if (this.x < left) {
		this.x = left;
	} else if (this.x > right) {
		this.x = right;
	}
	if (this.y < top) {
		this.y = top;
	} else if (this.y > bottom) {
		this.y = bottom;
	}
};
VerletPoint.prototype.getVelocity = function() {
	var velocity = new createjs.Point(this.x - this._oldX, this.y - this._oldY);
	return velocity;
};
VerletPoint.prototype.render = function(graphics) {
	graphics.beginFill("black")
	.drawCircle(this.x, this.y, 2.5)
	.endFill();
};
VerletPoint.prototype.addCoordinates = function(x, y) {
	this.x += x;
	this.y += y;
};


**/

