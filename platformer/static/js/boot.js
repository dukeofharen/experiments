var BasicGame = {};

BasicGame.Boot = function (game) {

};

BasicGame.Boot.prototype = {

    init: function () {

        //  Unless you specifically know your game needs to support multi-touch I would recommend setting this to 1
        this.input.maxPointers = 1;

        //  Phaser will automatically pause if the browser tab the game is in loses focus. You can disable that here:
       // this.stage.disableVisibilityChange = true;

        if (this.game.device.desktop)
        {
            //  If you have any desktop specific settings, they can go in here
            this.scale.pageAlignHorizontally = true;
        }
        else
        {
            //  Same goes for mobile settings.
            //  In this case we're saying "scale the game, no lower than 480x260 and no higher than 1024x768"
            this.scale.scaleMode = Phaser.ScaleManager.SHOW_ALL;
            this.scale.setMinMax(480, 260, 1024, 768);
            this.scale.forceLandscape = true;
            this.scale.pageAlignHorizontally = true;
        }

    },

    preload: function () {

        //  Here we load the assets required for our preloader (in this case a background and a loading bar)
        this.load.image('tiles-1', 'assets/tiles/tiles-1.png');
        this.load.image('cactus', 'assets/sprites/cactus.png');
        this.load.spritesheet('dude', 'assets/sprites/dude.png', 32, 48);
        this.load.spritesheet('droid', 'assets/sprites/droid.png', 32, 32);
        this.load.spritesheet('baddie1', 'assets/sprites/baddie1.png', 32, 32);
        this.load.image('baddieMarker', 'assets/sprites/baddie-marker.png');
        this.load.image('star', 'assets/sprites/star.png');
        this.load.image('starBig', 'assets/sprites/star2.png');
        this.load.image('diamond', 'assets/sprites/diamond.png');
        this.load.image('background', 'assets/backgrounds/background2.png');

        this.load.audio('jump', 'assets/audio/sfx/jump.wav');
        this.load.audio('coin', 'assets/audio/sfx/coin.wav');
        this.load.audio('stomp', 'assets/audio/sfx/stomp.wav');
        this.load.audio('stageClear', 'assets/audio/sfx/stage_clear.mp3');
        this.load.audio('song1', 'assets/audio/music/song1.mp3');
        //this.load.audio('titleMusic', 'assets/audio/music/titleMusic.mp3');

    },

    create: function () {

        //  By this point the preloader assets have loaded to the cache, we've set the game settings
        //  So now let's start the real preloader going
        this.state.start('Preloader');

    }

};