var apiFunction = function(request, response, con){
	var html = con.basichtmlpage;
	
	// Head
	var head = "";
	var jquery = request.param('jquery');
	if(jquery){
		head += con.scriptFrame.replace("{URL}", con.jqueryURL.replace("{V}", jquery));
	}
	var jqueryMobile = request.param('jqueryMobile');
	if(jqueryMobile){
		head += con.scriptFrame.replace("{URL}", con.jqueryMobileJSURL.replace("{V}", jqueryMobile));
		head += con.cssFrame.replace("{URL}", con.jqueryMobileCSSURL.replace("{V}", jqueryMobile));
	}
	var jqueryUI = request.param('jqueryUI');
	if(jqueryUI){
		head += con.scriptFrame.replace("{URL}", con.jqueryUIJSURL.replace("{V}", jqueryUI));
		head += con.cssFrame.replace("{URL}", con.jqueryUICSSURL.replace("{V}", jqueryUI));
	}
	var angular = request.param('angular');
	if(angular){
		head += con.scriptFrame.replace("{URL}", con.angularURL.replace("{V}", angular));
	}
	var dojo = request.param('dojo');
	if(dojo){
		head += con.scriptFrame.replace("{URL}", con.dojoURL.replace("{V}", dojo));
	}
	var extjs = request.param('extjs');
	if(extjs){
		head += con.scriptFrame.replace("{URL}", con.extjsURL.replace("{V}", extjs));
	}
	var mootools = request.param('mootools');
	if(mootools){
		head += con.scriptFrame.replace("{URL}", con.mootoolsURL.replace("{V}", mootools));
	}
	var prototypejs = request.param('prototypejs');
	if(prototypejs){
		head += con.scriptFrame.replace("{URL}", con.prototypejsURL.replace("{V}", prototypejs));
	}
	var scriptaculous = request.param('scriptaculous');
	if(scriptaculous){
		head += con.scriptFrame.replace("{URL}", con.scriptaculousURL.replace("{V}", scriptaculous));
	}
	var swfobject = request.param('swfobject');
	if(swfobject){
		head += con.scriptFrame.replace("{URL}", con.swfobjectURL.replace("{V}", swfobject));
	}
	var threejs = request.param('threejs');
	if(threejs){
		head += con.scriptFrame.replace("{URL}", con.threejsURL.replace("{V}", threejs));
	}
	var webfontloader = request.param('webfontloader');
	if(webfontloader){
		head += con.scriptFrame.replace("{URL}", con.webfontloaderURL.replace("{V}", webfontloader));
	}
	html = html.replace("{OTHER_HEAD}", head);
	// !Head
	
	var charenc = request.param('charenc');
	if(!charenc){
		charenc = "UTF-8";
	}
	html = html.replace("{CHARSET}", charenc);
	var title = request.param('title');
	if(!title){
		title = "";
	}
	html = html.replace("{TITLE}", title);
	html = html.replace("{BODY}", "");
	
	var htmltidy = request.param('htmltidy');
	if(htmltidy && htmltidy == "1"){
		html = html.replace(new RegExp("><", "g"), '>\r\n<');
	}
	response.send({html: html, valid: true});
};

exports.apiFunction = apiFunction;