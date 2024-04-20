// AJAX for Teacher Add can go in here!
// This file is connected to the project via Shared/_Layout.cshtml

function AddTeacher() {

	//goal: send a request which looks like this:
	//POST : http://localhost:55963/api/TeacherData/AddTeacher
	//with POST data of Teachername, bio, email, etc.

	var URL = "http://localhost:55963/api/TeacherData/AddTeacher/";

	var rq = new XMLHttpRequest();
	// where is this request sent to?
	// is the method GET or POST?
	// what should we do with the response?

	var TeacherFname = document.getElementById('teacherfname').value;
	var TeacherLname = document.getElementById('teacherlname').value;
	var Teacherempnum = document.getElementById('teacherempnum').value;
	var salary = document.getElementById('salary').value;


	var TeacherData = {
		"TeacherFname": TeacherFname,
		"TeacherLname": TeacherLname,
		"Teacherempnum": Teacherempnum,
		"salary": salary
		
	};


	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing.


		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(TeacherData));

}





function UpdateTeacher(teacherid) {

	//goal: send a request which looks like this:
	//POST : http://localhost:51326/api/TeacherData/UpdateTeacher/{id}
	//with POST data of Teachername, bio, email, etc.

	var URL = "http://localhost:51326/api/TeacherData/UpdateTeacher/" + teacherid;

	var rq = new XMLHttpRequest();


	var TeacherFname = document.getElementById('teacherfname').value;
	var TeacherLname = document.getElementById('teacherlname').value;
	var TeacherEmail = document.getElementById('Teachermail').value;
	var TeacherBio = document.getElementById('TeacherBio').value;



	var TeacherData = {
		"TeacherFname": TeacherFname,
		"TeacherLname": TeacherLname,
		"Teacherempnum": Teacherempnum,
		"salary": salary
	};


	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing.


		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(TeacherData));

}


