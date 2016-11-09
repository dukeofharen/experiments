var rootUrl = "http://api.postcodeapi.nu/";
var request = require("request");
var apiKey = "8ddfc3fb4c0ca43e8806949ad3137aa0be4d5835";
var snapin = require("./../snapin.js");
var mongoose = require('mongoose');

var nlPostcodeSchema = mongoose.Schema({
	postcode: String,
	postcodeResult: String
});
var PostcodeResult = mongoose.model('PostcodeResult', nlPostcodeSchema);

var error = function(res, msg){
	if(!msg){
		msg = "Something went wrong.";
	}
	res.status(500).send({valid: false, data: msg});
};
var success = function(res, body){
	res.status(200).send({valid: true, data: JSON.parse(body)});
};

var nlPostcode = {
	asyncExecute: function(req, res, command){
		var params = command.split(" ");
		if(params.length < 2){
			error(res, "You should fill in a postal code.");
		}
		var headers = {
			"Api-Key":apiKey
		};
		PostcodeResult.find({postcode: params[1]}, function(err, codes){
			if(err){
				console.log(err);
				error(res);
			}
			else{
				if(codes.length > 0){
					success(res, codes[0].postcodeResult);
				}
				else{
					request.get({url: rootUrl+params[1], headers: headers}, function(err, response, body){
						if(err){
							console.log(err);
							error(res);
						}
						else{
							var code = new PostcodeResult({
								postcode: params[1],
								postcodeResult: body
							});
							code.save(function(err, code){
								if(err){
									console.log(err);
									error(res);
								}
								else{
									success(res, code.postcodeResult);
								}
							});
						}
					});
				}
			}
		});
	}
};
snapin.addSnapin("nlPostcode", nlPostcode);