angular.module('myApp').controller('LoginController', ["user", "$routeParams", "$rootScope", "$location", function (user, $routeParams, $rootScope, $location) {
	var self = this;

	var onError = function () {
		$().tostie({ type: "error", message: "Your credentials are wrong." });
	};
	var onSuccess = function () {
		$location.path("/main");
	};

	this.username = "";
	this.password = "";

	this.login = function () {
		user.authenticate(self.username, self.password).then(onSuccess, onError);
	};
}]);