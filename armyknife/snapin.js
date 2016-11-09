var fs = require("fs");
var path = require("path");
var directory = "./snapins";

if(!global.snapins){
	global.snapins = {};
	fs.readdir(directory, function (err, files) {
	    if (err) {
	       console.log(err);
	    }
	    else{
	    	files.forEach(function(file){
	    		if(path.extname(file) == ".js"){
	    			require("./snapins/"+file);
	    		}
	    	});
	    }
	});
}

exports.addSnapin = function(uniqueName, snapin){
	console.log("Adding snapin "+uniqueName);
	global.snapins[uniqueName] = snapin;
};

exports.getExpressObject = function(){
	return global.app;
};

exports.getSnapin = function(uniqueName){
	return global.snapins[uniqueName];
};

exports.getSnapins = function(){
	return global.snapins;
};