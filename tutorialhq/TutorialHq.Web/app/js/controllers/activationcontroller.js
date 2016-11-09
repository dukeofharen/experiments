angular.module('myApp').controller('ActivationController', ["user", "$routeParams", "$location", function (user, $routeParams, $location) {
	var redirect = function () {
		$location.path("/main");
	};
	var onError = function () {
		$().tostie({ type: "error", message: "Your account couldn't been activated." });
		redirect();
	};
	var onSuccess = function () {
		$().tostie({ type: "success", message: "Your account has been activated successfully." });
		redirect();
	};

	user.activateUser($routeParams.username, $routeParams.code).then(onSuccess, onError);
}]);