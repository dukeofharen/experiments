$(document).ready(function(){
	$("#output textarea").focus(function() {
		var $this = $(this);
		$this.select();

		// Work around Chrome's little problem
		$this.mouseup(function() {
			// Prevent further mouseup intervention
			$this.unbind("mouseup");
			return false;
		});
	});
	$('.tooltip').tooltipster({ maxWidth: 500 });
	$('#social-link').click(function () {
	    $elem = $('.social');
	    if ($elem.is(":visible")) {
	        $elem.slideUp(500);
	    }
	    else {
	        $elem.slideDown(500);
	    }
	    return false;
	});
	$('#options').click(function(){
		optionsClick();
		return false;
	});
	$('#jquery').change(function(){
		if($(this).val() != ""){
			$('#mobilerow').show();
			$('#uirow').show();
		}
		else{
			$('#mobilerow').hide();
			$('#uirow').hide();
		}
	});
	$('#prototypejs').change(function(){
		if($(this).val() != ""){
			$('#scriptaculousrow').show();
		}
		else{
			$('#scriptaculousrow').hide();
		}
	});
});
function optionsClick() {
    $elem = $('#options-pane');
    if ($elem.is(":visible")) {
        $elem.slideUp(600);
    }
    else {
        $elem.slideDown(600);
    }
}