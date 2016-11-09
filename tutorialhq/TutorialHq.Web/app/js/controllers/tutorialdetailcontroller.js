angular.module('myApp').controller('TutorialDetailController', ["tutorial", "$routeParams", function (tutorial, $routeParams) {
	var self = this;

	var onError = function () {
		self.error = "Could not fetch data :(";
	};
	var onTutorialDetailComplete = function (data) {
		self.tutorialDetail = data;
	};

	this.tutorialDetail = {};

	tutorial.getTutorial($routeParams.id).then(onTutorialDetailComplete, onError);
}]);