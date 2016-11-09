var express = require("express");
var app = express();
var port = 1000;

app.use(express.static("static"));
app.use(express.static("bower_components"));

app.listen(port);
console.log("App listening on port "+port);