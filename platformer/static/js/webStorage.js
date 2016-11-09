var storage = (function(){
    var get = function(){
        return localStorage.game == null ? {} : JSON.parse(localStorage.game);
    };

    var set = function(game){
        localStorage.game = JSON.stringify(game);
    };

    return {
        get: get,
        set: set
    };
})();