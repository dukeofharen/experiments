var snapin = require("./../snapin.js");

var encode = {
	execute: function(command){
		var params = command.split(" ");
		if(params.length < 2){
			return {valid: false, data: "Please provide a string to encode."};
		}
		params.splice(0, 1);
		var string = params.join(" ");
		var b = new Buffer(string);
		return {valid: true, data: b.toString('base64')};
	}
};
snapin.addSnapin("toBase64", encode);

var decode = {
	execute: function(command){
		var params = command.split(" ");
		if(params.length < 2){
			return {valid: false, data: "Please provide a string to decode."};
		}
		var params = command.split(" ");
		var b = new Buffer(params[1], 'base64')
		return {valid: true, data: b.toString()};
	}
};
snapin.addSnapin("fromBase64", decode);