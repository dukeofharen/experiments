BasicGame.Level1_2 = function (game) {
	initialize(this);
};

BasicGame.Level1_2.prototype = {

    preload: function(){
        this.game.load.tilemap('level1', 'assets/levels/level1-2.json', null, Phaser.Tilemap.TILED_JSON);
    },

    create: function () {
        this.game.stage.backgroundColor = '#000000';
        var bg = this.game.add.tileSprite(0, 0, 1024, 768, 'background');
        bg.fixedToCamera = true;

        commonCreate(this, 32, 900, "Level 1-2", "Level1_3", "Level1_2", "song1");

        this.map = this.game.add.tilemap('level1');
        this.map.addTilesetImage('tiles-1');
        this.map.setCollisionByExclusion([ 13, 14, 15, 16, 46, 47, 48, 49, 50, 51 ]);
        this.layer = this.map.createLayer('Tile Layer 1');
        this.layer.resizeWorld();

        initializeStars(this, 'objects', 74);
        initializeBaddies(this, 'baddies', 79);
        initializeBaddieMarkers(this, 'baddieMarkers', 76);
        initializeDiamonds(this, 'diamonds', 70);

        commonAfterCreate(this);
    },

    update: function () {
        if(!this.game.physics.arcade.isPaused){
            commonUpdate(this);
        }
    },

    quitGame: function (pointer) {

        //  Here you should destroy anything you no longer need.
        //  Stop music, delete sprites, purge caches, free resources, all that good stuff.

        //  Then let's go back to the main menu.
        this.state.start('MainMenu');

    }

};