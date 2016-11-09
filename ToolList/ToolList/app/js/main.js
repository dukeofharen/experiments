var readMoreText = "Read more";
var viewLessText = "View less";
var filter = {};
$(document).ready(function(){
	initTools(filter);
	getCategories(function (data) {
		var categories = '<option value="0">Category</option>';
		data.forEach(function (category) {
			categories += '<option value="'+category.id+'">'+category.name+'</option>';
		});
		$('#category').html(categories);
		$('#categoryForm').html(categories);
		$('#category').change(function () {
			var value = $(this).val();
			if (value == 0) {
				value = null;
			}
			filter.category = value;
			initTools(filter);
		});
	});
	getTypes(function (data) {
		var types = '<option value="0">Type</option>';
		data.forEach(function (type) {
			types += '<option value="' + type.id + '">' + type.name + '</option>';
		});
		$('#type').html(types);
		$('#typeForm').html(types);
		$('#type').change(function () {
			var value = $(this).val();
			if (value == 0) {
				value = null;
			}
			filter.type = value;
			initTools(filter);
		});
	});
	getOSs(function (data) {
		var oss = '<option value="0">Operating System</option>';
		var ossForm = "";
		data.forEach(function (os) {
			oss += '<option value="' + os.id + '">' + os.name + '</option>';
			ossForm += '<input type="checkbox" name="oss[]" value="' + os.id + '" /> ' + os.name + '<br />';
		});
		$('#os').html(oss);
		$('#os').change(function () {
			var value = $(this).val();
			if (value == 0) {
				value = null;
			}
			filter.os = value;
			initTools(filter);
		});
		$('#ossForm').html(ossForm);
	});
	getLicenses(function (data) {
		var licenses = '<option value="0">License</option>';
		data.forEach(function (license) {
			licenses += '<option value="' + license.id + '">' + license.name + '</option>';
		});
		$('#license').html(licenses);
		$('#licenseForm').html(licenses);
		$('#license').change(function () {
			var value = $(this).val();
			if (value == 0) {
				value = null;
			}
			filter.license = value;
			initTools(filter);
		});
	});
	$('#add-tool').click(function () {
		var visible = $('#add-tool-div').is(":visible");
		var elem = $('#add-tool-div');
		if (visible) {
			elem.slideUp();
		}
		else {
			elem.slideDown();
		}
		return false;
	});
	$('#add-tool-form').submit(function () {
		submitTool($(this).serialize());
		return false;
	});
});

function initTools(filter) {
	getTools(function (data) {
		var tools = "";
		data.forEach(function (tool) {
			tools += getToolHtml(tool.name, tool.siteUrl, tool.downloadUrl, tool.imageUrl, tool.description, tool.category, tool.version, tool.lastUpdated, tool.license, tool.oss.join(", "), tool.creator, tool.creatorSite);
		});
		var toolsDom = $(tools);
		initReadMore(toolsDom.find(".read-more"));
		$('#tools').html(toolsDom);
	}, filter);
}

function initReadMore(elem){
	if(!elem){
		elem = $('.read-more');
	}
	elem.text(readMoreText);
	elem.click(function(){
		var current = $(this).text();
		if(current == readMoreText){
			$(this).text(viewLessText);
		}
		else{
			$(this).text(readMoreText);
		}
		$(this).parent().toggleClass("less");
		return false;
	});
}

function getToolHtml(name, siteUrl, downloadUrl, imageUrl, description, category, version, lastUpdated, license, os, creator, creatorSite){
	var result = '<div class="row tool">';
	result += '<div class="col-sm-2 title">';

	var download = '';
	if(downloadUrl){
		download = ' (<a href="'+downloadUrl+'" target="_blank">download</a>)';
	}
	result += '<strong><a href="'+siteUrl+'" target="_blank">'+name+'</a>'+download+'</strong>';

	if(imageUrl){
		result += '<div class="tool-image"><a href="' + siteUrl + '" target="_blank">';
		result += '<img src="'+imageUrl+'" alt="" />';
		result += '</a></div>';
	}

	result += '</div>'

	result += '<div class="col-sm-6 description less">';
	result += '<div>'+description+'<div class="shadow"></div></div>';
	result += '<a href="#" class="read-more"></a>';
	result += '</div>';

	result += '<div class="col-sm-4 meta"><p>';
	if(category){
		result += '<span><span class="glyphicon glyphicon-list-alt" title="Category"></span>&nbsp;&nbsp;'+category+'</span>';
	}
	if(version){
		result += '<span><span class="glyphicon glyphicon-upload" title="Version"></span>&nbsp;&nbsp;'+version+'</span>';
	}
	if(lastUpdated){
		result += '<span><span class="glyphicon glyphicon-calendar" title="Last updated"></span>&nbsp;&nbsp;'+lastUpdated+'</span>';
	}
	if(license){
		result += '<span><span class="glyphicon glyphicon-info-sign" title="License"></span>&nbsp;&nbsp;'+license+'</span>';
	}
	if(os){
		result += '<span><span class="glyphicon glyphicon-globe" title="OS"></span>&nbsp;&nbsp;'+os+'</span>';
	}
	if(creator && creatorSite){
		result += '<span><span class="glyphicon glyphicon-user" title="Creator"></span>&nbsp;&nbsp;<a href="'+creatorSite+'" target="_blank">'+creator+'</a></span>';
	}
	result += '</p></div>';

	result += '</div><hr />';
	
	return result;
}