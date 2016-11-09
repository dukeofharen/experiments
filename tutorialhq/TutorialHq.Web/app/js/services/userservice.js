(function () {
	var userService = function ($resource, $http, $rootScope, USER_ROLES) {
		var currentUser;
		var userResource = $resource('/api/users/:username', { username: '@username' }, {

		});
		var activationResource = $resource('/api/users/:username/activate/:activationCode', { username: '@username', activationCode: '@activationCode' }, {

		});

		var userService = {
			authenticate: function (username, password) {

				$http.defaults.headers.common['Authorization'] = 'Basic ' + btoa(username + ':' + password);
				return userResource.get({ username: username }, function (user) {
					sessionStorage.username = username;
					sessionStorage.password = password;
					if (user.role === 1) {
						user.role = "admin";
					}
					else {
						user.role = "regular";
					}
					$rootScope.loggedIn = true;
					userService.setCurrentUser(user);
				}).$promise;
			},
			isLoggedIn: function () {
				return currentUser ? true : false;
			},
			clear: function () {
				currentUser = null;
			},
			getCurrentUser: function () {
				return currentUser;
			},
			setCurrentUser: function (user) {
				currentUser = user;
			},
			isAuthorized: function (accessLevels) {
				if (!accessLevels || accessLevels.length === 0) {
					return true;
				}
				if (accessLevels.indexOf(USER_ROLES.all) != -1) {
					return true;
				}
				if (accessLevels.indexOf(USER_ROLES.not_logged_in) !== -1 && !userService.isLoggedIn()) {
					return true;
				}
				return (userService.isLoggedIn() && accessLevels.indexOf(currentUser.role) !== -1);
			},
			activateUser: function (username, activationCode) {
				return activationResource.get({ username: username, activationCode: activationCode }, function () {

				}).$promise;
			}
		}
		return userService;
	};

	var module = angular.module("myApp");
	module.factory("user", ["$resource", "$http", "$rootScope", "USER_ROLES", userService]);
}());