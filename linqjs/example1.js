var linq = require("linq");
var fs = require("fs");
//Example 1

//Let's take a look at all the files in Windows' System32 folder
fs.readdir("C:\\Windows\\System32", function(err, files){
	if(err){
		//If there's an error, show it
		console.log(err);
	}
	else{
		//Well, we have an array with file names here. I only want to show all DLL files
		var result = linq.from(files)
					 .where(function(f){
					 	return f.substr(f.length - 3) == "dll"
					 })
					 .toArray();
		console.log(result);
	}
});