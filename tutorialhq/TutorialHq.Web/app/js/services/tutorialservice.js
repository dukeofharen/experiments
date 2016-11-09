(function () {
	var tutorial = function ($http) {
		var getTutorials = function (categoryId) {
			var url = "/api/tutorials?howMany=20";
			if (categoryId) {
				url += "&categoryId=" + categoryId;
			}
			return $http.get(url).then(function (response) {
				return response.data;
			});
		};

		var getTutorial = function (id) {
			var tutorial;
			return $http.get("/api/tutorials/" + id).then(function (response) {
				tutorial = response.data;
				return $http.get("/api/tutorials/" + id + "/comments");
			})
			.then(function (response) {
				tutorial.comments = response.data;
				return tutorial;
			});
		};

		var getCategories = function () {
			return $http.get("/api/categories").then(function (response) {
				var categories = response.data;
				categories.splice(0, 0, { title: "All", id: "" });
				return categories;
			});
		};

		return {
			getTutorials: getTutorials,
			getTutorial: getTutorial,
			getCategories: getCategories
		};
	};

	var module = angular.module("myApp");
	module.factory("tutorial", ["$http", tutorial]);
}());