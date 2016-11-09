var express = require("express");
var app = express();
var hbs = require("hbs");
var router = require("./router");
var con = require("./constants");

app.set("view engine", "html");
app.engine("html", hbs.__express);
app.use(express.bodyParser());
app.use(express.static('public'));
app.use(express.urlencoded());
app.use(express.json());

app.listen(3000);
console.log("Started listening at port 3000");
router.route(app, con);