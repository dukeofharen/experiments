var linq = require("linq");

//Let's say we have 2 arrays
var arr1 = ["a", "b"];
var arr2 = ["c", "d"];

//Let's combine these 2 arrays
var result = linq.from(arr1)
			 .concat(arr2)
			 .toArray();
console.log(result);