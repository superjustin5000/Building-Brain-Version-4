using UnityEngine;
using System.Collections;

public class TimeScaleTester : Tester {


	public void Test_IncreaseTimeScale() {

		TimeScaleController ts = GameObject.Find("TimeTracker+Scale").GetComponent<TimeScaleController>();

		ts.printTimeScale(); //initial.

		ts.increaseTimeScale(); //increase..

		ts.printTimeScale();//final.

	}

	public void Test_DecreaseTimeScale() {

		TimeScaleController ts = GameObject.Find("TimeTracker+Scale").GetComponent<TimeScaleController>();

		ts.printTimeScale(); //initial.

		ts.decreaseTimeScale(); //decrease..
		
		ts.printTimeScale(); //final.

	}


	public void Test_SetSpecificTimeScale() {

		TimeScaleController ts = GameObject.Find("TimeTracker+Scale").GetComponent<TimeScaleController>();
		
		ts.printTimeScale(); //initial.

		//create random number.
		float randomTimeScale = Random.Range(0.0f, ts.maxTimeScale);


		ts.setSpecificTimeScale(randomTimeScale); //some specifc time.
		
		ts.printTimeScale(); //final.

	}


	public void Test_SetSpecificTimeScaleMax() {

		TimeScaleController ts = GameObject.Find("TimeTracker+Scale").GetComponent<TimeScaleController>();
		
		ts.printTimeScale(); //initial.

		float randomAboveMaxTimeScale = Random.Range(ts.maxTimeScale+0.1f, 100000f);

		ts.setSpecificTimeScale(randomAboveMaxTimeScale); //some specifc time above the maximum.
		
		ts.printTimeScale(); //final. --- should print the maximum.

	}


	public void Test_ResetTimeScaleTest() {

		TimeScaleController ts = GameObject.Find("TimeTracker+Scale").GetComponent<TimeScaleController>();
		
		//call the test for setting specific time..
		Test_SetSpecificTimeScale(); //will assign random time.

		ts.resetTimeScale(); //reset to what should be 1.0f

		ts.printTimeScale(); //final. --- should print 1.0.

	}

}
