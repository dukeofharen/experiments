var linq = require("linq");

//Example 2
var grades = [4.4, 8.2, 5.6, 7.8, 6.9, 5.0, 9.8, 10.0, 7.9];
//Ok, I have an array of grades, but I want to know the average:

var average = linq.from(grades)
			  .average();
console.log("Average grade: "+average);

//There, easy, right?

//But what's the lowest grade?
var lowestGrade = linq.from(grades)
				  .min();
console.log("Lowest grade: "+lowestGrade);

//And the highest?
var highestGrade = linq.from(grades)
				  .max();
console.log("Highest grade: "+highestGrade);