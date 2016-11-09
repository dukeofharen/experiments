angular.module('myApp').controller('NavbarController', ["user", "$routeParams", "$rootScope", function (user, $routeParams, $rootScope) {
	var self = this;
	this.rootscope = $rootScope;
	this.getUser = function () {
		return user.getCurrentUser();
	};
}]);