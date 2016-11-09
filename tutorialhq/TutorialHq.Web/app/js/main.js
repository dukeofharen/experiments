(function(){
	var myApp = angular.module('myApp', ['ngRoute', 'ngResource']);

	myApp.constant("USER_ROLES", {
		all: "*",
		not_logged_in: "nli",
		admin: "admin",
		regular: "regular"
	});

	myApp.config(["$routeProvider", "USER_ROLES", function ($routeProvider, USER_ROLES) {
		$routeProvider
			.when("/main", {
				templateUrl: "views/tutorials.html",
				controller: "TutorialController",
				controllerAs: "tutCtrl"
			})
			.when("/tutorials/:id", {
				templateUrl: "views/tutorial.html",
				controller: "TutorialDetailController",
				controllerAs: "tutCtrl"
			})
			.when("/login", {
				templateUrl: "views/login.html",
				controller: "LoginController",
				controllerAs: "lgnCtrl",
				accessLevels: [USER_ROLES.not_logged_in]
			})
			.when("/user/:username/activate/:code", {
				templateUrl: "views/activation.html",
				controller: "ActivationController",
				controllerAs: "actCtrl"
			})
			.otherwise({
				redirectTo:"/main"
			});
	}]);

	myApp.run(["user", "$rootScope", "$location", function (user, $rootScope, $location) {
		if (sessionStorage.username && sessionStorage.password) {
			user.authenticate(sessionStorage.username, sessionStorage.password);
		}

		$rootScope.$on("$routeChangeStart", function (evt, next, curr) {
			if (!user.isAuthorized(next.accessLevels)) {
				$location.path('/main');
			}
		});
	}]);
}());