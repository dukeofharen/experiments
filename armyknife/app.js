var express = require("express");
global.app = express();
var hbs = require("hbs");
var router = require("./router");
var bodyParser = require('body-parser');
var cons = require("./constants");
var snapin = require("./snapin");
var mongoose = require("mongoose");
mongoose.connect(cons.connection_string);

app.set("view engine", "html");
app.engine("html", hbs.__express);
app.use(express.static('public'));
app.use(bodyParser.urlencoded({extended: false,limit:'50mb'}));
app.use(bodyParser.json());

app.listen(cons.port_number);
console.log("Listening at port "+cons.port_number);
router.route();