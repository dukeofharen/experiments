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
	$('#language-link').click(function () {
	    $elem = $('.languages');
	    if ($elem.is(":visible")) {
	        $elem.slideUp(500);
	    }
	    else {
	        $elem.slideDown(500);
	    }
	    return false;
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