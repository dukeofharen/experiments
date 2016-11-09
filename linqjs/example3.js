var linq = require("linq");
//Let's say we have a large array of numbers

var arr = [];
for(var i=0;i<=1000;i++){
	arr.push(i);
}

//and we want 10 numbers from the 500th index. This is how it's done:
var result = linq.from(arr)
			 .skip(500)
			 .take(10)
			 .toArray();
console.log(result);

//Skip, like the name says, skips the first 500 numbers and take "takes" the next 10 numbers from the array