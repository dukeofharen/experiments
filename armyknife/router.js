var snapin = require("./snapin");
var app = snapin.getExpressObject();

exports.route = function(){
	app.get("/", function(req, res){
		res.render("index");
	});

	app.post("/execute", function(req, res){
		var command = req.body["command"];
		if(!command){
			res.status(400).send({valid: false, data: "You should send a command."});
		}
		else{
			var commandParts = command.split(' ');
			var snp = snapin.getSnapin(commandParts[0]);
			if(!snp){
				res.status(404).send({valid: false, data: "The command isn't found."});
			}
			else{
				commandParts.splice(0, 1);
				if(snp.execute){
					status = 200;
					var result = snp.execute(command);
					if(!result.valid){
						status = 400;
					}
					res.status(status).send(result);
				}
				else if(snp.asyncExecute){
					snp.asyncExecute(req, res, command);
				}
				else{
					res.status(500).send();
				}
			}
		}
	});
};