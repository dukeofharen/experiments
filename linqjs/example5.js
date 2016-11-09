var linq = require("linq");

//We have an array with a few objects inside. We only want an array with all names.

var arr = [
	{name: "Duco", country: "Netherlands"},
	{name: "Bill", country: "USA"},
	{name: "Norbert", country: "Belgium"}
];

//We do it like this
var result = linq.from(arr)
			 .select(function(x){
			 	return x.name;
			 })
			 .toArray();
console.log(result);
//There