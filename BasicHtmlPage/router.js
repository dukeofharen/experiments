var logic = require('./logic');
var route = function(app, con){
	var fs = require("fs");
	var year = new Date().getFullYear();
	
	app.get('/', function(request, response){
		fs.readFile('./views/adsense.html', 'utf8', function (err,data) {
			response.render("index", {title:con.title, year: year, jquery: con.jquery, jqueryMobile: con.jqueryMobile, jqueryUI: con.jqueryUI, angular: con.angular, dojo: con.dojo, extjs: con.extjs, mootools: con.mootools, prototypejs: con.prototypejs, scriptaculous: con.scriptaculous, swfobject: con.swfobject, threejs: con.threejs, webfontloader: con.webfontloader, adsense: data});
		});
	});
	
	app.get('/api', function(request, response){
		logic.apiFunction(request, response, con);
	});
	app.post('/api', function(request, response){
		logic.apiFunction(request, response, con);
	});
	
	app.get('/about', function(request, response){
		fs.readFile('./views/adsense.html', 'utf8', function (err,data) {
			response.render("about", {title:con.app_title+" - About", year: year, adsense: data});
		});
	});
}

exports.route = route;