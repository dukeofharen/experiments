$(document).ready(function(){
	$('#execute').click(function(){
		var cmdline = $('#cmdline');
		var command = cmdline.val();
		var result;
		$.post("/execute", {command: command})
		.done(function(data){
			result = data;
		})
		.fail(function(err){
			result = JSON.parse(err.responseText);
		})
		.always(function(){
			var resultText = typeof result == 'object' ? result.data : result+"";
			$('#result code').html(resultText);
			$('#result').slideDown();
		});
		return false;
	});

	$('#result code').dblclick(function(){
		$(this).selectText();
	});
});