function getTools(successCallback, filters) {
	var url = '/api/tools';
	var urlParams = [];
	if (filters.category) {
		urlParams.push("categoryId=" + filters.category);
	}
	if (filters.type) {
		urlParams.push("type=" + filters.type);
	}
	if (filters.os) {
		urlParams.push("os=" + filters.os);
	}
	if (filters.license) {
		urlParams.push("license=" + filters.license);
	}
	if (urlParams.length > 0) {
		url += "?" + urlParams.join("&");
	}
	$.get(url, function (data) {
		successCallback(data);
	});
}

function getCategories(successCallback) {
	$.get('/api/categories', function (data) {
		successCallback(data);
	});
}

function getTypes(successCallback) {
	$.get('/api/types', function (data) {
		successCallback(data);
	});
}

function getOSs(successCallback) {
	$.get('/api/os', function (data) {
		successCallback(data);
	});
}

function getLicenses(successCallback) {
	$.get('/api/licenses', function (data) {
		successCallback(data);
	});
}

function submitTool(serializedForm) {
	$.post('/api/tools', serializedForm, function (data) {
		console.log(data);
	}).fail(function () {
		console.log("error");
	});
}