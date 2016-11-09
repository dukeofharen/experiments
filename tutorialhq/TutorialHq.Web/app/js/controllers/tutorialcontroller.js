angular.module('myApp').controller('TutorialController', ["tutorial", "$routeParams", function (tutorial, $routeParams) {
	var self = this;

	var onError = function () {
		self.error = "Could not fetch data :(";
	};
	var onTutorialsComplete = function (data) {
		self.tutorials = data;
	};
	var onCategoriesComplete = function (data) {
		self.categories = data;
	};

	this.tutorials = [];
	this.categories = [];
	this.category;

	tutorial.getTutorials().then(onTutorialsComplete, onError);
	tutorial.getCategories().then(onCategoriesComplete, onError);

	this.categorySelectChange = function () {
		tutorial.getTutorials(this.category.id).then(onTutorialsComplete, onError);
	};
}]);